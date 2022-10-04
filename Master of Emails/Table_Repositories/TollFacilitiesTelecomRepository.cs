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
        
        public static SQLiteConnection DatabaseConnection { get; set; }


        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollFacilitiesTelecom>();
        }

        public void AddFacilitiesTelecom(string Facilities_telecom_kn_id, string Facilities_telecom_name, 
        string Facilities_telecom_phone_number, string Facilities_telecom_alternate_phone_number, 
        string Facilities_telecom_email, string Department)
        {
            Init();
            var facilitiestelecom = new TollFacilitiesTelecom
            {
                Facilities_telecom_kn_id = Facilities_telecom_kn_id,
                Facilities_telecom_name = Facilities_telecom_name,
                Facilities_telecom_phone_number = Facilities_telecom_phone_number,
                Facilities_telecom_alerternate_number= Facilities_telecom_alternate_phone_number,
                Facilities_telecom_email = Facilities_telecom_email,
                Department = Department

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

        public TableQuery<TollFacilitiesTelecom> QueryPersonaleByName(string FacilitiesTelecomName)
        {
            Init();
            FacilitiesTelecomName = FacilitiesTelecomName.ToUpper();
            return DatabaseConnection.Table<TollFacilitiesTelecom>().Where(value => value.Facilities_telecom_name.ToUpper().Contains(FacilitiesTelecomName));

            //return DatabaseConnection.Table<TollTechnician>().Where(value => value.Technician_name.
            //Equals(TechnicianName));
        }


    }


}
