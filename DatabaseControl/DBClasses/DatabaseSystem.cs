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
        public DatabaseSystem(string path)
        {
            DatabaseFileSystem.SetPath(path);
            Databases = new List<Database>();
            DatabaseFileSystem.LoadDatabases(this);
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
        public Database GetDatabase(int id)
        {
            return Databases.FindLast(t => t.Id == id); ;
        }
        public void DeleteDatabase(string name, int id)
        {
            var db = GetDatabase(name, id);
            DatabaseFileSystem.DeleteDB(db);
            Databases.Remove(db);
        }
        public void AddDatabase(Database db)
        {
            if (!CheckDatabase(db)) throw new Exception("Invalid database");
            var newDb = AddDatabase(db.Name);
            foreach(var table in db.Tables)
            {
                var newTable = newDb.AddTable(table.Name);
               
                foreach (var column in table.Columns)
                {
                    newTable.AddColumn(column.Name, column.TypeFullName);
                }
                foreach(var row in table.Rows)
                {
                    newTable.AddRow();
                    for(int i =0; i<table.Columns.Count; i++)
                    {
                        newTable.EditRow(row[i], table.Columns[i].Name, table.Rows.IndexOf(row));
                    }
                }
            }
        }
        public bool CheckDatabase(Database db)
        {
            foreach (var table in db.Tables)
            {
                if (!db.CheckTable(table)) throw new Exception("Invalid table specified");
            }
            return true;

        }
    }
}
