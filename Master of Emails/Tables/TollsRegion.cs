using Master_of_Emails.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Tables
{
    [Table("tollsRegion")]
    public class TollsRegion
    {
        [PrimaryKey, AutoIncrement]
        public int Region_id { get; set; }

        [MaxLength(250), Unique]
        public string Region_name { get; set; }
    }
}
