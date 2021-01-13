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
using System.Threading;

namespace DNMReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Jobs MainJobs = new Jobs();
        public List<JobsDisplay> ListJobs = new List<JobsDisplay>();
        EventBroker.EventObserver m_observerLog = null;
        EventBroker.EventObserver m_observerRunTask = null;
        EventBroker.EventParam m_timerEvent = null;
        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();

        public MainWindow()
        {
            InitializeComponent();

            m_observerLog = new EventBroker.EventObserver(OnReceiveLog);
            EventBroker.AddObserver(EventBroker.EventID.etLog, m_observerLog);
            

            m_observerRunTask = new EventBroker.EventObserver(OnTaskRun);
            EventBroker.AddObserver(EventBroker.EventID.etRunTask, m_observerRunTask);
            ControllerJobs jobs = new ControllerJobs();
            MainJobs = jobs.GetJobs();
          //  initlize_timer();
        }
        //private void initlize_timer()
        //{
        //    tmr.Interval = 1000;
        //    tmr.Enabled = false;
        //    tmr.Tick += Tmr_Tick;
        //    tmr.Start();

        //}

        //private void Tmr_Tick(object sender, EventArgs e)
        //{
        //    tmr.Stop();
           

        //    tmr.Start();
        //    tmr.Enabled = true;
        //}
        private void OnReceiveLog(EventBroker.EventID id, EventBroker.EventParam param)
        {
            if (param == null)
                return;
          
        }

      

       
        private void OnTaskRun(EventBroker.EventID id, EventBroker.EventParam param)
        {
            if (param == null)
                return;
            if (param.ParamString != null)
            {
              
            }
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
            if (m_timerEvent == null)
            {
                m_timerEvent = new EventBroker.EventParam(this, 0);
                EventBroker.AddTimeEvent(EventBroker.EventID.etRunTask, m_timerEvent, 1000, true);//66분에 한번씩
                //EventBroker.AddTimeEvent(EventBroker.EventID.etUpdateMe, m_timerEvent, 20000, true);//66분에 한번씩
            }

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
