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
using System.Windows.Shapes;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for TraineeWindow.xaml
    /// </summary>
    public partial class TraineeWindow : Window
    {
        public TraineeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Add_Trainee_Button_Click(object sender, RoutedEventArgs e)
        {
            new AddTraineeWindow().ShowDialog();
        }

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {
            new GetTraineesWindow().ShowDialog();
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTraineeWindow().ShowDialog();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            new deleteTraineeWindow().ShowDialog();
        }
    }
}
