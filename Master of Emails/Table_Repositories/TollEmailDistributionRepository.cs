using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Master_of_Emails.Table_Repositories
{
    public class TollEmailDistributionRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; set; }
        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollEmailDistribution>();
        }
        public void AddEmailDistribution(string Email_distribution_region, string Email_distribution_type,
            string Email_distribution_plaza_id,string Email_distribution_to, string Email_distribution_cc)
        {
            Init();
            var emaildistribution = new TollEmailDistribution
            {
                Email_distribution_region = Email_distribution_region,
                Email_distribution_type = Email_distribution_type,
                Email_distribution_plaza_id = Email_distribution_plaza_id,
                Email_distribution_to = Email_distribution_to,
                Email_distribution_cc = Email_distribution_cc

            };
            DatabaseConnection.Insert(emaildistribution);
        }

        public void DeleteEmailDistribution(int Email_distribution_id)
        {
            Init();
            DatabaseConnection.Delete<TollEmailDistribution>(Email_distribution_id);
        }

        public List<TollEmailDistribution> GetEmailDistributions()
        {
            Init();
            return DatabaseConnection.Table<TollEmailDistribution>().ToList();
        }

        public TableQuery<TollEmailDistribution> QueryByRegionEmailTypeAndPlazaId(string Region, string Type, string PlazaId)
        {
            Init();
            return DatabaseConnection.Table<TollEmailDistribution>().Where(value => 
            value.Email_distribution_region.Equals(Region) && 
            value.Email_distribution_type.Equals(Type) && 
            value.Email_distribution_plaza_id.Equals(PlazaId));
        }

        public TableQuery<TollEmailDistribution> QueryByPlazaId(string PlazaId)
        {
            Init();
            return DatabaseConnection.Table<TollEmailDistribution>().Where(value =>
            value.Email_distribution_plaza_id.Equals(PlazaId));
        }
    }
}
