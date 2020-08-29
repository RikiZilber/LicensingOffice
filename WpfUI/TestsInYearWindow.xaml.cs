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
    /// Interaction logic for TestsInYearWindow.xaml
    /// </summary>
    public partial class TestsInYearWindow : Window
    {
        BL.IBL bl;
        private List<string> errorMessages;

        public TestsInYearWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.getBL();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            errorMessages = new List<string>();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (errorMessages.Any()) //errorMessages.Count > 0 
                {
                    string err = "Exception:";
                    foreach (var item in errorMessages)
                        err += "\n" + item;

                    MessageBox.Show(err);
                    return;
                }
                else
                {
                    textbox.BorderBrush = Brushes.Black;
                    if (textbox.Text == "")
                    {
                        textbox.BorderBrush = Brushes.Red;
                        return;
                    }

                    int year = int.Parse(textbox.Text);
                   
                    if (year < 2015 || year > 2050) throw new Exception("Year not in range");

                    int num = bl.numOfPassedTestForYear(year);
                    MessageBox.Show($"Number of passed test trainees for this yesr: "+ num, "Test in year", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);
            this.button.IsEnabled = !errorMessages.Any();
        }
    }
}
