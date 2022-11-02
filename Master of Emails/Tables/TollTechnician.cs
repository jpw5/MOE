using SQLite;


namespace Master_of_Emails.Tables
{
    [Table("technician")]
    public class TollTechnician
    {
        [PrimaryKey]
        public string Technician_kn_id { get; set; }
        public string Technician_name { get; set; }
        public string Technician_phone_number { get; set; }
        public string Technician_email { get; set; }
        public string Technician_region { get; set; }
    }
}
