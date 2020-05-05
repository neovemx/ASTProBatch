using AST_ProBatch_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public HomeViewModel Home { get; set; }
        public SettingsViewModel Settings { get; set; }
        public StatisticalReportsMenuViewModel StatisticalReportsMenu { get; set; }
        public AboutViewModel About { get; set; }
        public CommandBatchViewModel CommandBatch { get; set; }
        public LogExecutionDelayViewModel LogExecutionDelay { get; set; }
        public LogExecutionViewModel LogExecution { get; set; }
        public PlannerMenuViewModel PlannerMenu { get; set; }
        public MonitoringAndExecutionMenuViewModel MonitoringAndExecutionMenu { get; set; }
        public OperationsLogViewModel OperationsLog { get; set; }
        public ProcessMonitorViewModel ProcessMonitor { get; set; }
        public RecurrenceMonitorViewModel RecurrenceMonitor { get; set; }
        public StatusInfoViewModel StatusInfo { get; set; }
        public NotificationsViewModel Notifications { get; set; }
        public InstanceNotificationsViewModel InstanceNotifications { get; set; }
        public MonitoringViewModel Monitoring { get; set; }
        public ExecutionViewModel Execution { get; set; }
        public ExecutionStageTwoViewModel ExecutionStageTwo { get; set; }
        public ExecutionStageThreeViewModel ExecutionStageThree { get; set; }
        public CommandNotificationsViewModel CommandNotifications { get; set; }
        public LogObservationsViewModel LogObservations { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.Home = new HomeViewModel();
            this.Settings = new SettingsViewModel();
            this.StatisticalReportsMenu = new StatisticalReportsMenuViewModel();
            this.About = new AboutViewModel();
            this.CommandBatch = new CommandBatchViewModel();
            this.LogExecutionDelay = new LogExecutionDelayViewModel();
            this.LogExecution = new LogExecutionViewModel();
            this.PlannerMenu = new PlannerMenuViewModel();
            this.MonitoringAndExecutionMenu = new MonitoringAndExecutionMenuViewModel();
            this.OperationsLog = new OperationsLogViewModel();
            this.ProcessMonitor = new ProcessMonitorViewModel();
            this.RecurrenceMonitor = new RecurrenceMonitorViewModel();
            this.StatusInfo = new StatusInfoViewModel();
            this.Notifications = new NotificationsViewModel(new Models.LogItem());
            this.InstanceNotifications = new InstanceNotificationsViewModel(new Models.InstanceItem());
            this.Monitoring = new MonitoringViewModel();
            this.Execution = new ExecutionViewModel();
            this.ExecutionStageTwo = new ExecutionStageTwoViewModel(new Models.LogItem());
            this.ExecutionStageThree = new ExecutionStageThreeViewModel(new Models.InstanceItem());
            this.CommandNotifications = new CommandNotificationsViewModel(new Models.CommandItem());
            this.LogObservations = new LogObservationsViewModel(new Models.LogItem());
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
