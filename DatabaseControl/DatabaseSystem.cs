using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseControl
{
    public class DatabaseSystem
    {
        public List<Database> Databases { get; private set; }

        public DatabaseSystem()
        {
            Databases = new List<Database>();
        }
        public Database AddDatabase(string name, bool save = true)
        {
            Database db = new Database(name);
            if (save)
                DatabaseFileSystem.SaveDB(db);
            Databases.Add(db);
            return db;
        }
        public Database GetDatabase(string name)
        {
            return Databases.FindLast(t => t.Name.Equals(name,StringComparison.OrdinalIgnoreCase));
        }
        public void DeleteDatabase(string name)
        {
            var db = GetDatabase(name);
            DatabaseFileSystem.DeleteDB(db);
            Databases.Remove(db);
        }
    }
}
