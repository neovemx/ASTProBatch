using AST_ProBatch_Mobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.Services
{
    public class DataHelper
    {
        private ISQLitePlatform platform;
        private SQLiteAsyncConnection dbAsync;
        private SQLiteConnection db;

        public DataHelper()
        {
            platform = DependencyService.Get<ISQLitePlatform>();
            dbAsync = platform.GetAsyncConnection();
            db = platform.GetConnection();
        }

        public async Task<bool> CreateAsyncTablesAndInitialData()
        {
            try
            {
                int countTableCreationSuccess = 0;
                var resultTappuser = await dbAsync.GetTableInfoAsync("Table_User");
                if (resultTappuser.Count == 0)
                {
                    await dbAsync.CreateTableAsync<Table_User>();
                    Table_User table_User = new Table_User { Username = "", Password = "" };
                    var resultDB = await dbAsync.InsertAsync(table_User);
                    if (resultDB == 1)
                    {
                        countTableCreationSuccess += 1;
                    }
                }
                var resultTconfig = await dbAsync.GetTableInfoAsync("Table_Config");
                if (resultTconfig.Count == 0)
                {
                    await dbAsync.CreateTableAsync<Table_Config>();
                    Table_Config table_Config = new Table_Config { UrlDomain = "", UrlPrefix = "", FingerPrintAllow = false };
                    var resultDB = await dbAsync.InsertAsync(table_Config);
                    if (resultDB == 1)
                    {
                        countTableCreationSuccess += 1;
                    }
                }
                if (countTableCreationSuccess == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateTablesAndInitialData()
        {
            try
            {
                int countTableCreationSuccess = 0;
                var resultTappuser = db.GetTableInfo("Table_User");
                if (resultTappuser.Count == 0)
                {
                    db.CreateTable<Table_User>();
                    Table_User table_User = new Table_User { Username = "", Password = "" };
                    var resultDB = db.Insert(table_User);
                    if (resultDB == 1)
                    {
                        countTableCreationSuccess += 1;
                    }
                }
                else
                {
                    countTableCreationSuccess += 1;
                }
                var resultTconfig = db.GetTableInfo("Table_Config");
                if (resultTconfig.Count == 0)
                {
                    db.CreateTable<Table_Config>();
                    Table_Config table_Config = new Table_Config { UrlDomain = "", UrlPrefix = "", FingerPrintAllow = false };
                    var resultDB = db.Insert(table_Config);
                    if (resultDB == 1)
                    {
                        countTableCreationSuccess += 1;
                    }
                }
                else
                {
                    countTableCreationSuccess += 1;
                }

                if (countTableCreationSuccess == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Table_Config> GetAsyncAppConfig()
        {
            try
            {
                IEnumerable<Table_Config> getConfigApp = await dbAsync.QueryAsync<Table_Config>("SELECT * FROM Table_Config where Id = 1");
                return getConfigApp.ToList<Table_Config>()[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Table_Config GetAppConfig()
        {
            try
            {
                IEnumerable<Table_Config> getConfigApp = db.Query<Table_Config>("SELECT * FROM Table_Config where Id = 1");
                return getConfigApp.ToList<Table_Config>()[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Table_User> GetAsyncProbatchCredentials()
        {
            try
            {
                IEnumerable<Table_User> getProbatchUser = await dbAsync.QueryAsync<Table_User>("SELECT * FROM Table_User where Id = 1");
                return getProbatchUser.ToList<Table_User>()[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PullAsyncAppConfig(Table_Config table_Config)
        {
            try
            {
                var result = await dbAsync.UpdateAsync(table_Config);
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PullAsyncProbatchCredentials(Table_User table_User)
        {
            try
            {
                var result = await dbAsync.UpdateAsync(table_User);
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task<bool> CreateTableItems()
        //{
        //    try
        //    {
        //        var resultTappuser = await db.GetTableInfoAsync("TItems");
        //        if (resultTappuser.Count == 0)
        //        {
        //            await db.CreateTableAsync<TItems>();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<ObservableCollection<TItemsView>> GetItems()
        //{
        //    ObservableCollection<TItemsView> listItems = new ObservableCollection<TItemsView>();
        //    TItemsView tItems;
        //    try
        //    {
        //        var resultListItems = await db.QueryAsync<TItems>("SELECT * FROM TItems order by ItemType");
        //        foreach (TItems item in resultListItems)
        //        {
        //            tItems = new TItemsView();
        //            tItems.Id = item.Id;
        //            tItems.ItemId = item.ItemId;
        //            tItems.ItemName = item.ItemName;
        //            tItems.ItemType = item.ItemType;
        //            tItems.ItemIcon = item.ItemIcon;
        //            tItems.LastUpdate = item.LastUpdate;
        //            tItems.LastUpdateView = item.LastUpdate.ToString("dd/MM/yyyy hh:mm ttt");
        //            listItems.Add(tItems);
        //        }
        //        return listItems;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en base de datos");
        //    }
        //}

        //public async Task<ObservableCollection<TimagesDecrypted>> GetImagesByItemId(string itemId, float imagesSize)
        //{
        //    ObservableCollection<TimagesDecrypted> listImages = new ObservableCollection<TimagesDecrypted>();
        //    TimagesDecrypted timagesDecrypted;
        //    try
        //    {
        //        var resultListImagesEncrypted = await db.QueryAsync<TImagesEncrypted>("SELECT * FROM TImagesEncrypted where ItemId = '" + itemId + "'");
        //        foreach (TImagesEncrypted imageEncrypted in resultListImagesEncrypted)
        //        {
        //            timagesDecrypted = new TimagesDecrypted();
        //            timagesDecrypted.Id = imageEncrypted.Id;
        //            timagesDecrypted.ItemId = imageEncrypted.ItemId;
        //            timagesDecrypted.ImageName = Crypto.DecodeString(imageEncrypted.ImageName) + imageEncrypted.FileExtension;
        //            string imageEncryptedInBase64 = Convert.ToBase64String(imageEncrypted.ImageData);
        //            string imageDecryptedInBase64 = Crypto.DecodeString(imageEncryptedInBase64);
        //            byte[] imageDecryptedInBytes = Convert.FromBase64String(imageDecryptedInBase64);
        //            timagesDecrypted.ImageData = ImageSource.FromStream(() => new MemoryStream(imageDecryptedInBytes));
        //            timagesDecrypted.ImageDataMini = ImageSource.FromStream(() => new MemoryStream(Photo.GetMini(imageDecryptedInBytes, imagesSize, imagesSize)));
        //            timagesDecrypted.DateCreation = imageEncrypted.DateCreation.ToString("dd/MM/yyyy hh:mm ttt");
        //            timagesDecrypted.FileName = imageEncrypted.FileName;
        //            timagesDecrypted.FileExtension = imageEncrypted.FileExtension.Split('.')[1].ToUpper();
        //            timagesDecrypted.OriginalFullPath = imageEncrypted.OriginalFullPath;
        //            timagesDecrypted.FileSize = (imageEncrypted.FileSize / 1024).ToString() + " Kb";
        //            timagesDecrypted.OriginalData = imageEncrypted;
        //            listImages.Add(timagesDecrypted);
        //        }
        //        return listImages;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error en base de datos");
        //    }
        //}

        //public async Task<bool> CreateNewItem(TItems tItems)
        //{
        //    try
        //    {
        //        var result = await db.InsertAsync(tItems);
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}

        //public async Task<bool> CreateNewImage(TImagesEncrypted imageEncrypted)
        //{
        //    try
        //    {
        //        var result = await db.InsertAsync(imageEncrypted);
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> DeleteImage(TImagesEncrypted imageEncrypted)
        //{
        //    try
        //    {
        //        var result = await db.DeleteAsync(imageEncrypted);
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}
