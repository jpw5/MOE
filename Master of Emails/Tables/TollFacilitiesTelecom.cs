﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Tables
{
    [Table("facilitiestelecom")]
    public class TollFacilitiesTelecom
    {
        [PrimaryKey, Unique]
        public string Facilities_telecom_kn_id { get; set; }
        public string Facilities_telecom_name { get; set; }
        public string Facilities_telecom_phone_number { get; set; }
        public string Facilities_telecom_email { get; set; }
    }
}
