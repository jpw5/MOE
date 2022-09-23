using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Tables
{
    [Table("reason")]
    public class TollDuressReason
    {
        [PrimaryKey, AutoIncrement]
        public int Duress_reason_id { get; set; }
        public string Duress_reason_name { get; set; }

    }
}
