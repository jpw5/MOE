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
    public class TollPersonaleRepository
    {
        public DB DB;

        public static SQLiteConnection DatabaseConnection { get; set; }
        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollBomitem>();
        }

        public void AddPersonale(string NewPersonaleKnId, string NewPersonaleName,
        string NewPersonalePhoneNumber, string NewPersonaleEmail, string NewPersonaleDepartment)
        {
            Init();
            var personale = new TollPersonale
            {
                Personale_kn_id = NewPersonaleKnId,
                Personale_email = NewPersonaleEmail,
                Personale_name = NewPersonaleName,
                Personale_phone_number = NewPersonalePhoneNumber,
                Personale_department = NewPersonaleDepartment
            };
            DatabaseConnection.Insert(personale);
        }

        public void DeletePersonale(string Personale_kn_id)
        {
            Init();
            DatabaseConnection.Delete<TollPersonale>(Personale_kn_id);
        }

        public List<TollPersonale> GetPersonale()
        {
            Init();
            return DatabaseConnection.Table<TollPersonale>().ToList();
        }



    }
}
