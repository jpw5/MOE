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
    public class TollRegionRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; private set; }
        private static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPlaza>();
        }

        public void AddRegion(string RegionName)
        {
            Init();
            var region = new TollRegion
                {
                  Region_name = RegionName
                };
               var id=DatabaseConnection.Insert(region);
        }

        public void DeleteRegion(int Id)
        {
             Init();
             DatabaseConnection.Delete<TollRegion>(Id);
        }

        public List<TollRegion> GetRegions()
        {
            Init();
            return DatabaseConnection.Table<TollRegion>().ToList();
        }

    }
}
