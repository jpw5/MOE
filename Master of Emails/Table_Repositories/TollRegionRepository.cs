using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails.Table_Repositories
{
    public class TollRegionRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; private set; }
        private static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollPlaza>();
        }

        public void AddRegion(string RegionName)
        {
            Init();
            var region = new TollRegion
            {
                Region_name = RegionName
            };
            var id = DatabaseConnection.Insert(region);
        }

        public void DeleteRegion(int Region_id)
        {
            Init();
            DatabaseConnection.Delete<TollRegion>(Region_id);
        }

        public void UpdateRegion(int Region_id, string RegionName)
        {
            Init();
            var region = new TollRegion
            {
                Region_id = Region_id,
                Region_name = RegionName
            };
            DatabaseConnection.Update(region);
        }

        public List<TollRegion> GetRegions()
        {
            Init();

            try
            {
                return DatabaseConnection.Table<TollRegion>().ToList();
            }

            catch (Exception)
            {
                return null;
            }
        }

    }
}
