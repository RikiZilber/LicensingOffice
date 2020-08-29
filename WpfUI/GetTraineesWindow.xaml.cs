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
    /// Interaction logic for GetTraineesWindow.xaml
    /// </summary>
    public partial class GetTraineesWindow : Window
    {
        BL.IBL bl;

        public GetTraineesWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            this.TraineeDataGrid.ItemsSource = bl.getTraineesList();
     
            comboBoxCarType.ItemsSource = Enum.GetValues(typeof(CarType));
            ComboBoxGender.ItemsSource = Enum.GetValues(typeof(Gender));
            ComboBoxGearBox.ItemsSource = Enum.GetValues(typeof(GearBox));

        }

        private void more_details_click(object sender, RoutedEventArgs e)
        {
            Trainee obj = this.TraineeDataGrid.SelectedItem as Trainee;
            if (obj != null)
            {
                MessageBox.Show($"Datails of Trainee: \n{obj}", "Trainee's Datails", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //user sign check box is handicapped as true
        private void CheckBoxIsHandicapped_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxIsHandicapped.IsChecked != false)
            {
                handicappedClear.Visibility = Visibility.Visible;
                comboBoxCarType.SelectedItem = null;//cannot filter with another criterias
                ComboBoxGender.SelectedItem = null;
                ComboBoxGearBox.SelectedItem = null;
                TextBoxSchoolName.Text = "";
                this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.IsHandicapped == true);
            }
            else
            handicappedClear.Visibility = Visibility.Hidden;
        }
         
        //for the whole list
        private void handicappedClear_Click(object sender, RoutedEventArgs e)
        {
            CheckBoxIsHandicapped.IsChecked = false;
            handicappedClear.Visibility = Visibility.Hidden;
            this.TraineeDataGrid.ItemsSource = bl.getTraineesList();
        }

        private void num_test_click(object sender, RoutedEventArgs e)
        {
            Trainee t = this.TraineeDataGrid.SelectedItem as Trainee;
            if (t != null)
            {
                try
                {
                    new numberOfTestForTraineeWindow(t).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void have_licence_click(object sender, RoutedEventArgs e)
        {
            Trainee t = this.TraineeDataGrid.SelectedItem as Trainee;
            if (t != null)
            {
                try
                {
                    new DoesTraineeHasLicenceWindow(t).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //user choose one of car type
        private void comboBoxCarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCarType.SelectedItem != null)
            {
                CarType c = (CarType)comboBoxCarType.SelectedItem;
                carClear.Visibility = Visibility.Visible;
                ComboBoxGearBox.SelectedItem = null;
                TextBoxSchoolName.Text = "";
                CheckBoxIsHandicapped.IsChecked = false;
                handicappedClear.Visibility = Visibility.Hidden;
            }
            else carClear.Visibility = Visibility.Hidden;
            filter(); //this.TraineeDataGrid.ItemsSource = bl.getTraineeList(t => t.CarTypeTester == c);
        }

        //for the whole list
        private void CarClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCarType.SelectedItem = null;
            carClear.Visibility = Visibility.Hidden;
        }

        //user choose specific gender
        private void ComboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxGender.SelectedItem != null)
            {
                Gender g = (Gender)ComboBoxGender.SelectedItem;
                genderClear.Visibility = Visibility.Visible;
                ComboBoxGearBox.SelectedItem = null;
                TextBoxSchoolName.Text = "";
                CheckBoxIsHandicapped.IsChecked = false;
                handicappedClear.Visibility = Visibility.Hidden;
            }
            else genderClear.Visibility = Visibility.Hidden;
            filter(); //this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.Gender == g);
        }

        //for the whole list
        private void GenderClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxGender.SelectedItem = null;
            genderClear.Visibility = Visibility.Hidden;
        }

        //user choose specific gear box
        private void ComboBoxGearBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxGearBox.SelectedItem != null)
            {
                GearBox w = (GearBox)ComboBoxGearBox.SelectedItem;
                gearBoxClear.Visibility = Visibility.Visible;
                comboBoxCarType.SelectedItem = null;
                ComboBoxGender.SelectedItem = null;
                CheckBoxIsHandicapped.IsChecked = false;
                handicappedClear.Visibility = Visibility.Hidden;
            }
            else gearBoxClear.Visibility = Visibility.Hidden;
            filter(); //this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.GearBox == w);
        }

        //for the whole list
        private void GearBoxClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxGearBox.SelectedItem = null;
            gearBoxClear.Visibility = Visibility.Hidden;
        }

        //user choose specific school name
        private void TextBoxSchoolName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSchoolName.Text != "")
            {
                try
                {
                    string school = (TextBoxSchoolName.Text).ToString();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                schoolClear.Visibility = Visibility.Visible;
                comboBoxCarType.SelectedItem = null;
                ComboBoxGender.SelectedItem = null;
                CheckBoxIsHandicapped.IsChecked = false;
                handicappedClear.Visibility = Visibility.Hidden;
            }
            else schoolClear.Visibility = Visibility.Hidden;
            filter(); // //this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.SchoolName == school);
        }

        //for the whole list
        private void schoolClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSchoolName.Text = "";
            schoolClear.Visibility = Visibility.Hidden;
        }

        private void filter()
        {
            bool car = comboBoxCarType.SelectedItem != null ? true : false;
            bool gender = ComboBoxGender.SelectedItem != null ? true : false;
            bool gearBox = ComboBoxGearBox.SelectedItem != null ? true : false;
            bool school = TextBoxSchoolName.Text != "" ? true : false;

            if (car || gender)  //user wants to filter by car type or gender
            {
                if (car && !gender)
                {
                    CarType c = (CarType)comboBoxCarType.SelectedItem;
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.CarTypeTrainee == c);
                    return;
                }
                if (gender && !car)
                {
                    Gender g = (Gender)ComboBoxGender.SelectedItem;
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.GenderTrainee == g);
                    return;
                }
                if (car && gender)
                {
                    CarType c = (CarType)comboBoxCarType.SelectedItem;
                    Gender g = (Gender)ComboBoxGender.SelectedItem;
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => (t.CarTypeTrainee == c) && (t.GenderTrainee == g));
                    return;
                }
            }

            if (gearBox || school)      //user wants to filter by gear box or by school name
            {
                if (gearBox && !school)
                {
                    GearBox g = (GearBox)ComboBoxGearBox.SelectedItem;
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.gearBoxtrainee == g);
                    return;
                }
                if (school && !gearBox)
                {
                    string s = (TextBoxSchoolName.Text).ToString();
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => t.SchoolName == s);
                    return;
                }
                if (school && gearBox)
                {
                    GearBox g = (GearBox)ComboBoxGearBox.SelectedItem;
                    string s = (TextBoxSchoolName.Text).ToString();
                    this.TraineeDataGrid.ItemsSource = bl.getTraineesList(t => (t.SchoolName == s) && (t.gearBoxtrainee == g));

                    return;
                }
            }
            this.TraineeDataGrid.ItemsSource = bl.getTraineesList();
        }

        
    }
}

