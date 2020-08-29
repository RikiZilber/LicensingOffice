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
    /// Interaction logic for deleteTraineeWindow.xaml
    /// </summary>
    public partial class deleteTraineeWindow : Window
    {
        BL.IBL bl;
        private List<string> errorMessages;
        public deleteTraineeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.FactoryBL.getBL();
            refreshData();
            errorMessages = new List<string>();
        }

        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            Trainee obj = this.TraineeDataGrid.SelectedItem as Trainee;
            if (obj != null)
            {
                MessageBoxResult res = MessageBox.Show($"Are you sure you want to delete this Trainee?", "Remove Trainee", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool b = bl.deleteTrainee(obj);
                        if (!b) MessageBox.Show("Trainee has not been deleted!", "Delete trainee", MessageBoxButton.OK, MessageBoxImage.Information);
                        refreshData(); //TraineeDataGrid.ItemsSource = bl.getTraineesList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void refreshData()
        {
            try
            {
                this.TraineeDataGrid.ItemsSource = bl.getTraineesList();
            }
            catch
            {
                this.Close();
            }
        }

        private void more_details_click(object sender, RoutedEventArgs e)
        {
            Trainee obj = this.TraineeDataGrid.SelectedItem as Trainee;
            if (obj != null)
            {
                MessageBox.Show($"Datails of Trainee: \n{obj}", "Trainee's Datails", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

