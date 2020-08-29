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
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        Test test;
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateTestWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            test = new Test();

            this.testCodeComboBox.ItemsSource = bl.getTestsList();
            this.testCodeComboBox.DisplayMemberPath = "TestCode";
            this.testCodeComboBox.SelectedValuePath = "TestCode";

            errorMessages = new List<string>();
        }

        private void TestCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.testCodeComboBox.SelectedItem is Test)
            {
                this.test = ((Test)this.testCodeComboBox.SelectedItem).DeepClone();
                this.testDetailsGrid.DataContext = test;
                if (test.ScoreTest != null)
                    MessageBox.Show("You can not update a test which has been updated already!", "Test updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (testCodeComboBox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a test first!");
                return;
            }

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
                    if (test.ScoreTest != null)
                    {
                        MessageBox.Show("Pleases choose another test to update!", "Test updated", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    if (noteTextbox.Text == "")
                    {
                        MessageBox.Show("You must write a note for trainee's score!");
                        noteTextbox.BorderBrush = Brushes.Red;
                        return;
                    }
                    test.ScoreTest = scoreCheckbox.IsChecked == true ? true : false;
                    test.TesterNote = noteTextbox.Text;
                    test.Criteria[Parameters.distance_keeping] = distanceCheckboc.IsChecked == true ? true : false;
                    test.Criteria[Parameters.mirrors_looking] = mirrorsCheckboc.IsChecked == true ? true : false;
                    test.Criteria[Parameters.reverse_parking] = reverseCheckboc.IsChecked == true ? true : false;
                    test.Criteria[Parameters.signaling] = signalingCheckboc.IsChecked == true ? true : false;
                    test.Criteria[Parameters.traffic_signs] = trafficCheckboc.IsChecked == true ? true : false;

                    bl.updateTest(test);
                    MessageBox.Show("Test " + test.TestCode + " was successfully updated!", "Test updated", MessageBoxButton.OK, MessageBoxImage.Information);

                    test = new Test();
                    this.testDetailsGrid.DataContext = test;               
                    this.testCodeComboBox.ItemsSource = bl.getTestsList();
                    restart();

                    //this.close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.test = ((Test)this.testCodeComboBox.SelectedItem).DeepClone();
                this.testDetailsGrid.DataContext = test;
                restart();
            }
        }

        private void restart()
        {
            scoreCheckbox.IsChecked = false;
            noteTextbox.Text = "";
            distanceCheckboc.IsChecked = false;
            mirrorsCheckboc.IsChecked = false;
            signalingCheckboc.IsChecked = false;
            reverseCheckboc.IsChecked = false;
            trafficCheckboc.IsChecked = false;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // testViewSource.Source = [generic data source]
        //}
    }
}
