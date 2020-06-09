using AST_ProBatch_Mobile.iOS.Services;
using AST_ProBatch_Mobile.Services;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(iOSSQLitePlatform))]

namespace AST_ProBatch_Mobile.iOS.Services
{
    public class iOSSQLitePlatform : ISQLitePlatform
    {
        private string GetPatch()
        {
            var dbName = "PBMobileDB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return path;
        }
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetPatch());
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPatch());
        }
    }
}