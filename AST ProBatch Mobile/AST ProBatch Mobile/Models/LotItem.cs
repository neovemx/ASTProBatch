namespace AST_ProBatch_Mobile.Models
{
    public class LotItem
    {
        #region Atributes
        public int Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreation { get; set; }
        public string InPath { get; set; }
        public string OutPath { get; set; }
        public PickerStatusItem StatusSelected { get; set; }
        #endregion   
    }
}
