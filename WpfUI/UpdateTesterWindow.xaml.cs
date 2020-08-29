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
    /// Interaction logic for UpdateTesterWindow.xaml
    /// </summary>
    public partial class UpdateTesterWindow : Window
    {
        Tester tester;
        BL.IBL bl;
        private List<string> errorMessages;
        CheckBox[,] c = new CheckBox[6, 5];
        Address address = new Address();


        public UpdateTesterWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            tester = null;

            this.birthDateDatePicker.DisplayDateEnd = DateTime.Now;
            this.carTypeTesterComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.workerTypeComboBox.ItemsSource = Enum.GetValues(typeof(WorkerType));
            this.genderTesterComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(Status));

            setMatrix();

            this.idComboBox.ItemsSource = bl.getTestersList();
            this.idComboBox.DisplayMemberPath = "Id";
            this.idComboBox.SelectedValuePath = "Id";

            errorMessages = new List<string>();
        }

        private void setMatrix()
        {
            for (int i = 0; i < Configuration.NUM_OF_HOURS; i++)
            {
                for (int j = 0; j < Configuration.NUM_OF_DAYS; ++j)
                {
                    c[i, j] = new CheckBox();
                    Grid.SetColumn(c[i, j], j);
                    Grid.SetRow(c[i, j], i);
                    c[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                    c[i, j].VerticalAlignment = VerticalAlignment.Center;
                    matrixCeckBoxes.Children.Add(c[i, j]);
                }
            }
        }

        private void IdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idComboBox.SelectedItem is Tester)
            {
                this.tester = ((Tester)this.idComboBox.SelectedItem).DeepClone();
                address = tester.AddressTester;
                setTesterFields();
            }
        }

        private void setTesterFields()
        {
            this.testerDetailsGrid.DataContext = tester;
            //this.AddressGrid.DataContext = tester.AddressTester;
            firstNameTextBox.Text = tester.FirstName;
            lastNameTextBox.Text = tester.LastName;
            maxDistanceTesterTextBox.Text = tester.MaxDistanceTester.ToString();
            maxWeeklyTestsTextBox.Text = tester.MaxWeeklyTests.ToString();
            cellPhoneTextBox.Text = tester.CellPhone;
            experienceYearsTextBox.Text = tester.ExperienceYears.ToString();
            cityTextBox.Text = tester.AddressTester.City;
            houseNumberTextBox.Text = tester.AddressTester.HouseNumber.ToString();
            streetTextBox.Text = tester.AddressTester.Street;

            for (int i = 0; i < Configuration.NUM_OF_HOURS; i++)
            {
                for (int j = 0; j < Configuration.NUM_OF_DAYS; ++j)
                {
                    c[i, j].IsChecked = tester.AvailabilityTester[i, j];
                }
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
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

                if (idComboBox.SelectedItem == null)
                {
                    MessageBox.Show($"Please choose a tester to update!", "Update tester", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;     // can not update a tester without id
                }

                if ((cellPhoneLable.Content != "") || (firstNameLable.Content != "") || (lastNameLable.Content != "") ||
                        (experienceYearsLable.Content != "") || (maxDistanceTesterLable.Content != "") ||
                        (maxWeeklyTestLable.Content != "") || (cityLable.Content != "") ||
                        (houseLable.Content != "") || (streetLable.Content != ""))
                {
                    MessageBox.Show($"Not all files are correct", "Update tester", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;     // user has to fix the input first
                }

                for (int i = 0; i < Configuration.NUM_OF_HOURS; i++)
                {
                    for (int j = 0; j < Configuration.NUM_OF_DAYS; ++j)
                    {
                        tester.AvailabilityTester[i, j] = c[i, j].IsChecked == true ? true : false;
                    }
                }

                tester.AddressTester = address;

                bl.updateTester(tester);

                MessageBox.Show("Tester " + tester.FirstName + " was successfully updated!", "Tester updated", MessageBoxButton.OK, MessageBoxImage.Information);
                cellPhoneTextBox.Text = "";
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                experienceYearsTextBox.Text = "";
                maxDistanceTesterTextBox.Text = "";
                maxDistanceTesterTextBox.Text = "";
                cityTextBox.Text = "";
                houseNumberTextBox.Text = "";
                streetTextBox.Text = "";
                maxWeeklyTestsTextBox.Text = "";

                this.testerDetailsGrid.DataContext = tester = null;
                this.AddressGrid.DataContext = null;
                for (int i = 0; i < Configuration.NUM_OF_HOURS; i++)
                {
                    for (int j = 0; j < Configuration.NUM_OF_DAYS; ++j)
                    {
                        c[i, j].IsChecked = false;
                    }
                }
                this.idComboBox.ItemsSource = bl.getTestersList();

                //this.close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                setTesterFields();
            }
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // testerViewSource.Source = [generic data source]
        //}

        //from oshri:
        //private void changeImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
        //    f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        //    if (f.ShowDialog() == true)
        //    {
        //        this.studentImage.Source = new BitmapImage(new Uri(f.FileName));
        //    }
        //}

        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            this.updateButton.IsEnabled = !errorMessages.Any();
        }
        private void CellPhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.CellPhone = cellPhoneTextBox.Text;
            }
            catch (Exception exp)
            {
                cellPhoneTextBox.BorderBrush = Brushes.Red;
                cellPhoneLable.Content = exp.Message;
            }
        }

        private void CellPhoneTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            cellPhoneLable.Content = "";
            cellPhoneTextBox.BorderBrush = Brushes.Black;
        }

        private void experienceYearsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.ExperienceYears = int.Parse(experienceYearsTextBox.Text);
            }
            catch (Exception exp)
            {
                experienceYearsTextBox.BorderBrush = Brushes.Red;
                experienceYearsLable.Content = exp.Message;
            }
        }

        private void experienceYearsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            experienceYearsLable.Content = "";
            experienceYearsTextBox.BorderBrush = Brushes.Black;
        }
        private void firstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.FirstName = firstNameTextBox.Text;
            }
            catch (Exception exp)
            {
                firstNameTextBox.BorderBrush = Brushes.Red;
                firstNameLable.Content = exp.Message;
            }
        }

        private void firstNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            firstNameLable.Content = "";
            firstNameTextBox.BorderBrush = Brushes.Black;
        }

        private void lastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.LastName = lastNameTextBox.Text;
            }
            catch (Exception exp)
            {
                lastNameTextBox.BorderBrush = Brushes.Red;
                lastNameLable.Content = exp.Message;
            }
        }

        private void lastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lastNameLable.Content = "";
            lastNameTextBox.BorderBrush = Brushes.Black;
        }
        private void maxDistanceTesterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.MaxDistanceTester = int.Parse(maxDistanceTesterTextBox.Text);
            }
            catch (Exception exp)
            {
                maxDistanceTesterTextBox.BorderBrush = Brushes.Red;
                maxDistanceTesterLable.Content = exp.Message;
            }
        }

        private void maxDistanceTesterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            maxDistanceTesterLable.Content = "";
            maxDistanceTesterTextBox.BorderBrush = Brushes.Black;
        }
        private void maxWeeklyTestsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.MaxWeeklyTests = int.Parse(maxWeeklyTestsTextBox.Text);
            }
            catch (Exception exp)
            {
                maxWeeklyTestsTextBox.BorderBrush = Brushes.Red;
                maxWeeklyTestLable.Content = exp.Message;
            }
        }

        private void maxWeeklyTestsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            maxWeeklyTestLable.Content = "";
            maxWeeklyTestsTextBox.BorderBrush = Brushes.Black;
        }

        private void CityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                address.City = cityTextBox.Text;
            }
            catch (Exception exp)
            {
                cityTextBox.BorderBrush = Brushes.Red;
                cityLable.Content = exp.Message;
            }
        }

        private void CityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            cityLable.Content = "";
            cityTextBox.BorderBrush = Brushes.Black;
        }

        private void HouseNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                address.HouseNumber = int.Parse(houseNumberTextBox.Text);
            }
            catch (Exception exp)
            {
                houseNumberTextBox.BorderBrush = Brushes.Red;
                houseLable.Content = exp.Message;
            }
        }

        private void HouseNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            houseLable.Content = "";
            houseNumberTextBox.BorderBrush = Brushes.Black;
        }

        private void StreetTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                address.Street = streetTextBox.Text;
            }
            catch (Exception exp)
            {
                streetTextBox.BorderBrush = Brushes.Red;
                streetLable.Content = exp.Message;
            }
        }

        private void StreetTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            streetLable.Content = "";
            streetTextBox.BorderBrush = Brushes.Black;
        }
    }
}
