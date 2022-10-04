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
    public class TollLaneRepository
    {
        public DB DB;
        
        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollLane>();
        }

        public void AddLane(int Plaza_id, int Lane_number, string Lane_type)
        {
            Init();
            var lane = new TollLane
                {
                  Plaza_id = Plaza_id,
                  Lane_number = Lane_number,
                  Lane_Type = Lane_type
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
    }

    


}
