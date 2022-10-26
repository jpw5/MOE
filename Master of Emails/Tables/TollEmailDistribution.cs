using SQLite;

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

        /* Unmerged change from project 'Master of Emails (net6.0-maccatalyst)'
        Before:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        After:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-windows10.0.19041.0)'
        Before:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        After:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-ios)'
        Before:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        After:
                public string Email_distribution_plaza_id { get; set; }

                public string Email_distribution_to { get; set; }
        */
        public string Email_distribution_plaza_id { get; set; }

        public string Email_distribution_to { get; set; }

        public string Email_distribution_cc { get; set; }
    }
}
