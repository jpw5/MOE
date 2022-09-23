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
    public class TollBomitemRepository
    {
        public DB DB;
        public string StatusMessage;
        public static SQLiteConnection DatabaseConnection { get; set; }

        public static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollBomitem>();
        }

        public void AddBomitem(string Bomitem_lane_type, string Bomitem_name)
        {
            Init();
            var bomitem = new TollBomitem
            {
                Bomitem_lane_type = Bomitem_lane_type,
                Bomitem_name = Bomitem_name
            };
            var id=DatabaseConnection.Insert(bomitem);
        }

        public void DeleteBomitem(int Bomitem_id)
        {
            Init();
            DatabaseConnection.Delete<TollBomitem>(Bomitem_id);
        }

        public List<TollBomitem> GetBomitems()
        {
            Init();
            return DatabaseConnection.Table<TollBomitem>().ToList();
        }

        public TableQuery<TollBomitem> QueryByLaneType(string Lane_Type)
        {
            Init();
            return DatabaseConnection.Table<TollBomitem>().Where(value => value.Bomitem_lane_type.Equals(Lane_Type));
        }


    }
}
