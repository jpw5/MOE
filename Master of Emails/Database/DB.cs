using SQLite;
/* 
 Creates connection to MOE.db to be used by other parts of the app.
*/

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
