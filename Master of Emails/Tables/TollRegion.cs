using SQLite;


namespace Master_of_Emails.Tables
{
    [Table("region")]
    public class TollRegion
    {
        [PrimaryKey, AutoIncrement]
        public int Region_id { get; set; }

        [MaxLength(250), Unique, NotNull]
        public string Region_name { get; set; }
    }
}
