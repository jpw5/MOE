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
        public string StatusMessage;


        public static SQLiteConnection DatabaseConnection { get; private set; }

        public static void Init()
        {

            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPlaza>();
        }

        public void AddPlaza(int Plaza_id, string Plaza_name, string Plaza_roadway, int Plaza_milepost, string Plaza_region, string Plaza_phone_number)
        {
                Init();

            try
            {
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
                StatusMessage = string.Format("Plaza {0} added (Name: {1})", Plaza_id, Plaza_name);

            }

            catch (Exception ex) 
            {
                StatusMessage = string.Format("Failed to add plaza. {0}", ex.Message);
            }
        }

        public void DeletePlaza(int Plaza_id) {

            Init();

            DatabaseConnection.Delete<TollPlaza>(Plaza_id);
            StatusMessage = "Plaza Deleted";
        }

        public List<TollPlaza> GetPlazas()
        {
            Init();

            try
            {
                return DatabaseConnection.Table<TollPlaza>().ToList();
            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);

            }

            return new List<TollPlaza>();

        }  

         public TableQuery<TollPlaza> PlazaQuery(int Plaza_Id)
        {
            Init();

            StatusMessage = string.Format("Failed to retrieve data.");
            return DatabaseConnection.Table<TollPlaza>().Where(value => value.Plaza_id.Equals(Plaza_Id));
        }

    }
}
