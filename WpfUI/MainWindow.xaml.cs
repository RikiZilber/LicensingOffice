using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;


namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CarSongs.wav")); // to get relative location
        bool soundFlag = true;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            player.PlayLooping();
        }

        private void Trainee_Options_Button_Click(object sender, RoutedEventArgs e)
        {
            new TraineeWindow().ShowDialog();
        }

        private void Tester_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            new TesterWindow().ShowDialog();
        }

        private void Test_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            new TestWindow().ShowDialog();
        }

        private void More_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            new MoreWindow().ShowDialog();
        }

        private void Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            if (soundFlag) //music is being playing
            {
                player.Stop();
                pause.Source = new ImageSourceConverter().ConvertFromString(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "play.jpg")) as ImageSource;  // to get relative location
                soundFlag = false;
            }
            else //music is not being playing
            {
                player.PlayLooping();
                pause.Source = new ImageSourceConverter().ConvertFromString(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pause.jpg")) as ImageSource;  // to get relative location
                soundFlag = true;
            }
        }
    }
}
