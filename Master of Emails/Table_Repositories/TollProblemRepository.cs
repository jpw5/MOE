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
    public class TollProblemRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; private set; }

        private static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollProblem>();
        }

    }
}
