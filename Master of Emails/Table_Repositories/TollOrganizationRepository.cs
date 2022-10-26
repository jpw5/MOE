using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails.Table_Repositories
{
    public class TollOrganizationRepository
    {
        public DB DB;

        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollOrganization>();
        }

        public void AddOrganization(string Organization_name, string Organization_phone_number,
        string Organization_email)
        {
            Init();
            var organization = new TollOrganization
            {
                Organization_name = Organization_name,
                Organization_phone_number = Organization_phone_number,
                Organization_email = Organization_email
            };
            DatabaseConnection.Insert(organization);
        }

        public void DeleteOrgnization(int Organization_id)
        {
            Init();
            DatabaseConnection.Delete<TollOrganization>(Organization_id);
        }

        public List<TollOrganization> GetOrganizations()
        {
            Init();
            return DatabaseConnection.Table<TollOrganization>().ToList();
        }

        public TableQuery<TollOrganization> QueryByOrganizationName(string OrganizationName)
        {
            Init();
            OrganizationName = OrganizationName.ToUpper();
            return DatabaseConnection.Table<TollOrganization>().Where(value => value.Organization_name.ToUpper().Contains(OrganizationName));
        }
    }
}
