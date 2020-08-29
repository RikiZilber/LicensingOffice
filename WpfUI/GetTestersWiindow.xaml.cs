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
    /// Interaction logic for GetTestersWiindow.xaml
    /// </summary>
    public partial class GetTestersWiindow : Window
    {
        BL.IBL bl;
        Address address;
        int radius;

        public GetTestersWiindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();
            this.TesterDataGrid.ItemsSource = bl.getTestersList();

            this.datePicker.DisplayDateStart = DateTime.Now;
            this.datePicker.SelectedDate = DateTime.Now;
            string[] hours = new string[6] { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00" };
            this.comboBoxHours.ItemsSource = hours;
            comboBoxCarType.ItemsSource = Enum.GetValues(typeof(CarType));
            ComboBoxGender.ItemsSource = Enum.GetValues(typeof(Gender));
            ComboBoxWorkerType.ItemsSource = Enum.GetValues(typeof(WorkerType));
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            Tester t = this.TesterDataGrid.SelectedItem as Tester;
            if (t != null)
            {
                try
                {
                    MessageBox.Show($"Datails of Tester: \n{t}", "Tester's Datails", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // shows for each tester (if user pushes the button "%") the percents of trainees tested by him- who passed the test
        private void show_percents_click(object sender, RoutedEventArgs e)
        {
            Tester t = this.TesterDataGrid.SelectedItem as Tester;
            if (t != null)
            {
                try
                {
                    MessageBox.Show($"percent of past test trainees for trster: \n" + (bl.precentOfPassedTestTraineesForTester(t)).ToString() + "%", "percent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // called when user changed the selected value of the car taype combobox
        private void comboBoxCarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCarType.SelectedItem != null)
            {
                CarType c = (CarType)comboBoxCarType.SelectedItem;
                carClear.Visibility = Visibility.Visible;
                unEnableSecondLine();
                ComboBoxWorkerType.SelectedItem = null;
                TextBoxMinimumexperienceYear.Text = "";
            }
            else carClear.Visibility = Visibility.Hidden;
            filter(); //this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.CarTypeTester == c);
        }

        // reset the selection of car type combobox
        private void CarClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCarType.SelectedItem = null;
            carClear.Visibility = Visibility.Hidden;
        }

        // called when user changed the selected value of the gender combobox
        private void ComboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxGender.SelectedItem != null)
            {
                Gender g = (Gender)ComboBoxGender.SelectedItem;
                genderClear.Visibility = Visibility.Visible;
                unEnableSecondLine();
                ComboBoxWorkerType.SelectedItem = null;
                TextBoxMinimumexperienceYear.Text = "";
            }
            else genderClear.Visibility = Visibility.Hidden;
            filter(); //this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.Gender == g);
        }

        // reset the selection of gender combobox
        private void GenderClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxGender.SelectedItem = null;
            genderClear.Visibility = Visibility.Hidden;
        }

        // called when user changed the selected value of the worker type combobox
        private void ComboBoxWorkerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxWorkerType.SelectedItem != null)
            {
                WorkerType w = (WorkerType)ComboBoxWorkerType.SelectedItem;
                workerClear.Visibility = Visibility.Visible;
                unEnableSecondLine();
                comboBoxCarType.SelectedItem = null;
                ComboBoxGender.SelectedItem = null;
            }
            else workerClear.Visibility = Visibility.Hidden;
            filter(); //this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.WorkerType == w);
        }

        // reset the selection of worker type combobox
        private void WorkerClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxWorkerType.SelectedItem = null;
            workerClear.Visibility = Visibility.Hidden;
        }

        // called when user changed the selected value of the min experience text box
        private void TextBoxMinimumexperienceYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxMinimumexperienceYear.Text != "")
            {
                try
                {
                    int num = int.Parse(TextBoxMinimumexperienceYear.Text);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                expClear.Visibility = Visibility.Visible;
                unEnableSecondLine();
                comboBoxCarType.SelectedItem = null;
                ComboBoxGender.SelectedItem = null;
            }
            else expClear.Visibility = Visibility.Hidden;
            filter(); // //this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.exp >= num);
        }

        // reset the selection of min experience text boxx
        private void ExpClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxMinimumexperienceYear.Text = "";
            expClear.Visibility = Visibility.Hidden;
        }

        // called when user clicked of "find close testers" option
        private void find_close_testers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            unEnableFirstLine();    // user can not filter by both "find close testers" and first line filtering option
            unEnableAvailable();    // user can not filter by both "find close testers" and "find available testers"

            houseNumberTextBox.IsEnabled = true;
            cityTextBox.IsEnabled = true;
            streetTextBox.IsEnabled = true;
            radiusTextBox.IsEnabled = true;
            closeButton.IsEnabled = true;
            streetLable.Foreground = Brushes.Black;
            houseLable.Foreground = Brushes.Black;
            cityLable.Foreground = Brushes.Black;
            radiusLable.Foreground = Brushes.Black;
        }

        // called when user pushed the button "Find" of "find close testers" option
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if ((streetTextBox.Text == "") || (houseNumberTextBox.Text == "") ||
                (cityTextBox.Text == "") || (radiusTextBox.Text == ""))
                MessageBox.Show("You must enter street, house numberm, city and radius first!");
            else
            {
                try
                {
                    address = new Address() // checking input validation
                    {
                        Street = streetTextBox.Text,
                        City = cityTextBox.Text,
                        HouseNumber = int.Parse(houseNumberTextBox.Text)
                    };

                    radius = int.Parse(radiusTextBox.Text);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                filter(); // filter by distance 
            }
        }

        // called when user clicked of "find available testers" option
        private void LabelDate__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            unEnableFirstLine();
            unEnableClose();
            datePicker.IsEnabled = true;
            comboBoxHours.IsEnabled = true;
            availableButton.IsEnabled = true;
            hourLable.Foreground = Brushes.Black;
        }

        // called when user pushed the button "Find" of "find available testers" option
        private void AvailableButton_Click(object sender, RoutedEventArgs e)
        {
            if ((datePicker.SelectedDate == null) || (comboBoxHours.SelectedItem == null))
                MessageBox.Show("You must choose a date and hour first!");
            
            else filter(); // find available testers;
        }

        private void unEnableFirstLine()
        {
            comboBoxCarType.SelectedItem = null;
            ComboBoxGender.SelectedItem = null;
            ComboBoxWorkerType.SelectedItem = null;
            TextBoxMinimumexperienceYear.Text = "";
        }

        private void unEnableSecondLine()
        {
            unEnableClose();
            unEnableAvailable();
        }

        // unEnabling the fields of  "find close testers" option
        private void unEnableClose()
        {
            streetLable.Foreground = Brushes.Gray;
            streetTextBox.Text = "";
            houseLable.Foreground = Brushes.Gray;
            houseNumberTextBox.Text = "";
            cityLable.Foreground = Brushes.Gray;
            cityTextBox.Text = "";
            radiusLable.Foreground = Brushes.Gray;
            radiusTextBox.Text = "";
            closeButton.IsEnabled = false;
        }

        // unEnabling the fields of  "find available testers" option
        private void unEnableAvailable()
        {
            datePicker.IsEnabled = false;
            hourLable.Foreground = Brushes.Gray;
            comboBoxHours.SelectedItem = null;
            availableButton.IsEnabled = false;
        }

        // refreshing the list accorrding to user's filtering chooses
        private void filter()
        {
            try
            {
                if (streetTextBox.Text != "") //user wants to filter by "find close testers" option
                {
                    this.TesterDataGrid.ItemsSource = bl.getCloseTesters(address, radius);
                    return;
                }

                if (comboBoxHours.SelectedItem != null) //user wants to filter by "find available testers" option
                {
                    DateTime tmp = ((DateTime)this.datePicker.SelectedDate).DeepClone(); 
                    if (tmp.DayOfWeek == DayOfWeek.Friday || tmp.DayOfWeek == DayOfWeek.Saturday)
                        throw new Exception("there are no testers who work on Friday or Saturday!");
                    string str = comboBoxHours.SelectedItem.ToString();
                    int wantedTime = ((str[0] - 48) * 10 + (str[1]) - 48);           //to get the hour from string
                    DateTime wantedDate = new DateTime(tmp.Year, tmp.Month, tmp.Day, wantedTime, 0, 0);

                    this.TesterDataGrid.ItemsSource = bl.getAvailableTesters(wantedDate);
                    return;
                }

                bool car = comboBoxCarType.SelectedItem != null ? true : false;
                bool gender = ComboBoxGender.SelectedItem != null ? true : false;
                bool worker = ComboBoxWorkerType.SelectedItem != null ? true : false;
                bool exp = TextBoxMinimumexperienceYear.Text != "" ? true : false;

                if (car || gender)
                {
                    if (car && !gender)      //user wants to filter by car type
                    {
                        CarType c = (CarType)comboBoxCarType.SelectedItem;
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.CarTypeTester == c);
                        return;
                    }
                    if (gender && !car)        //user wants to filter by gender
                    {
                        Gender g = (Gender)ComboBoxGender.SelectedItem;
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.GenderTester == g);
                        return;
                    }
                    if (car && gender)         //user wants to filter by both car type and gender
                    {
                        CarType c = (CarType)comboBoxCarType.SelectedItem;
                        Gender g = (Gender)ComboBoxGender.SelectedItem;
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => (t.CarTypeTester == c) && (t.GenderTester == g));
                        return;
                    }
                }

                if (worker || exp)
                {
                    if (worker && !exp)      //user wants to filter by worker type
                    {
                        WorkerType w = (WorkerType)ComboBoxWorkerType.SelectedItem;
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.WorkerType == w);
                        return;
                    }
                    if (exp && !worker)      //user wants to filter by minimum experience years
                    {
                        int e = int.Parse(TextBoxMinimumexperienceYear.Text);
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => t.ExperienceYears >= e);
                        return;
                    }
                    if (exp && worker)      //user wants to filter by both worker type and minimum experience years
                    {
                        WorkerType w = (WorkerType)ComboBoxWorkerType.SelectedItem;
                        int e = int.Parse(TextBoxMinimumexperienceYear.Text);
                        this.TesterDataGrid.ItemsSource = bl.getTestersList(t => (t.ExperienceYears >= e) && (t.WorkerType == w));

                        return;
                    }
                }

                this.TesterDataGrid.ItemsSource = bl.getTestersList();      // if no choose has been made- call the original list
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
