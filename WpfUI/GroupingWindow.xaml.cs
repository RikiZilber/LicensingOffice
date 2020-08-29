using BE;
using BL;
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
    /// Interaction logic for GroupingWindow.xaml
    /// </summary>
    public partial class GroupingWindow : Window
    {
        IBL bl;
        public GroupingWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bl = BL.FactoryBL.getBL();

            List<string> list = new List<string>();
            foreach (var item in bl.getTraineesList())
            {
                list.Add(item.SchoolName);
            }

            list = list.Distinct().ToList();

            this.SchoolNameComboBox.ItemsSource = list;
                //bl.getTraineesList().Distinct();
            //this.SchoolNameComboBox.DisplayMemberPath = "SchoolName";
            //this.SchoolNameComboBox.SelectedValuePath = "SchoolName";

            //this.traineeIdComboBox.ItemsSource = bl.getTraineesList();
            //this.traineeIdComboBox.DisplayMemberPath = "FirstName";
            //this.traineeIdComboBox.SelectedValuePath = "Id";

        }

        //private string GetSelectedTraineeId()
        //{
        //    object result = this.SchoolNameComboBox.SelectedValue;

        //    if (result == null)
        //        throw new Exception("must select Trainee Id First");
        //    return (string)result;
        //}

        private string GetSelectedSchoolName()
        {
            object result = this.SchoolNameComboBox.SelectedValue;

            if (result == null)
                throw new Exception("must select School Name First");
            return (string)result;
        }

        private void AllTestersExperience_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupTesterExperienceUserControl uc = new GroupTesterExperienceUserControl();
                uc.Source = bl.GetAllTestersGroupByExpertise(true);
                this.page.Content = uc;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AllTraineesForTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupTraineesByTesterrUserControl uc = new GroupTraineesByTesterrUserControl();
                uc.Source = bl.GetAllTraineesGroupByTester(true);
                this.page.Content = uc;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AllTraineesForSchoolName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string schoolName = GetSelectedSchoolName();
                GroupTraineeBySchoolUserControl uc = new GroupTraineeBySchoolUserControl();
                uc.Source = bl.GetAllTraineesGroupBySchoolName(true, schoolName);
                this.page.Content = uc;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void AllTraineesForTestNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupingTraineeByTestNumUserControl uc = new GroupingTraineeByTestNumUserControl();
                uc.Source = bl.GetAllTraineessGroupByTestsNum(true);
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
