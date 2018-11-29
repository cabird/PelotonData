using PelotonData;
using PelotonData.JSONClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PelotonDataGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILogger
    {
        ILogger Logger;
        public MainWindow()
        {
            InitializeComponent();
            Logger = this;
            Logger.Log("Starting up");
        }

        public void Log(LogEntry entry)
        {
            string logMessage = "";
            if (entry.Severity != LoggingEventType.Information) logMessage += entry.Severity.ToString() + ": ";
            if (entry.Message != null) logMessage += entry.Message;
            if (entry.Exception != null) logMessage += "\nException: " + entry.Exception.ToString();

            string text = LogTextBox.Text;
            text += logMessage + "\n";
            LogTextBox.Text = text;
            LogTextBox.ScrollToEnd();
        }

        AuthResponse auth;

        private void HandleException(Exception ex)
        {
            if (ex is WebException)
            {
                var wex = ex as WebException;
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    var statusCode = ((HttpWebResponse)wex.Response).StatusCode;
                    var statusDescription = ((HttpWebResponse)wex.Response).StatusDescription;
                    if (statusCode == HttpStatusCode.Unauthorized)
                    {
                        Logger.LogError("Received response \"Unauthorized\" from Peloton Server.  Please check you username and password");
                    }
                    else
                    {
                        Logger.LogError($"Received protocol error from Peloton Server: {statusCode} \"{statusDescription}\"");
                    }
                }
                else
                {
                    Logger.LogError($"Received Web Exception from Peloton Server", ex);
                }
            } else
            {
                Logger.LogError($"Error during execution", ex);
            }

        }

        private async Task SetProgress(double progress)
        {
            ProgressBar.Value = progress;
            await Task.Delay(100);
        }

        private async void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            double progress = 0.0;

            var p = new Program();

            await SetProgress(0);

            Logger.Log("Authenticating");
            var authTask = p.AuthenticateAsync(UsernameTextBox.Text, PasswordTextBox.Password, this);
            try
            {
                auth = await authTask;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                Logger.Log("Aborting...");
                return;
            }

            progress += 5.0;
            await SetProgress(progress);
          
            Logger.Log("Success!");

            List<RideDatum> workoutList;
            Logger.Log("Getting list of workouts");
            try
            {
                workoutList = await p.GetWorkoutListAsync(auth, this);
            } catch (Exception ex)
            {
                HandleException(ex);
                Logger.Log("Aborting...");
                return;
            }

            Logger.Log($"Received {workoutList.Count} workouts");

            progress += 5.0;
            await SetProgress(progress);

            double progressStep = (100 - progress) / workoutList.Count;

            Logger.Log("Downloading data for each workout");
            foreach (var ride in workoutList)
            {
                try
                {
                    string filename = p.GetFileNameFromRideDatum(ride);
                    string path = System.IO.Path.Combine(OutputDirectoryTextBox.Text, filename);
                    var data = await p.GetWorkoutMetricsAsync(ride, this);
                    Logger.Log($"Writing data to {path}");
                    p.OutputRideCSV(data, path);
                } catch (Exception ex)
                {
                    HandleException(ex);
                    Logger.Log("Skipping workout");
                }
                progress += progressStep;
                await SetProgress(progress);
            }
        }

        private void OutputDirectoryBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Output Directory";
            dlg.IsFolderPicker = true;
            if (String.IsNullOrWhiteSpace(OutputDirectoryTextBox.Text))
            {
                dlg.InitialDirectory = OutputDirectoryTextBox.Text;
            }

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            //dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputDirectoryTextBox.Text = dlg.FileName;
            }
        }
    }
}
