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
    /// Interaction logic for MoreWindow.xaml
    /// </summary>
    public partial class MoreWindow : Window
    {
        BL.IBL bl;
        private List<string> errorMessages;
        public MoreWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();
            errorMessages = new List<string>();
        }

        private void Grouping_Button_Click(object sender, RoutedEventArgs e)
        {
            new GroupingWindow().ShowDialog();
        }

        private void Maximum_percent_passing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Tester maxTester = bl.maxPrecentOfPassedTestTrainees();
                MessageBox.Show($"the tester with the maximum percents of passing: \n{maxTester}", "maximum percent", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "list of tests is empty", MessageBoxButton.OK, MessageBoxImage.Error);
            }      
        }

        private void Percent_of_passed_test_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double percent = bl.percentAverageOfPassedTestTrainees();
                int iPercent = Convert.ToInt32(percent);
                MessageBox.Show($"the average percent is: {iPercent} %", "average percent of past test traineet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "there are no tests yet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new TestsInYearWindow().ShowDialog();
        }
    }
}
