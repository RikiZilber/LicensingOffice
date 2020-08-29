using System;
using System.Collections.Generic;
using BE;
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
    /// Interaction logic for deleteTesterWindow.xaml
    /// </summary>
    public partial class deleteTesterWindow : Window
    {
        BL.IBL bl;
        private List<string> errorMessages;

        public deleteTesterWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.FactoryBL.getBL();
            refreshData();
            errorMessages = new List<string>();
        }

        private void delete_Tester_click(object sender, RoutedEventArgs e)
        {
            Tester obj = this.TesterDataGrid.SelectedItem as Tester;
            if (obj != null)
            {
                MessageBoxResult res = MessageBox.Show($"Are you sure you want to delete this Tester?", "Remove Tester", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool b = bl.deleteTester(obj);
                        //oTesterDataGrid.Items.Refresh();
                        this.TesterDataGrid.ItemsSource = bl.getTestersList();

                        //foreach (var item in bl.getTestersList())
                        //{
                        //    MessageBox.Show(item.ToString());
                        //}

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
                this.TesterDataGrid.ItemsSource = bl.getTestersList();
            }
            catch
            {
                this.Close();
            }
        }

        private void more_details_click(object sender, RoutedEventArgs e)
        {
            Tester obj = this.TesterDataGrid.SelectedItem as Tester;
            if (obj != null)
            {
                MessageBox.Show($"Datails of Tester: \n{obj}", "Tester's Datails", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

