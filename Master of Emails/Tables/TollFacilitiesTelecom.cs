using SQLite;


namespace Master_of_Emails.Tables
{
    [Table("facilitiestelecom")]
    public class TollFacilitiesTelecom
    {
        [PrimaryKey, Unique]
        public string Facilities_telecom_kn_id { get; set; }
        public string Facilities_telecom_name { get; set; }
        public string Facilities_telecom_phone_number { get; set; }
        public string Facilities_telecom_alerternate_number { get; set; }
        public string Facilities_telecom_email { get; set; }
        public string Department { get; set; }
    }
}
