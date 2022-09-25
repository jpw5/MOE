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
    public class TollScadaAlarmRepository
    {
        public DB DB;
        public string StatusMessage;
        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollScadaAlarm>();
        }

        public void AddScadaAlarm(string Scada_alarm_name)
        {
            Init();
            var scadaalarm = new TollScadaAlarm
            {
               Scada_alarm_name=Scada_alarm_name
            };
            var id = DatabaseConnection.Insert(scadaalarm);
        }

        public void DeleteScadaAlarm(int Scada_alarm_id)
        {
            Init();
            DatabaseConnection.Delete<TollScadaAlarm>(Scada_alarm_id);
        }

        public List<TollScadaAlarm> GetScadaAlarms()
        {
            Init();
            return DatabaseConnection.Table<TollScadaAlarm>().ToList();
        }
    }
}
