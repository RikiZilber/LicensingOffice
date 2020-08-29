using BE;
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
    /// Interaction logic for numberOfTestForTraineeWindow.xaml
    /// </summary>
    public partial class numberOfTestForTraineeWindow : Window
    {
        BL.IBL bl;
        BE.Trainee trainee;
        public numberOfTestForTraineeWindow(BE.Trainee t)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();
            trainee = t;
            comboBox.ItemsSource = Enum.GetValues(typeof(CarType));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedItem!=null)
            {
                CarType c = (CarType)comboBox.SelectedItem;
                MessageBox.Show("Nubmer of test: "+(bl.getNumTestsForCarType(trainee,c)).ToString(), "Number of tests for car type ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nubmer of test: "+(bl.getNumTestsGeneral(trainee)).ToString(), "Number of tests general ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
