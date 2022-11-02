using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails.Table_Repositories
{
    public class TollPersonaleRepository
    {
        public DB DB;

        public static SQLiteConnection DatabaseConnection { get; set; }
        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPersonale>();
        }

        public void AddPersonale(string NewPersonaleKnId, string NewPersonaleName,
        string NewPersonalePhoneNumber, string NewPersonaleEmail, string NewPersonaleDepartment, string NewPersonaleRole)
        {
            Init();
            var personale = new TollPersonale
            {
                Personale_kn_id = NewPersonaleKnId,
                Personale_email = NewPersonaleEmail,
                Personale_name = NewPersonaleName,
                Personale_phone_number = NewPersonalePhoneNumber,
                Personale_department = NewPersonaleDepartment,
                Personale_role = NewPersonaleRole
            };
            DatabaseConnection.Insert(personale);
        }

        public void DeletePersonale(string Personale_kn_id)
        {
            Init();
            DatabaseConnection.Delete<TollPersonale>(Personale_kn_id);
        }

        public List<TollPersonale> GetPersonale()
        {
            Init();
            return DatabaseConnection.Table<TollPersonale>().ToList();
        }

        public TableQuery<TollPersonale> QueryPersonaleByName(string PersonaleName)
        {
            Init();
            PersonaleName = PersonaleName.ToUpper();
            return DatabaseConnection.Table<TollPersonale>().Where(value => value.Personale_name.ToUpper().Contains(PersonaleName));

        }
    }
}
