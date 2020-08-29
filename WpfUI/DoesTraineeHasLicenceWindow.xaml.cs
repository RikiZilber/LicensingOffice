using System;
using BE;
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
    /// Interaction logic for DoesTraineeHasLicenceWindow.xaml
    /// </summary>
    public partial class DoesTraineeHasLicenceWindow : Window
    {
        BL.IBL bl;
        BE.Trainee trainee;
        public DoesTraineeHasLicenceWindow(BE.Trainee t)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.FactoryBL.getBL();
            trainee = t;
            comboBox.ItemsSource = Enum.GetValues(typeof(CarType));
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedItem != null)
            {
                CarType c = (CarType)comboBox.SelectedItem;
                MessageBox.Show((bl.isAllowLicenseForCarType(trainee, c)).ToString(), "Does the trainee allow lisence for car type?", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show((bl.isAllowLicenseGeneral(trainee)).ToString(), "Does the trainee allow lisence general", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
