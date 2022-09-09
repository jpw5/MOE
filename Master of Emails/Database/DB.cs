using practice.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Database
{
    public class DB
    {
        public static string DatabasePath;
        public static string DatabaseName;
        public static SQLiteConnection DatabaseConnection;

        public static SQLiteConnection DatabaseInit()
        {
            DatabaseName = "MOE.db";
            DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
            DatabaseConnection = new SQLiteConnection(DatabasePath);
            return DatabaseConnection;
        }
    }
}
