using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Tables
{
    [Table("organization")]
    public class TollOrganization
    {
        [PrimaryKey, AutoIncrement]
        public int Organization_id { get; set; }

        public string Organization_name { get; set; }

        public string Organization_phone_number { get; set; }

        public string Organization_email { get; set; }
    }
}
