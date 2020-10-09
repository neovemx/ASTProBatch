using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Services;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class MainViewModel
    {
        #region Properties
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
        public StatusInfoPlannerViewModel StatusInfoPlanner { get; set; }
        public NotificationsViewModel Notifications { get; set; }
        public InstanceNotificationsViewModel InstanceNotifications { get; set; }
        public MonitoringViewModel Monitoring { get; set; }
        public ExecutionViewModel Execution { get; set; }
        public ExecutionStageTwoViewModel ExecutionStageTwo { get; set; }
        public ExecutionStageThreeViewModel ExecutionStageThree { get; set; }
        public CommandNotificationsViewModel CommandNotifications { get; set; }
        public LogObservationsViewModel LogObservations { get; set; }
        public OperatorChangeViewModel OperatorChange { get; set; }
        public ControlSchedulesExecutionViewModel ControlSchedulesExecution { get; set; }
        public StatusInfoControlSchedulesExecutionViewModel StatusInfoControlSchedulesExecution { get; set; }
        public LogInquiriesViewModel LogInquiries { get; set; }
        public LogInquirieDetailViewModel LogInquirieDetail { get; set; }
        public ControlSchedulesExecutionDetailViewModel ControlSchedulesExecutionDetail { get; set; }
        public BatchQueryViewModel BatchQuery { get; set; }
        public EditParametersViewModel EditParameters { get; set; }
        public StatusInfoExecutionStageTwoViewModel StatusInfoExecutionStageTwo { get; set; }
        public StatusInfoExecutionStageThreeViewModel StatusInfoExecutionStageThree { get; set; }
        public StatusInfoLogInquiriesViewModel StatusInfoLogInquiries { get; set; }
        public DependenciesViewModel Dependencies { get; set; }
        public StatusInfoDependenciesViewModel StatusInfoDependencies { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            InitialLoad initialLoad = new InitialLoad();
            DataHelper dbHelper = new DataHelper();
            Table_Config table_Config = null;
            if (!dbHelper.CreateTablesAndInitialData())
            {
                initialLoad.IsSuccess = false;
            }
            else
            {
                initialLoad.IsSuccess = true;
                table_Config = dbHelper.GetAppConfig();
                if (table_Config != null)
                {
                    initialLoad.HasConfigData = true;
                }
                else
                {
                    initialLoad.HasConfigData = false;
                }
            }

            this.Login = new LoginViewModel(initialLoad, table_Config);
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
            this.StatusInfoPlanner = new StatusInfoPlannerViewModel();
            this.Notifications = new NotificationsViewModel(false, new Models.LogItem());
            this.InstanceNotifications = new InstanceNotificationsViewModel(new Models.InstanceItem());
            this.Monitoring = new MonitoringViewModel();
            this.Execution = new ExecutionViewModel(false);
            this.ExecutionStageTwo = new ExecutionStageTwoViewModel(false, new Models.LogItem());
            this.ExecutionStageThree = new ExecutionStageThreeViewModel(false, new Models.InstanceItem());
            this.CommandNotifications = new CommandNotificationsViewModel(new Models.CommandItem());
            this.LogObservations = new LogObservationsViewModel(false, new Models.LogItem());
            this.OperatorChange = new OperatorChangeViewModel(false, new Models.LogItem());
            this.ControlSchedulesExecution = new ControlSchedulesExecutionViewModel(false, new Models.LogItem());
            this.StatusInfoControlSchedulesExecution = new StatusInfoControlSchedulesExecutionViewModel();
            this.LogInquiries = new LogInquiriesViewModel(false, new Models.LogItem());
            this.LogInquirieDetail = new LogInquirieDetailViewModel(new Models.ResultLogInquirieItem());
            this.ControlSchedulesExecutionDetail = new ControlSchedulesExecutionDetailViewModel(new Models.CommandsToControl());
            this.BatchQuery = new BatchQueryViewModel(false, new Models.CommandItem());
            this.EditParameters = new EditParametersViewModel(new Models.CommandItem());
            this.StatusInfoExecutionStageTwo = new StatusInfoExecutionStageTwoViewModel();
            this.StatusInfoExecutionStageThree = new StatusInfoExecutionStageThreeViewModel();
            this.StatusInfoLogInquiries = new StatusInfoLogInquiriesViewModel();
            this.Dependencies = new DependenciesViewModel(false, new Models.CommandItem());
            this.StatusInfoDependencies = new StatusInfoDependenciesViewModel();
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
