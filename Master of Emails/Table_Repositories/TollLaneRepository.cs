using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;


namespace Master_of_Emails.Table_Repositories
{
    public class TollLaneRepository
    {

        /* Unmerged change from project 'Master of Emails (net6.0-maccatalyst)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-windows10.0.19041.0)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */

        /* Unmerged change from project 'Master of Emails (net6.0-ios)'
        Before:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        After:
                public DB DB;

                public static SQLiteConnection DatabaseConnection { get; set; }
        */
        public DB DB;

        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollLane>();
        }

        public void AddLane(int Plaza_id, int Lane_number, string Lane_type, string Lane_direction)
        {
            Init();
            var lane = new TollLane
            {
                Plaza_id = Plaza_id,
                Lane_number = Lane_number,
                Lane_type = Lane_type,
                Lane_direction=Lane_direction
            };
            DatabaseConnection.Insert(lane);
        }

        public void DeleteLane(int Id)
        {
            Init();
            DatabaseConnection.Delete<TollLane>(Id);
        }

        public List<TollLane> GetLanes()
        {
            Init();
            return DatabaseConnection.Table<TollLane>().ToList();
        }

        public TableQuery<TollLane> QueryByPlazaId(int Plaza_Id)
        {
            Init();
            return DatabaseConnection.Table<TollLane>().Where(value => value.Plaza_id.Equals(Plaza_Id));
        }

        public TableQuery<TollLane> QueryByPlazaIdAndLaneDirection(int Plaza_Id, string Lane_Direction)
        {
            Init();
            return DatabaseConnection.Table<TollLane>().Where(value => 
            value.Plaza_id.Equals(Plaza_Id) && 
            value.Lane_direction.Equals(Lane_Direction));
        }
    }




}
