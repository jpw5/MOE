using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Master_of_Emails.Table_Repositories
{
    public class TollsRegionRepository
    {
        public DB DB;
        public string StatusMessage;
        public static SQLiteConnection DatabaseConnection { get; private set; }
        private static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPlaza>();
        }

        public void AddRegion(string Region_name)
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(Region_name))
                    throw new Exception("Valid name required");

                var region = new TollsRegion
                {
                    Region_name = Region_name

                };

                var id =  DatabaseConnection.Insert(region);
                StatusMessage = string.Format("{0} record(s) added (Name: {1})", id, Region_name);
            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", Region_name, ex.Message);
            }
        
        }

        public void RemoveRegion(int id)
        {
             Init();
             DatabaseConnection.Delete<TollsRegion>(id);
             StatusMessage = "Region Deleted";
        }

        public List<TollsRegion> GetRegions()
        {
            
            Init();
            try
            {
                return DatabaseConnection.Table<TollsRegion>().ToList();
            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                
            }

            return new List<TollsRegion>();

            
        }

    }
}
