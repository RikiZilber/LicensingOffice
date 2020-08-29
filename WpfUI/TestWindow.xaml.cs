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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Add_Test_Button_Click(object sender, RoutedEventArgs e)
        {
            new AddTestWindow().ShowDialog();
        }

        private void Get_Test_Button_Click(object sender, RoutedEventArgs e)
        {
            new GetTestsWindow().ShowDialog();
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTestWindow().ShowDialog();
        }
    }
}
