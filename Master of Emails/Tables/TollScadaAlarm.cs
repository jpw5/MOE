using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Tables
{
    [Table("scadaalarm")]
    public class TollScadaAlarm
    {
        [PrimaryKey, AutoIncrement]
        public int Scada_alarm_id { get; set; }

        public string Scada_alarm_name { get; set; }
    }
}
