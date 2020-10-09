using System;
using System.Collections.ObjectModel;

namespace AST_ProBatch_Mobile.Models
{
    public class LotItem
    {
        #region Original Model From API
        public Int32 IdLot { get; set; }
        public string Code { get; set; }
        public string NameLot { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public string PathIn { get; set; }
        public string PathOut { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
        public bool IsTransfer { get; set; }
        #endregion

        #region Model Extended
        public string CreationDateString { get; set; }
        #endregion

        //#region Atributes
        //public int Id { get; set; }
        //public int Code { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public string DateCreation { get; set; }
        //public string InPath { get; set; }
        //public string OutPath { get; set; }
        //public PickerStatusItem StatusSelected { get; set; }
        //public ObservableCollection<CommandItem> CommandItems;
        //#endregion   
    }
}
