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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DNMReport.Model;
using DNMReport.Controller;
using DNMReport.Logfile;

namespace DNMReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Jobs MainJobs = new Jobs();
        public List<JobsDisplay> ListJobs = new List<JobsDisplay>();

        public MainWindow()
        {
            InitializeComponent();
            ControllerJobs jobs = new ControllerJobs();
            MainJobs = jobs.GetJobs();
        }

        private void dtgv_schedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dtgv_schedule_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNum_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtNum1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtgv_schedule_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdUp1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdUp2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDown2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDown1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtNum2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Start program", Title);
               JobsDisplay jobsDisplay = new JobsDisplay();
            jobsDisplay.JobId = MainJobs.ID;
            jobsDisplay.TaskId = MainJobs.Task.Id;
            jobsDisplay.ScheduleId = MainJobs.Schedule.ID;
            jobsDisplay.TimeRun = MainJobs.Schedule.TimeRun;
            ListJobs.Add(jobsDisplay);
            dtgv_schedule.ItemsSource = ListJobs;

        }

        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {
            ControllerJMP controllerJMP = new ControllerJMP();
            controllerJMP.RunJslFileButton(MainJobs.Task.Direction+"\\"+MainJobs.Task.Name);
        }

        private void btn_JMP_Click(object sender, RoutedEventArgs e)
        {
            ControllerJMP controllerJMP = new ControllerJMP();
            controllerJMP.TestJMPFile(StaticConfigurationcs.JMPfolderInitial + "Rerun X.jmp");
        }
    }
}
