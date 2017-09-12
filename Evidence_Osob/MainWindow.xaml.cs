using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Evidence_Osob
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = new Timer();       
        Rest webClient = new Rest();
        ObservableCollection<Person> persons = new ObservableCollection<Person>();
        string downloadTime;
        public MainWindow()
        {
            InitializeComponent();
            SetTimeInMili();
            GetDataAsync();
        }

        public async Task GetDataAsync()
        {
            persons = await webClient.GetPersonsListAsync();
            ListBox.ItemsSource = persons;
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
        }

        private void SetTimeInMili()
        {
            new System.Windows.Threading.DispatcherTimer(new TimeSpan(0, 0, 0, 0, 5), DispatcherPriority.SystemIdle, delegate
            {
                lblTime.Content = DateTime.Now.ToString("ffff");
            }, Dispatcher);
        }

        public void SaveToFile()
        {
            downloadTime = lblTime.Content.ToString();
            string json = DateTime.Now + " " + downloadTime + " " + JsonConvert.SerializeObject(persons.ToArray());
            System.IO.File.WriteAllText(@"E:\Person.txt", json);        
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            downloadTime = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
