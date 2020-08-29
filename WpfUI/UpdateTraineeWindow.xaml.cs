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
    /// Interaction logic for UpdateTraineeWindow.xaml
    /// </summary>
    public partial class UpdateTraineeWindow : Window
    {
        Trainee trainee;
        BL.IBL bl;
        private List<string> errorMessages;
        Address address = new Address();

        public UpdateTraineeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            trainee = null;
            this.birthDateDatePicker.DisplayDateEnd = DateTime.Now;

            this.carTypeTraineeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.gearBoxtraineeComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            this.genderTraineeComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(Status));

            this.idCombobox.ItemsSource = bl.getTraineesList();
            this.idCombobox.DisplayMemberPath = "Id";
            this.idCombobox.SelectedValuePath = "Id";

            errorMessages = new List<string>();
        }

        private void IdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idCombobox.SelectedItem is Trainee)
            {
                this.trainee = ((Trainee)this.idCombobox.SelectedItem).DeepClone();
                address = trainee.AddressTrainee;
                setTraineesFields();
            }
        }

        private void setTraineesFields()
        {
            this.traineesDetailsGrid.DataContext = trainee;

            cellPhoneTextBox.Text = trainee.CellPhone;
            firstNameTextBox.Text = trainee.FirstName;
            lastNameTextBox.Text = trainee.LastName;
            numLessonsTextBox.Text = trainee.NumLessons.ToString();
            schoolNameTextBox.Text = trainee.SchoolName;
            teacherNameTextBox.Text = trainee.TeacherName;
            cityTextBox.Text = trainee.AddressTrainee.City;
            houseNumberTextBox.Text = trainee.AddressTrainee.HouseNumber.ToString();
            streetTextBox.Text = trainee.AddressTrainee.Street;
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

                if (idCombobox.SelectedItem == null)
                {
                    MessageBox.Show($"Please choose a trainee to update!", "Update trainee", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;     // can not update a trainee without id
                }
                    if ((cellPhoneLable.Content != "") || (firstNameLable.Content != "") ||
                       (lastNameLable.Content != "") || (numLessonsLable.Content != "") ||
                       (schoolLable.Content != "") || (teacherLable.Content != "") ||
                       (cityLable.Content != "") || (houseLable.Content != "") ||
                       (streetLable.Content != ""))
                {
                    return;     // user has to fix the input first
                }

                trainee.AddressTrainee = address;
                bl.updateTrainee(trainee);
                MessageBox.Show("Trainee " + trainee.FirstName + " was successfully updated!", "Trainee updated", MessageBoxButton.OK, MessageBoxImage.Information);
                cellPhoneTextBox.Text = "";
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                numLessonsTextBox.Text = "";
                schoolNameTextBox.Text = "";
                teacherNameTextBox.Text = "";
                cityTextBox.Text = "";
                houseNumberTextBox.Text = "";
                streetTextBox.Text = "";
                


                this.traineesDetailsGrid.DataContext = trainee = null;
                this.AddressGrid.DataContext = null;
                this.idCombobox.ItemsSource = bl.getTraineesList();

                //this.close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.traineesDetailsGrid.DataContext = trainee = ((Trainee)this.idCombobox.SelectedItem).DeepClone();
                this.AddressGrid.DataContext = trainee.AddressTrainee;
            }
        }

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
                schoolLable.Content = exp.Message;
            }
        }

        private void SchoolNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            schoolLable.Content = "";
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
                teacherLable.Content = exp.Message;
            }
        }

        private void TeacherNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            teacherLable.Content = "";
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
    }
}
