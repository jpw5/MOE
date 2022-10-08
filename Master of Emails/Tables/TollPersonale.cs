using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Tables
{
    [Table("personale")]
    public class TollPersonale
    {
        public string Personale_kn_id { get; set; }

        public string Personale_name { get; set; }

        public string Personale_phone_number { get; set; }

        public string Personale_email { get; set; }

        public string Personale_department { get; set; }

        public string Personale_role { get; set; }

    }
}
