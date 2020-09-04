using System;

namespace ASTProBatchMobile.Models.Service
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
}
