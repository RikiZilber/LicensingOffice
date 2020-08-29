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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        Trainee trainee;
        BL.IBL bl;
        private List<string> errorMessages;
        Address address = new Address();

        public AddTraineeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            trainee = new Trainee();

            this.traineesDetailsGrid.DataContext = trainee;
            bl = BL.FactoryBL.getBL();
            this.birthDateDatePicker.DisplayDateEnd = DateTime.Now;


            this.personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(BE.Status));
            this.carTypeTraineeComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.genderTraineeComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.gearBoxtraineeComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));

            errorMessages = new List<string>();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ageTraineeTextBlock.Text = trainee.AgeTrainee.ToString();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
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
                if ((idTextBox.Text == "") || (firstNameTextBox.Text == ""))
                {
                    if (idTextBox.Text == "")
                        idTextBox.BorderBrush = Brushes.Red;
                    if (firstNameTextBox.Text == "")
                        firstNameTextBox.BorderBrush = Brushes.Red;
                    return;     // can not add a trainee without id or name
                }

                if ((cellPhoneLable.Content != "") || (firstNameLable.Content != "") ||
                        (idLable.Content != "") || (lastNameLable.Content != "") ||
                        (numLessonsLable.Content != "") || (schoolNameLable.Content != "") ||
                        (teacherNameLable.Content != "") || (cityLable.Content != "") ||
                        (houseLable.Content != "") || (streetLable.Content != ""))
                {
                    return;     // user has to fix the input first
                }

                trainee.AddressTrainee = address;
                bl.addTrainee(trainee);
                MessageBox.Show($"Trainee " + trainee.FirstName + " was successfully added!", "Trainee added", MessageBoxButton.OK, MessageBoxImage.Information);
                carTypeTraineeComboBox.SelectedItem = null;
                cellPhoneTextBox.Text = "";
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                idTextBox.Text = "";
                numLessonsTextBox.Text = "";
                schoolNameTextBox.Text = "";
                teacherNameTextBox.Text = "";
                cityTextBox.Text = "";
                houseNumberTextBox.Text = "";
                streetTextBox.Text = "";
                personalStatusComboBox.SelectedItem = null;
                gearBoxtraineeComboBox.SelectedItem = null;
                genderTraineeComboBox.SelectedItem = null;

                trainee = new Trainee();
                this.traineesDetailsGrid.DataContext = trainee;
                this.AddressGrid.DataContext = trainee.AddressTrainee;

                //this.Close();     
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

            //this.addButton.IsEnabled = !errorMessages.Any();
        }

        private void Choose_Image_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                this.traineeImage.Source = new BitmapImage(new Uri(f.FileName));
            }
        }

        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.FirstName = firstNameTextBox.Text;
            }
            catch (Exception exp)
            {
                firstNameTextBox.BorderBrush = Brushes.Red;
                firstNameLable.Content = exp.Message;
            }
        }

        private void FirstNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            firstNameLable.Content = "";
            firstNameTextBox.BorderBrush = Brushes.Black;
        }

        private void CellPhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.CellPhone = cellPhoneTextBox.Text;
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

        private void IdTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.Id = idTextBox.Text;
            }
            catch (Exception exp)
            {
                idTextBox.BorderBrush = Brushes.Red;
                idLable.Content = exp.Message;
            }
        }

        private void IdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            idLable.Content = "";
            idTextBox.BorderBrush = Brushes.Black;
        }

        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.LastName = lastNameTextBox.Text;
            }
            catch (Exception exp)
            {
                lastNameTextBox.BorderBrush = Brushes.Red;
                lastNameLable.Content = exp.Message;
            }
        }

        private void LastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lastNameLable.Content = "";
            lastNameTextBox.BorderBrush = Brushes.Black;
        }

        private void NumLessonsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.NumLessons = int.Parse(numLessonsTextBox.Text);
            }
            catch (Exception exp)
            {
                numLessonsTextBox.BorderBrush = Brushes.Red;
                numLessonsLable.Content = exp.Message;
            }
        }

        private void NumLessonsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            numLessonsLable.Content = "";
            numLessonsTextBox.BorderBrush = Brushes.Black;
        }

        private void SchoolNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.SchoolName = schoolNameTextBox.Text;
            }
            catch (Exception exp)
            {
                schoolNameTextBox.BorderBrush = Brushes.Red;
                schoolNameLable.Content = exp.Message;
            }
        }

        private void SchoolNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            schoolNameLable.Content = "";
            schoolNameTextBox.BorderBrush = Brushes.Black;
        }

        private void TeacherNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.TeacherName = teacherNameTextBox.Text;
            }
            catch (Exception exp)
            {
                teacherNameTextBox.BorderBrush = Brushes.Red;
                teacherNameLable.Content = exp.Message;
            }
        }

        private void TeacherNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            teacherNameLable.Content = "";
            teacherNameTextBox.BorderBrush = Brushes.Black;
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



        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource addressViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("addressViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // addressViewSource.Source = [generic data source]
        //}

    }
}
