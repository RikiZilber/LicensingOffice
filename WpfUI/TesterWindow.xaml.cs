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
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        public TesterWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Add_Tetser_Button_Click(object sender, RoutedEventArgs e)
        {
            new AddTesterWindow().ShowDialog();
        }

        private void Get_Testers_Button_Click(object sender, RoutedEventArgs e)
        {
            new GetTestersWiindow().ShowDialog();
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {
            new deleteTesterWindow().ShowDialog();
        }

        private void update_Button_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTesterWindow().ShowDialog();
        }
    }
}
