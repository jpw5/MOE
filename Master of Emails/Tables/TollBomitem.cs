using SQLite;

namespace Master_of_Emails.Tables
{
    [Table("bomitem")]

    public class TollBomitem
    {
        [PrimaryKey, AutoIncrement]
        public int Bomitem_id { get; set; }

        public string Bomitem_lane_type { get; set; }

        public string Bomitem_name { get; set; }
    }
}
