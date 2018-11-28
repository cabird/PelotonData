using PelotonData;
using PelotonData.JSONClasses;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PelotonDataGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILogger
    {
        public MainWindow()
        {
            InitializeComponent();
            Log("Starting up");
        }

        public void Log(string s)
        {
            string text = LogTextBox.Text;
            text += s + "\n";
            LogTextBox.Text = text;
            LogTextBox.ScrollToEnd();
        }

        AuthResponse auth;

        private async void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            var p = new Program();
            Log("Authenticating");
            var authTask = p.AuthenticateAsync(UsernameTextBox.Text, PasswordTextBox.Text, this);
            auth = await authTask;
            Log("Done authenticating");

            Log("Getting list of workouts");
            var workoutList = await p.GetWorkoutListAsync(auth, this);
            Log($"Received {workoutList.Count} workouts");

            Log("Downloading data for each workout");
            foreach (var ride in workoutList)
            {
                string filename = p.GetFileNameFromRideDatum(ride);
                string path = System.IO.Path.Combine(OutputDirectoryTextBox.Text, filename);
                var data = await p.GetWorkoutMetricsAsync(ride, this);

                Log($"Writing data to {path}");
                p.OutputRideCSV(data, path);
                
            }
        }
    }
}
