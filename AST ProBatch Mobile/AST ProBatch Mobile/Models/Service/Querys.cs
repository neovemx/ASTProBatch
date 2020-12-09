using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class InstanceQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public bool IsEventual { get; set; }
    }

    public class CommandQueryValues
    {
        public Int32 IdInstance { get; set; }
    }

    public class OperatorChangeUserQueryValues
    {
        public string CurrentIdUser { get; set; }
    }

    public class OperatorChangeInstanceQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public bool IsSupervisor { get; set; }
    }

    public class OperatorChangeUserIsInAllInstancesQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public string Instances { get; set; }
    }

    public class OperatorChangeQueryValues
    {
        public Int32 IdLog { get; set; }
        public string NewIdUser { get; set; }
        public string Instances { get; set; }
        public string OldIdUser { get; set; }
        public string StartDate { get; set; }
        public string ClientIp { get; set; }
    }

    public class ObservationGetQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class ObservationAddQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string NameObsv { get; set; }
        public string DetailObsv { get; set; }
    }

    public class ObservationModQueryValues
    {
        public Int32 IdObsv { get; set; }
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string NameObsv { get; set; }
        public string DetailObsv { get; set; }
    }

    public class ObservationDelQueryValues
    {
        public Int32 IdObsv { get; set; }
    }

    public class ControlSchedulesExecutionQueryValues
    {
        public Int32 IdLog { get; set; }
    }

    public class LogInquirieGetCommandsQueryValues
    {
        public Int32 IdLot { get; set; }
    }

    public class LogInquirieGetOperatorsQueryValues
    {
        public Int32 IdLog { get; set; }
    }

    public class LogInquirieGetLogsQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class DependenciesGetLotsThatDependsQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
    }

    public class DependenciesGetDependentLotDetailQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
    }

    public class DependenciesGetCommandsThatDependsQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class DependenciesGetDependentCommandDetailQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class BatchAllQueryValues
    {
        public Int32 IdLot { get; set; }
    }

    public class LotAndCommandLogQueryValues
    {
        public Int32 IdTemplate { get; set; }
    }

    public class LotAndCommandLotQueryValues
    {
        public Int32 IdLog { get; set; }
    }

    public class LotAndCommandCommandQueryValues
    {
        public Int32 IdLot { get; set; }
    }

    public class LotAndCommandResultQueryValues
    {
        public Int32 IdTemplate { get; set; }
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int32 Executions { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class LogExecutionDelayLotQueryValues
    {
        public Int32 IdLog { get; set; }
    }

    public class LogExecutionDelayCommandQueryValues
    {
        public Int32 IdLot { get; set; }
    }

    public class LogExecutionDelayResultQueryValues
    {
        public Int32 IdTemplate { get; set; }
        public Int32 IdLog { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public Int32 IdEvent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class LogExecutionResultQueryValues
    {
        public Int32 IdTemplate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int32 Executions { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class OperationsLogLogQueryValues
    {
        public bool Finished { get; set; }
    }

    public class OperationsLogCommandQueryValues
    {
        public Int32 IdLot { get; set; }
    }


    public class OperationsLogResultQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public string IdStatus { get; set; }
        public Int32 IdEnvironment { get; set; }
        public Int32 IdService { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public DateTime StartDateFrom { get; set; }
        public DateTime StartDateTo { get; set; }
        public DateTime EndDateFrom { get; set; }
        public DateTime EndDateTo { get; set; }
    }

    public class MonitorDataQueryValues
    {
        public DateTime CurrentDate { get; set; }
        public int IsRecurrent { get; set; }
    }

    public class ExecutionResultQueryValues
    {
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class CommandDataIdCommandQueryValues
    {
        public Int32 IdCommand { get; set; }
    }

    public class CommandDataIdDestinationQueryValues
    {
        public Int32 IdDestination { get; set; }
    }

    public class CommandDataExecutionQueryValues
    {
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class CommandDataObservationQueryValues
    {
        public Int32 IdLog { get; set; }
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
    }

    public class CommandDataInterfacesQueryValues
    {
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public bool IsEventual { get; set; }
    }
}
