using Newtonsoft.Json;
using PelotonData.JSONClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scratch
{
    class Program
    {
        public static void Main(string[] args)
        {
            TestNullInJson();
        }

        public static void TestDownloadSessionMetrics()
        {
            var p = new PelotonData.PelotonDataDownloader();
            string workout = "64fea3aed3b34270b9e9e20df02f7759";
            var task = p.AuthenticateAsync("", "", null).Result;
            //task.RunSynchronously();
            var data = p.SimpleGetWorkoutSessionMetricsAsync(workout, 1).Result;
            p.OutputRideCSV(data, @"C:\Users\cbird\Documents\PelotonData\test.csv");
        }



        public static void TestNullInJson()
        {
            string path = @"C:\Users\cbird\Documents\PelotonData\authentication_response.json";
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(File.ReadAllText(path), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

    }
}
