using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Tables
{
    [Table("emaildistribution")]
    public class TollEmailDistribution
    {
        [PrimaryKey, AutoIncrement]
        public int Email_distribution_id { get; set; }
        [NotNull]
        public string Email_distribution_region { get; set; }
        [NotNull]
        public string Email_distribution_type { get; set; }
        [NotNull]
        public string Email_distribution_plaza_id { get; set; }
        
        public string Email_distribution_to { get; set; }
        
        public string Email_distribution_cc { get; set; }
    }
}
