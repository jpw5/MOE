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
    public class TollDuressReasonRepository
    {
        public DB DB;
        
        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollDuressReason>();
        }

        public void AddDuressReason(string Duress_reason_name)
        {
            Init();
            var reason = new TollDuressReason
            {
                Duress_reason_name= Duress_reason_name
            };
            var id = DatabaseConnection.Insert(reason);
        }

        public void DeleteDuressReason(int Duress_reason_id)
        {
            Init();
            DatabaseConnection.Delete<TollDuressReason>(Duress_reason_id);
        }

        public List<TollDuressReason> GetDuressReasons()
        {
            Init();
            return DatabaseConnection.Table<TollDuressReason>().ToList();
        }
    }
}


