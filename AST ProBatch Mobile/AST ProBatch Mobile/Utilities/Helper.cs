using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
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
        public static string Black { get { return "Black"; } }
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
        public const string PBMenuBTest = "/pbauth";
        public const string PBAuthApiAuth = "/pbauth/apiauth";
        public const string PBAuthAuthentication = "/pbauth/probatchauth";
        public const string PBMenuBApiAuth = "/pbmenub/apiauth";
        public const string PBMenuBExecute = "/pbmenub/probatchmonitoringandexecution";
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
        public const string GetObservations = "/getobservationsbyloganduser";
        public const string AddObservation = "/addobservationsbyloganduser";
        public const string ModObservation = "/modobservationsbyloganduser";
        public const string DelObservation = "/delobservations";
        public const string ControlSchedulesExecution = "/getControlschedulesexecution";
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
