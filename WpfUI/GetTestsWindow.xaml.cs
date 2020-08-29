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
    /// Interaction logic for GetTestsWindow.xaml
    /// </summary>
    public partial class GetTestsWindow : Window
    {
        BL.IBL bl;
        public GetTestsWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            this.TestsDataGrid.ItemsSource = bl.getTestsList();

            comboBoxCarType.ItemsSource = Enum.GetValues(typeof(CarType));
        }

        private void more_details_click(object sender, RoutedEventArgs e)
        {
            Test obj = this.TestsDataGrid.SelectedItem as Test;
            if (obj != null)
            {
                MessageBox.Show($"Datails of Test: \n{obj}", "Test's Datails", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        //user sign check box available for handicapped as true
        private void CheckBoxIsHandicapped_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxIsHandicapped.IsChecked != false)
            {
                handicappedClear.Visibility = Visibility.Visible;
                unEnablePlannedTests();
            }
            else
                handicappedClear.Visibility = Visibility.Hidden;
            filter();
        }

        //for the whole list
        private void handicappedClear_Click(object sender, RoutedEventArgs e)
        {
            CheckBoxIsHandicapped.IsChecked = false;
            handicappedClear.Visibility = Visibility.Hidden;
            filter();
        }

       
        private void comboBoxCarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCarType.SelectedItem != null)
            {
                carClear.Visibility = Visibility.Visible;
                unEnablePlannedTests();
            }
            else carClear.Visibility = Visibility.Hidden;
            filter(); //this.TestsDataGrid.ItemsSource = bl.getTestsList(t => t.CarType == this car type);
        }

        private void CarClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCarType.SelectedItem = null;
            carClear.Visibility = Visibility.Hidden;
        }

        private void unEnablePlannedTests()
        {
            FindSpesipicTests.Foreground = Brushes.Gray;
            datePicker.IsEnabled = false;
            findButton.IsEnabled = false;
            radioDay.IsEnabled = false;
            radioMonth.IsEnabled = false;
            radioDay.Foreground = Brushes.Gray;
            radioMonth.Foreground = Brushes.Gray;
        }

        private void FindSpesipicTests__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FindSpesipicTests.Foreground = Brushes.Black;
            datePicker.IsEnabled = true;           
            findButton.IsEnabled = true;
            comboBoxCarType.SelectedItem = null;
            CheckBoxIsHandicapped.IsChecked = false;
            radioDay.IsEnabled = true;
            radioDay.Foreground = Brushes.Black;
            radioMonth.IsEnabled = true;
            radioMonth.Foreground = Brushes.Black;
            handicappedClear.Visibility = Visibility.Hidden;
        }

        private void find_Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null || (radioMonth.IsChecked == false && radioDay.IsChecked == false))
                MessageBox.Show("You have to choose date and day/month first!");
            else filter();
        }

        private void filter()
        {
            if (datePicker.SelectedDate != null) //user wants to filter by planned tests
            {
                DateTime wantedDate = ((DateTime)this.datePicker.SelectedDate).DeepClone();
                DateChoice choise = radioDay.IsChecked == true ? DateChoice.day : DateChoice.month;

                this.TestsDataGrid.ItemsSource = bl.getPlannedTests(wantedDate, choise);
                return;
            }

            bool car = comboBoxCarType.SelectedItem != null ? true : false;
            bool isHendicapped = CheckBoxIsHandicapped.IsChecked != false ? true : false;
            if (car || isHendicapped)  //user wants to filter by car type or exessible for handicapped
            {
                if (car && !isHendicapped)
                {
                    CarType c = (CarType)comboBoxCarType.SelectedItem;
                    this.TestsDataGrid.ItemsSource = bl.getTestsList(t => t.carTypeTest == c);
                    return;
                }
                if (isHendicapped && !car)
                {
                    this.TestsDataGrid.ItemsSource = bl.getTestsList(t => t.IsAccessibleForHandicapped == true);
                    return;
                }
                if (car && isHendicapped)
                {
                    CarType c = (CarType)comboBoxCarType.SelectedItem;
                    this.TestsDataGrid.ItemsSource = bl.getTestsList(t => (t.carTypeTest == c) && (t.IsAccessibleForHandicapped == true));
                    return;
                }
            }
            this.TestsDataGrid.ItemsSource = bl.getTestsList();
        }
    }
}
