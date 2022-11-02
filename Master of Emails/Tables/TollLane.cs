using SQLite;


namespace Master_of_Emails.Tables
{
    [Table("lane")]
    public class TollLane
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Plaza_id { get; set; }

        public int Lane_number { get; set; }

        public string Lane_Type { get; set; }
    }
}
