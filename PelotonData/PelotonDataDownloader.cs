using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PelotonData.JSONClasses;
using PelotonData.JSONClasses.EventDetails;
using PelotonData.JSONClasses.WorkoutList;
using PelotonData.JSONClasses.WorkoutSessionMetrics;

namespace PelotonData
{
    public class WebClientEx : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }
    }
    public class PelotonDataDownloader
    {
        static int ThrottleMilliseconds = 2000;

        JsonSerializerSettings JSonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        static void Main(string[] args)
        {
            var p = new PelotonDataDownloader();
            p.RunAsync("username", "password").RunSynchronously();
        }

        async Task RunAsync(string username, string password)
        {
            var auth = await AuthenticateAsync(username, password, null);
            var rideData = await GetWorkoutListAsync(auth, null);

            string directory = @"C:\Users\cbird\Documents\PelotonData";
            bool overwriteFiles = true;

            foreach (var ride in rideData)
            {
                try
                {
                    string filenameBase = Path.Combine(directory, GetFileNameBaseFromRideDatum(ride));
                    string metricsFilename = filenameBase + "_metrics.csv";
                    if (File.Exists(metricsFilename) && !overwriteFiles) continue;
                    var data = await GetWorkoutMetricsAsync(ride, null);
                    OutputRideCSV(data, metricsFilename);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Debug.WriteLine("\n\n*** Continuing ***");
                }
                await Throttle();
            }
        }

        public async Task<AuthResponse> AuthenticateAsync(string user, string password, ILogger logger, string SaveJsonPath=null)
        {
            string authURL = "https://api.onepeloton.com/auth/login";
            var info = new { password = password, username_or_email = user };
            string infoAsString = JsonConvert.SerializeObject(info);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


                var responseTask = client.UploadStringTaskAsync(authURL, infoAsString);
                string response = await responseTask;

                if (SaveJsonPath != null)
                {
                    var formattedJson = JToken.Parse(response).ToString(Formatting.Indented);
                    File.WriteAllText(SaveJsonPath, formattedJson);
                }

                Debug.WriteLine("  " + response.Substring(0, 50));
                Debug.WriteLine($"  Length: {response.Length}");

                var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response, JSonSerializerSettings);
                return authResponse;
            }
        }

        public string GetFileNameBaseFromRideDatum(RideDatum ride)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string str = rgx.Replace(ride.ride.title, "_");
            string filename = Util.DateTimeFromEpochSeconds(ride.device_time_created_at).ToString("yyy-dd-MM_HH-mm") + "_" + str;
            return filename;
        }



        public async Task<List<RideDatum>> GetWorkoutListAsync(AuthResponse auth, ILogger logger, string saveFileDirectory=null)
        {
            var rideDataList = new List<RideDatum>();
            string cookie = $"peloton_session_id={auth.session_id}";
            int pageNum = 0;
            while (true)
            {
                string url = $"https://api.onepeloton.com/api/user/{auth.user_id}/workouts?joins=ride&limit=20&page={pageNum}";
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Cookie, cookie);
                    client.Headers["accept"] = "application/json";
                    client.Headers["origin"] = "https://members.onepeloton.com";
                    client.Headers["accept-language"] = "en-US,en;q=0.9";
                    client.Headers["user-agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36";
                    client.Headers["accept"] = "application/json";
                    client.Headers["referer"] = "https://members.onepeloton.com/profile/workouts";
                    client.Headers["authority"] = "api.onepeloton.com";
                    client.Headers["x-requested-with"] = "XmlHttpRequest";
                    client.Headers["peloton-platform"] = "web";

                    if (logger != null) logger.Log($"Requesting page {pageNum + 1}");
                    var response = await client.DownloadStringTaskAsync(new Uri(url));
                    
                    Debug.WriteLine("  " + response.Substring(0, 50));
                    Debug.WriteLine($"  Length: {response.Length}");

                    if (saveFileDirectory != null)
                    {
                        string saveJsonPath = Path.Combine(saveFileDirectory, $"workoutListPage_{pageNum}.json");
                        var formattedJson = JToken.Parse(response).ToString(Formatting.Indented);
                        File.WriteAllText(saveJsonPath, formattedJson);
                    }

                    WorkoutList workoutList = JsonConvert.DeserializeObject<WorkoutList>(response, JSonSerializerSettings);
                    if (logger != null) logger.Log($"Page retrieved, contains {workoutList.count} workouts");

                    rideDataList.AddRange(workoutList.data);

                    if (workoutList.show_next)
                    {
                        pageNum++;
                        await Throttle();
                    } else
                    {
                        break;
                    }
                }
            }
            return rideDataList;
        }

        public async Task Throttle()
        {
            await Task.Delay(ThrottleMilliseconds);
        }

        public async Task<UserWorkoutDetails> GetWorkoutUserDetails(RideDatum ride, ILogger logger, string SaveJsonPath=null)
        {
            //https://api.onepeloton.com/api/workout/887f8325e5a14ba0be00705aaa6f2db1
            using (var client = new WebClient())
            {
                string url = $"https://api.onepeloton.com/api/workout/{ride.id}";
                if (logger != null) logger.Log($"Downloading user ride details for workout: {ride.ride.title} on {Util.DateTimeFromEpochSeconds(ride.device_time_created_at).ToShortDateString()}");
                var response = await client.DownloadStringTaskAsync(url);
                Debug.WriteLine("  " + response.Substring(0, 50));
                Debug.WriteLine($"  Length: {response.Length}");
                if (logger != null) logger.Log($"Done downloading");

                if (SaveJsonPath != null)
                {
                    var formattedJson = JToken.Parse(response).ToString(Formatting.Indented);
                    File.WriteAllText(SaveJsonPath, formattedJson);
                }
                UserWorkoutDetails userDetails = JsonConvert.DeserializeObject<UserWorkoutDetails>(response, JSonSerializerSettings);
                return userDetails;
            }
        }

        public async Task<EventDetails> GetWorkoutEventDetails(RideDatum ride, ILogger logger, string SaveJsonPath=null)
        {
            using (var client = new WebClient())
            {
                client.Headers["accept"] = "application/json";
                string url = $"https://api.onepeloton.com/api/ride/{ride.ride.id}/details";

                if (logger != null) logger.Log($"Downloading details for workout: {ride.ride.title} on {Util.DateTimeFromEpochSeconds(ride.device_time_created_at).ToShortDateString()}");
                var response = await client.DownloadStringTaskAsync(url);
                Debug.WriteLine("  " + response.Substring(0, 50));
                Debug.WriteLine($"  Length: {response.Length}");
                if (logger != null) logger.Log($"Done downloading");

                if (SaveJsonPath != null)
                {
                    var formattedJson = JToken.Parse(response).ToString(Formatting.Indented);
                    File.WriteAllText(SaveJsonPath, formattedJson);
                }
                EventDetails details = JsonConvert.DeserializeObject<EventDetails>(response, JSonSerializerSettings);
                return details;
            }
        }

        public async Task<WorkoutSessionMetrics> SimpleGetWorkoutSessionMetricsAsync(string id, int secondsPerObservation)
        {
            using (var client = new WebClient())
            {
                client.Headers["accept"] = "application/json";
                string url = $"https://api.onepeloton.com/api/workout/{id}/performance_graph?every_n={secondsPerObservation}";
                var response = await client.DownloadStringTaskAsync(url);
                return JsonConvert.DeserializeObject<WorkoutSessionMetrics>(response, JSonSerializerSettings);
            }
        }

        public async Task<WorkoutSessionMetrics> GetWorkoutMetricsAsync(RideDatum ride, ILogger logger, string SaveJsonPath=null)
        {
            using (var client = new WebClient())
            {
                client.Headers["accept"] = "application/json";
                string url = $"https://api.onepeloton.com/api/workout/{ride.id}/performance_graph?every_n=1";

                if (logger != null) logger.Log($"Downloading metrics for workout: {ride.ride.title} on {Util.DateTimeFromEpochSeconds(ride.device_time_created_at).ToShortDateString()}");
                var response = await client.DownloadStringTaskAsync(url);
                Debug.WriteLine("  " + response.Substring(0, 50));
                Debug.WriteLine($"  Length: {response.Length}");
                if (logger != null) logger.Log($"Done downloading");

                if (SaveJsonPath != null)
                {
                    var formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(response, JSonSerializerSettings), Formatting.Indented, JSonSerializerSettings);
                    File.WriteAllText(SaveJsonPath, formattedJson);
                }
                WorkoutSessionMetrics session = JsonConvert.DeserializeObject<WorkoutSessionMetrics>(response, JSonSerializerSettings);
                return session;
            }
        }

        public void OutputRideCSV(WorkoutSessionMetrics session, string filename)
        {
            List<string> lines = new List<string>();
            var header = "elapsed_seconds," + string.Join(",", session.metrics.Select(m => m.slug));
            lines.Add(header);

            var metricLists = session.metrics.Select(m => m.values);
            for (int i=0; i < session.seconds_since_pedaling_start.Count(); i++)
            {
                string line = session.seconds_since_pedaling_start[i] + "," +
                    string.Join(",", metricLists.Select(m => m[i]));
                lines.Add(line);
            }
            File.WriteAllLines(filename, lines);
        }

    }
}
