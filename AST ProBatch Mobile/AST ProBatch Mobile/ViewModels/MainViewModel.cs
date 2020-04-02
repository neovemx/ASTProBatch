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
        public MonitoringAndExecutionViewModel MonitoringAndExecution { get; set; }
        public OperationsLogViewModel OperationsLog { get; set; }
        public ProcessMonitorViewModel ProcessMonitor { get; set; }
        public RecurrenceMonitorViewModel RecurrenceMonitor { get; set; }
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
            this.MonitoringAndExecution = new MonitoringAndExecutionViewModel();
            this.OperationsLog = new OperationsLogViewModel();
            this.ProcessMonitor = new ProcessMonitorViewModel();
            this.RecurrenceMonitor = new RecurrenceMonitorViewModel();
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
