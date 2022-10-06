﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Tables
{
    [Table("plaza")]
    public class TollPlaza
    {

        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        [NotNull]
        public int Plaza_id { get; set; }
        [MaxLength(250), NotNull]
        public string Plaza_name { get; set; }
        [MaxLength(250), NotNull]
        public string Plaza_roadway { get; set; }
        [NotNull]
        public int Plaza_milepost { get; set; }
        [NotNull]
        public string Plaza_phone_number{ get; set; }
        [NotNull]
        public string Plaza_region { get; set; }
        public string Plaza_company { get; set; }
    }
}
