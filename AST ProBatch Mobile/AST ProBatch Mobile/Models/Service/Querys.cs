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
    }

    public class ObservationAddQueryValues
    {
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
        public string NameObsv { get; set; }
        public string DetailObsv { get; set; }
    }

    public class ObservationModQueryValues
    {
        public Int32 IdObsv { get; set; }
        public Int32 IdLog { get; set; }
        public string IdUser { get; set; }
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
}
