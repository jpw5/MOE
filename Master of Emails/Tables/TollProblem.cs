using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Tables
{
    [Table("problem")]
    public class TollProblem
    {
        [PrimaryKey, Unique]
        public int Problem_id { get; set; } 
        public string Problem_name { get; set; }
        public string Problem_bomitem { get; set; }

    }
}
