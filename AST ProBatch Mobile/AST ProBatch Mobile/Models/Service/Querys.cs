﻿namespace ASTProBatchMobile.Models.Service
{
    public class InstanceQueryValues
    {
        public int IdLog { get; set; }
        public string IdUser { get; set; }
        public bool IsEventual { get; set; }
    }

    public class CommandQueryValues
    {
        public int IdInstance { get; set; }
    }
}
