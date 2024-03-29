﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;

namespace AST_ProBatch_Mobile.Utilities
{
    public class Alert
    {
        public static async void Show(string message)
        {
            await UserDialogs.Instance.AlertAsync(message, "AST●ProBatch®", "Cerrar");
        }

        public static async void Show(string message, string buttontext)
        {
            await UserDialogs.Instance.AlertAsync(message, "AST●ProBatch®", buttontext);
        }
    }

    public class Confirm
    {
        public static Task<bool> Show(string message)
        {
            return UserDialogs.Instance.ConfirmAsync(message, "AST●ProBatch®", "Si", "No");
        }

        public static Task<bool> Show(string message, string okbuttonmessage, string cancelbuttonmessage)
        {
            return UserDialogs.Instance.ConfirmAsync(message, "AST●ProBatch®", okbuttonmessage, cancelbuttonmessage);
        }
    }

    public class GetStatusColor
    {
        public static string ByIdStatus(string IdStatus)
        {
            string resultColor = string.Empty;
            switch (IdStatus)
            {
                case Status.INITIAL:
                    resultColor = StatusColor.White;
                    break;
                case Status.ERROR:
                    resultColor = StatusColor.Red;
                    break;
                case Status.INTERFAZ_ERROR:
                    resultColor = StatusColor.Red;
                    break;
                case Status.INTERFAZ_WAITING:
                    resultColor = StatusColor.Yellow;
                    break;
                case Status.DEPENDENCY_ERROR:
                    resultColor = StatusColor.Red;
                    break;
                case Status.WAITING_DEPENDENCY:
                    resultColor = StatusColor.Yellow;
                    break;
                case Status.CONNECTION_ERROR:
                    resultColor = StatusColor.Red;
                    break;
                case Status.STOP:
                    resultColor = StatusColor.Orange;
                    break;
                case Status.SUCCESS:
                    resultColor = StatusColor.Blue;
                    break;
                case Status.WAITING_RESOURCE:
                    resultColor = StatusColor.Yellow;
                    break;
                case Status.EXECUTING:
                    resultColor = StatusColor.Green;
                    break;
                case Status.MESSAGE:
                    resultColor = StatusColor.Yellow;
                    break;
                case Status.KILLED:
                    resultColor = StatusColor.Purple;
                    break;
                default:
                    resultColor = StatusColor.Black;
                    break;
            }
            return resultColor;
        }
    }

    public class GetExecutionStatus
    {
        public static StatusItem ByIdStatus(string IdStatus)
        {
            StatusItem resultStatusObject = new StatusItem();
            switch (IdStatus)
            {
                case Status.INITIAL:
                    resultStatusObject.State = "state_p";
                    resultStatusObject.StateColor = StatusColor.Grey;
                    break;
                case Status.EXECUTING:
                    resultStatusObject.State = "state_e";
                    resultStatusObject.StateColor = StatusColor.Green;
                    break;
                case Status.FINISHED:
                    resultStatusObject.State = "state_f";
                    resultStatusObject.StateColor = StatusColor.Blue;
                    break;
                case Status.WAITING_DEPENDENCY:
                    resultStatusObject.State = "state_ed";
                    resultStatusObject.StateColor = StatusColor.Blue;
                    break;
                case Status.INTERFAZ_WAITING:
                    resultStatusObject.State = "state_ei";
                    resultStatusObject.StateColor = StatusColor.Yellow;
                    break;
                case Status.WAITING_RESOURCE:
                    resultStatusObject.State = "state_er";
                    resultStatusObject.StateColor = StatusColor.Yellow;
                    break;
                case Status.WAITING_SOCKET:
                    resultStatusObject.State = "state_ep";
                    resultStatusObject.StateColor = StatusColor.Yellow;
                    break;
                case Status.RESOURCE_ERROR:
                    resultStatusObject.State = "state_er";
                    resultStatusObject.StateColor = StatusColor.Red;
                    break;
                case Status.ERROR:
                    resultStatusObject.State = "state_e";
                    resultStatusObject.StateColor = StatusColor.Red;
                    break;
                case Status.DEPENDENCY_ERROR:
                    resultStatusObject.State = "state_ed";
                    resultStatusObject.StateColor = StatusColor.Orange;
                    break;
                case Status.INTERFAZ_ERROR:
                    resultStatusObject.State = "state_ei";
                    resultStatusObject.StateColor = StatusColor.Red;
                    break;
                case Status.SOCKET_ERROR:
                    resultStatusObject.State = "state_ep";
                    resultStatusObject.StateColor = StatusColor.Red;
                    break;
                case Status.STOP:
                    resultStatusObject.State = "state_pause";
                    resultStatusObject.StateColor = StatusColor.White;
                    break;
                case Status.KILLED:
                    resultStatusObject.State = "state_ab";
                    resultStatusObject.StateColor = StatusColor.Purple;
                    break;
                case Status.OMITTED:
                    resultStatusObject.State = "state_om";
                    resultStatusObject.StateColor = StatusColor.White;
                    break;
                case Status.SUCCESS:
                    resultStatusObject.State = "state_f";
                    resultStatusObject.StateColor = StatusColor.Blue;
                    break;
                default:
                    resultStatusObject.State = "";
                    resultStatusObject.StateColor = StatusColor.Black;
                    break;
            }
            return resultStatusObject;
        }
    }

    public static class StatusColor
    {
        public static string Green { get { return "#33CC33"; } }
        public static string Red { get { return "#FE6347"; } }
        public static string Orange { get { return "#FDA600"; } }
        public static string Yellow { get { return "#FFFF00"; } }
        public static string White { get { return "#FFFFFF"; } }
        public static string Grey { get { return "#7F7F7F"; } }
        public static string Purple { get { return "#B9908E"; } }
        public static string Blue { get { return "#87CEFA"; } }
        public static string Black { get { return "#000000"; } }
        public static string DarkBlue { get { return "#070471"; } }
    }

    public static class Answers
    {
        public static string Yes { get { return "SI"; } }
        public static string No { get { return "NO"; } }
    }

    public static class AlertMessages
    {
        public static string UserInvalid { get { return "Credenciales inválidas!"; } }
        public static string Error { get { return "Ocurrió un error!"; } }
        public static string Success { get { return "Operación completada exitosamente!"; } }
        public static string Existing { get { return "Elemento existente!"; } }
        public static string NoSuccess { get { return "No se pudo completar la operación!"; } }
        public static string Adding { get { return "Agregando nuevo elemento..."; } }
        public static string Updating { get { return "Actualizando elemento seleccionado..."; } }
        public static string Deleting { get { return "Eliminando elemento seleccionado..."; } }
        public static string UpdatingList { get { return "Actualizando lista de elementos..."; } }
        public static string Delete { get { return "Debe seleccionar un elemento para eliminar!"; } }
    }

    public static class BarItemColor
    {
        public static string Base { get { return "#D7D7D7"; } }
        public static string HighLight { get { return "#87CEFA"; } }
    }

    public static class ApiConsult
    {
        public const string ApiAuth = "PBAUTH";
        public const string ApiMenuA = "PBMENUA";
        public const string ApiMenuB = "PBMENUB";
        public const string ApiMenuC = "PBMENUC";
        public const string ApiMenuD = "PBMENUD";
    }

    public static class ApiController
    {
        public const string PBAuthTest = "/pbauth";
        public const string PBMenuATest = "/pbmenua";
        public const string PBMenuBTest = "/pbmenub";
        public const string PBMenuCTest = "/pbmenuc";
        public const string PBMenuDTest = "/pbmenud";
        public const string PBAuthApiAuth = "/pbauth/apiauth";
        public const string PBAuthAuthentication = "/pbauth/probatchauth";
        public const string PBMenuAApiAuth = "/pbmenua/apiauth";
        public const string PBMenuBApiAuth = "/pbmenub/apiauth";
        public const string PBMenuCApiAuth = "/pbmenuc/apiauth";
        public const string PBMenuDApiAuth = "/pbmenud/apiauth";
        public const string PBMenuA = "/pbmenua/probatchstatisticalreports";
        public const string PBMenuB = "/pbmenub/probatchmonitoringandexecution";
        public const string PBMenuC = "/pbmenuc/probatchoperationslog";
        public const string PBMenuD = "/pbmenud/probatchplanner";
    }

    public static class ApiMethod
    {
        public const string Test = "/auth";
        public const string Login = "/login";
        public const string GetLogs = "/getlogs";
        public const string GetInstancesByLogAndUser = "/getinstances";
        public const string GetCommandsByInstance = "/getcommands";
        public const string GetOperatorChangeUsers = "/getoperatorchangeusers";
        public const string GetOperatorChangeInstances = "/getoperatorchangeinstances";
        public const string GetOperatorChangeUserIsInAllInstances = "/getoperatorchangeuserisinallinstances";
        public const string GetOperatorChange = "/getoperatorchange";
        public const string GetObservations = "/getobservations";
        public const string AddObservation = "/addobservations";
        public const string ModObservation = "/modobservations";
        public const string DelObservation = "/delobservations";
        public const string ControlSchedulesExecution = "/getcontrolschedulesexecution";
        public const string ExecutionResultGet = "/executionresultget";
        public const string ExecutionResultErrorLogGet = "/executionresulterrorlogget";
        public const string LogInquirieGetLots = "/loginquiriesgetlots";
        public const string LogInquirieGetCommands = "/loginquiriesgetcommands";
        public const string LogInquirieGetOperators = "/loginquiriesgetoperators";
        public const string LogInquirieGetLogs = "/loginquiriesgetlogs";
        public const string DependenciesGetLotsThatDepends = "/dependenciesgetlotsthatdepends";
        public const string DependenciesGetDependentLotDetail = "/dependenciesgetdependentlotdetail";
        public const string DependenciesGetCommandsThatDepends = "/dependenciesgetcommandsthatdepends";
        public const string DependenciesGetDependentCommandDetail = "/dependenciesgetdependentcommanddetail";
        public const string DependenciesGetResource = "/dependenciesgetresource";
        public const string CommandDataGetGeneral = "/commanddatagetgeneral";
        public const string CommandDataGetCommand = "/commanddatagetcommand";
        public const string CommandDataGetCommandProcess = "/commanddatagetcommandprocess";
        public const string CommandDataGetTransfer = "/commanddatagettransfer";
        public const string CommandDataGetTransferOriginActions = "/commanddatagettransferoriginactions";
        public const string CommandDataGetTransferDestination = "/commanddatagettransferdestination";
        public const string CommandDataGetTransferDestinationActions = "/commanddatagettransferdestinationactions";
        public const string CommandDataGetExecution = "/commanddatagetexecution";
        public const string CommandDataGetObservation = "/commanddatagetobservation";
        public const string CommandDataGetInterface = "/commanddatagetinterface";
        public const string CommandDataGetExecutionHistory = "/commanddatagetexecutionhistory";
        public const string BatchQueryGetLots = "/batchquerygetlots";
        public const string BatchQueryGetParameters = "/batchquerygetparameters";
        public const string BatchQueryGetCommands = "/batchquerygetcommands";
        public const string BatchQueryGetCalendars = "/batchquerygetcalendars";
        public const string LotAndCommandGetTemplates = "/lotandcommandgettemplate";
        public const string LotAndCommandGetLogs = "/lotandcommandgetlogs";
        public const string LotAndCommandGetLots = "/lotandcommandgetlots";
        public const string LotAndCommandGetCommands = "/lotandcommandgetcommands";
        public const string LotAndCommandGetResults = "/lotandcommandgetresults";
        public const string LogExecutionDelayGetTemplates = "/logexecutiondelaygettemplates";
        public const string LogExecutionDelayGetEvents = "/logexecutiondelaygetevents";
        public const string LogExecutionDelayGetLots = "/logexecutiondelaygetlots";
        public const string LogExecutionDelayGetCommands = "/logexecutiondelaygetcommands";
        public const string LogExecutionDelayGetResults = "/logexecutiondelaygetresults";
        public const string LogExecutionGetTemplates = "/logexecutiongettemplates";
        public const string LogExecutionGetResults = "/logexecutiongetresults";
        public const string OperationsLogGetLogs = "/operationsloggetlogs";
        public const string OperationsLogGetUsers = "/operationsloggetusers";
        public const string OperationsLogGetStatuses = "/operationsloggetstatuses";
        public const string OperationsLogGetEnvironments = "/operationsloggetenvironments";
        public const string OperationsLogGetServices = "/operationsloggetservices";
        public const string OperationsLogGetLots = "/operationsloggetlots";
        public const string OperationsLogGetCommands = "/operationsloggetcommands";
        public const string OperationsLogGetResults = "/operationsloggetresults";
        public const string PlannerMonitorsGetData = "/monitorgetdata";
    }

    public static class TokenType
    {
        public const string Scheme = "Bearer";
    }

    public static class IconSet
    {
        public static string Notification { get { return "notification"; } }
        public static string NotificationPush { get { return "notification_n"; } }
    }

    public static class DateTimeFormatString
    {
        public static string LatinDate24Hours { get { return "dd/MM/yyyy HH:mm:ss"; } }
        public static string LatinDate12Hours { get { return "dd/MM/yyyy hh:mm:ss"; } }
        public static string AmericanDate24Hours { get { return "MM/dd/yyyy HH:mm:ss"; } }
        public static string AmericanDate12Hours { get { return "MM/dd/yyyy hh:mm:ss"; } }
        public static string LatinDate12HoursTTT { get { return "dd/MM/yyyy HH:mm:ss ttt"; } }
        public static string AmericanDate12HoursTTT { get { return "MM/dd/yyyy hh:mm:ss ttt"; } }
        public static string LatinDate { get { return "dd/MM/yyyy"; } }
        public static string AmericanDate { get { return "MM/dd/yyyy"; } }
        public static string Time12Hour { get { return "hh:mm:ss"; } }
        public static string Time24Hour { get { return "HH:mm:ss"; } }
    }

    public static class Status
    {
        public const string INITIAL = "";
        public const string ERROR = "E";
        public const string INTERFAZ_ERROR = "EIE";
        public const string INTERFAZ_WAITING = "WIE";
        public const string DEPENDENCY_ERROR = "D";
        public const string WAITING_DEPENDENCY = "W";
        public const string CONNECTION_ERROR = "EC";
        public const string STOP = "P";
        public const string SUCCESS = "S";
        public const string WAITING_RESOURCE = "Y";
        public const string EXECUTING = "X";
        public const string MESSAGE = "M";
        public const string KILLED = "K";
        public const string WAITING_SOCKET = "WS";
        public const string RESOURCE_ERROR = "EY";
        public const string SOCKET_ERROR = "ES";
        public const string OMITTED = "O";
        public const string FINISHED = "F";
    }

    public static class AddOrModResult
    {
        public const int OK = 1;
        public const int ERROR = 0;
        public const int EXISTING_DATA = 2;
    }

    public class AppHelper
    {
        public static bool UserIsSupervisor(PbUser pbUser)
        {
            bool result = false;
            foreach (PbUserProfile userProfile in pbUser.Profiles)
            {
                switch (userProfile.Profile)
                {
                    case "SUPERVISOR":
                        result = true;
                        break;
                }
            }
            return result;
        }
    }

    public class TokenValidator
    {
        public static bool IsValid(Token token)
        {
            bool result = false;
            if (token.Creation.Date == DateTime.Now.Date)
            {
                if (token.Creation.Hour == DateTime.Now.Hour)
                {
                    if ((token.Creation.Minute - DateTime.Now.Minute) <= token.ValidTime)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
