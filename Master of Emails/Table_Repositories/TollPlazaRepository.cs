using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using Google.Apis.Compute.v1.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Table_Repositories
{
    public class TollPlazaRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; private set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPlaza>();
        }

        public void AddPlaza(int Plaza_id, string Plaza_name, string Plaza_roadway, int Plaza_milepost, string Plaza_region, string Plaza_phone_number)
        {
            Init();
            var region = new TollPlaza
               {
                 Plaza_id = Plaza_id,
                 Plaza_name = Plaza_name,
                 Plaza_roadway = Plaza_roadway,
                 Plaza_milepost = Plaza_milepost,
                 Plaza_region = Plaza_region,
                 Plaza_phone_number=Plaza_phone_number
               };
            DatabaseConnection.Insert(region);
        }

        public void DeletePlaza(int Plaza_id) 
        {
            Init();
            DatabaseConnection.Delete<TollPlaza>(Plaza_id);
        }

        public List<TollPlaza> GetPlazas()
        {
            Init();
            return DatabaseConnection.Table<TollPlaza>().ToList();
        }  

         public TableQuery<TollPlaza> QueryByPlazaId(int Plaza_Id)
        {
            Init();
            return DatabaseConnection.Table<TollPlaza>().Where(value => value.Plaza_id.Equals(Plaza_Id) );
        }

        public TableQuery<TollPlaza> QueryByRegionName(string Region)
        {
            Init();
            return DatabaseConnection.Table<TollPlaza>().Where(value => value.Plaza_region.Equals(Region));
        }

    }
}
