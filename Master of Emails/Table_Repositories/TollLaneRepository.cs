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
        public string StatusMessage;
        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {

            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollLane>();
        }

        public void AddLane(int Plaza_id, int Lane_number, string Lane_type)
        {
            Init();

            try
            {
                var lane = new TollLane
                {
                    Plaza_id = Plaza_id,
                    Lane_number = Lane_number,
                    Lane_Type = Lane_type
                };

                DatabaseConnection.Insert(lane);
                StatusMessage = string.Format("Lane added");

            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add plaza. {0}", ex.Message);
            }

        }

        public void DeleteLane(int Id)
        {
            Init();
            DatabaseConnection.Delete<TollLane>(Id);
            StatusMessage = "Region Deleted";
        }


        public List<TollLane> GetLanes()
        {
            Init();

            try
            {
                return DatabaseConnection.Table<TollLane>().ToList();
            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);

            }

            return new List<TollLane>();

        }

        public TableQuery<TollLane> LaneQuery(int Plaza_Id)
        {
            Init();
            return DatabaseConnection.Table<TollLane>().Where(value => value.Plaza_id.Equals(Plaza_Id));
        }
    }

    


}
