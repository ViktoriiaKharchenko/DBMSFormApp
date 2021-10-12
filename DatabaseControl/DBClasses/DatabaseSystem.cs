using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseControl
{
    public class DatabaseSystem
    {
        private int dbKey = 0;
        public List<Database> Databases { get; private set; }

        public DatabaseSystem()
        {
            Databases = new List<Database>();
        }
        public Database AddDatabase(string name, bool save = true)
        {
            Database db = new Database(name, dbKey++);
            if (save)
                DatabaseFileSystem.SaveDB(db);
            Databases.Add(db);
            return db;
        }
        public Database GetDatabase(string name, int id)
        {
            return Databases.FindLast(t => t.Name.Equals(name,StringComparison.OrdinalIgnoreCase) && t.Id == id);
        }
        public Database GetDatabase(string name)
        {
            return Databases.FindLast(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public void DeleteDatabase(string name, int id)
        {
            var db = GetDatabase(name, id);
            DatabaseFileSystem.DeleteDB(db);
            Databases.Remove(db);
        }
    }
}
