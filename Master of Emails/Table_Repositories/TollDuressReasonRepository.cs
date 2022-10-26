using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails.Table_Repositories
{
    public class TollDuressReasonRepository
    {

        /* Unmerged change from project 'Master of Emails (net6.0-maccatalyst)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-windows10.0.19041.0)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-ios)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */
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
                Duress_reason_name = Duress_reason_name
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


