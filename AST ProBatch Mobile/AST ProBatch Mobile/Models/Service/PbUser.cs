using System.Collections.Generic;

namespace AST_ProBatch_Mobile.Models
{
    public class PbUser
    {
        public string IdUser { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PbUserProfile> Profiles { get; set; }
        public bool IsValid { get; set; }
    }
}
