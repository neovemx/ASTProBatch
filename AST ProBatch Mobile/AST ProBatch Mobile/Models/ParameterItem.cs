namespace AST_ProBatch_Mobile.Models
{
    public class ParameterItem
    {
        #region Atributes
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int IdEnvironment { get; set; }
        public string TitleEnvironment { get; set; }
        public int IdUser { get; set; }
        public string TitleUser { get; set; }
        #endregion
    }
}
