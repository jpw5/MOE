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
    public class TollFacilitiesTelecomRepository
    {
        public DB DB;
        public string StatusMessage;
        public static SQLiteConnection DatabaseConnection { get; set; }


        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollFacilitiesTelecom>();
        }

        public void AddFacilitiesTelecom(string Facilities_telecom_kn_id, string Facilities_telecom_name, string Facilities_telecom_phone_number, string Facilities_telecom_email)
        {
            Init();
            var facilitiestelecom = new TollFacilitiesTelecom
            {
                Facilities_telecom_kn_id = Facilities_telecom_kn_id,
                Facilities_telecom_name = Facilities_telecom_name,
                Facilities_telecom_phone_number = Facilities_telecom_phone_number,
                Facilities_telecom_email= Facilities_telecom_email
            };
             DatabaseConnection.Insert(facilitiestelecom);
        }

        public void DeleteFacilitiesTelecom(string Facilities_telecom_kn_id)
        {
            Init();
            DatabaseConnection.Delete<TollFacilitiesTelecom>(Facilities_telecom_kn_id);
        }

        public List<TollFacilitiesTelecom> GetFacilitiesTelecoms()
        {
            Init();
            return DatabaseConnection.Table<TollFacilitiesTelecom>().ToList();
        }


    }


}
