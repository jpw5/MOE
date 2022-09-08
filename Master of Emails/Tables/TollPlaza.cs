using SQLite;
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
        [PrimaryKey, Unique]
        public int Plaza_id { get; set; }

        [MaxLength(250), Unique]
        public string Plaza_name { get; set; }

        [MaxLength(250), Unique]
        public string Plaza_roadway { get; set; }

        public int Plaza_milepost { get; set; }

        public string Plaza_phone_number{ get; set; }

        public string Plaza_region { get; set; }


    }
}
