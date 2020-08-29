using BE;
using System.Diagnostics;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        Trainee trainee;
        BL.IBL bl;
        private List<string> errorMessages;
        BackgroundWorker processingWorker;

        public AddTestWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            processingWorker = new BackgroundWorker();
            processingWorker.DoWork += new DoWorkEventHandler(Processing_DoWork);
            processingWorker.ProgressChanged += new ProgressChangedEventHandler(ProcessingWorker_ProgressChanged);
            processingWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessingWorker_RunWorkerCompleted);
            processingWorker.WorkerReportsProgress = true;
            processingWorker.WorkerSupportsCancellation = true;

            trainee = new Trainee(); 

            bl = BL.FactoryBL.getBL();

            this.testDateTimeDatePicker.DisplayDateStart = DateTime.Now.AddDays(1); 
            this.testDateTimeDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            string[] hours = new string[6] { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00" };
            this.comboBoxHours.ItemsSource = hours;

            this.idCombobox.ItemsSource = bl.getTraineesList();
            this.idCombobox.DisplayMemberPath = "Id";
            this.idCombobox.SelectedValuePath = "Id";

            errorMessages = new List<string>();
        }

        private void Processing_DoWork(object sender, DoWorkEventArgs e)
        {
            //while (!processingWorker.CancellationPending)
            //{
            //    processingWorker.ReportProgress(1);
            //    System.Threading.Thread.Sleep(100);
            //    if (processingWorker.CancellationPending) break;
            //}

            //while (!processingWorker.CancellationPending)

            for (int i = 0; i < 200; i++)
            {
                if (!processingWorker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(100);
                    processingWorker.ReportProgress(i);
                    if (processingWorker.CancellationPending) break;
                }
            }
            //e.Result = "result";
        }

        private void ProcessingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (processingWorker.CancellationPending)
                return;
            //MessageBox.Show("!");

            processLable.Visibility = Visibility.Visible;
            elip1.Visibility = Visibility.Visible;
            elip2.Visibility = Visibility.Visible;
            elip3.Visibility = Visibility.Visible;

            elip1.Height *= 2;
            elip1.Width *= 2;
            System.Threading.Thread.Sleep(200);
            elip1.Height *= 0.5;
            elip1.Width *= 0.5;

            elip2.Height *= 2;
            elip2.Width *= 2;
            System.Threading.Thread.Sleep(200);
            elip2.Height *= 0.5;
            elip2.Width *= 0.5;

            elip3.Height *= 2;
            elip3.Width *= 2;
            System.Threading.Thread.Sleep(200);
            elip3.Height *= 0.5;
            elip3.Width *= 0.5;
        }

        private void ProcessingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("end");
            processLable.Visibility = Visibility.Hidden;
            elip1.Visibility = Visibility.Hidden;
            elip2.Visibility = Visibility.Hidden;
            elip3.Visibility = Visibility.Hidden;
        }


        private void IdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idCombobox.SelectedItem is Trainee)
            {
                this.trainee = ((Trainee)this.idCombobox.SelectedItem).DeepClone();
                this.testDetailsGrid.DataContext = trainee;
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (processingWorker.IsBusy != true)
                processingWorker.RunWorkerAsync();

            if (idCombobox.SelectedItem == null)
            {
                if (processingWorker.IsBusy) processingWorker.CancelAsync();
                MessageBox.Show("You have to choose trainee first!");
                return;
            }

            if (comboBoxHours.SelectedItem == null)
            {
                if (processingWorker.IsBusy) processingWorker.CancelAsync();
                MessageBox.Show("You have to choose hour first!");
                return;
            }

            try
            {
                if (errorMessages.Any()) //errorMessages.Count > 0 
                {
                    string err = "Exception:";
                    foreach (var item in errorMessages)
                        err += "\n" + item;

                    if (processingWorker.IsBusy) processingWorker.CancelAsync();
                    MessageBox.Show(err);
                    return;
                }
                else
                {
                    this.trainee = ((Trainee)this.idCombobox.SelectedItem).DeepClone();

                    DateTime tmp = ((DateTime)this.testDateTimeDatePicker.SelectedDate).DeepClone();
                    if (tmp.DayOfWeek == DayOfWeek.Friday || tmp.DayOfWeek == DayOfWeek.Saturday)
                      throw new Exception  ("You can not set a test on friday or saturday!");

                    string str = comboBoxHours.SelectedItem.ToString();
                    int wantedTime = ((str[0]-48)*10 + (str[1])-48);           //to get the hour from string
                    DateTime wantedDate = new DateTime(tmp.Year, tmp.Month, tmp.Day, wantedTime, 0, 0);

                    if (!processingWorker.IsBusy)
                        processingWorker.RunWorkerAsync();

                    bl.addTest(trainee, wantedDate);

                    if (processingWorker.IsBusy) processingWorker.CancelAsync();

                    MessageBox.Show("Test for trainee " + trainee.FirstName + " was successfully added!", "Test added", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.testDetailsGrid.DataContext = trainee = null;
                    this.testDateTimeDatePicker.SelectedDate = DateTime.Now;
                    this.idCombobox.ItemsSource = bl.getTraineesList();

                    //this.Close(); 
                }

            }
            catch (Exception exp)
            {
                if (processingWorker.IsBusy) processingWorker.CancelAsync();
                MessageBox.Show(exp.Message);
                if (processingWorker.IsBusy) processingWorker.CancelAsync();
            }
        }
    }
}
