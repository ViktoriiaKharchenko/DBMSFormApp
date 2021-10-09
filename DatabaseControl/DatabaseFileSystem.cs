using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseControl
{
    static class DatabaseFileSystem
    {
        private static readonly string path = "C:\\Users\\Boss\\source\\repos\\DatabaseControlSystem\\Database";

        public static void SaveTable (Table table, string dbName)
        {
            string filePath = path + "\\" + dbName + "\\" + table.Name;
            FileUtil.ToCSV(table, filePath);
        }
        public static void SaveDB(Database db)
        {
            string dirPath = path + "\\"+db.Name;
            FileUtil.CreateFolder(dirPath);
        }
        public static void DeleteDB (Database db)
        {
            string dirPath = path + "\\" + db.Name;
            try
            {
                FileUtil.DeleteFolder(dirPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void DeleteTable(Table table, string dbName)
        {
            string filePath = path + "\\" + dbName + "\\" + table.Name+".csv";
            try
            {
                FileUtil.DeleteFile(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void LoadDatabases(DatabaseSystem dbSystem)
        {
            try
            {
                var subDirs = FileUtil.GetSubDirs(path);
                foreach (var dir in subDirs)
                {
                    var dbName = dir.Split('\\')[dir.Split('\\').Length-1];
                    var db = dbSystem.AddDatabase(dbName, false);
                    var files = FileUtil.GetFiles(dir);
                    foreach (var file in files)
                    {
                        var tableName = file.Split('\\')[file.Split('\\').Length - 1].Split('.')[0];
                        var table = db.AddTable(tableName, false);
                        table.Database = db.Name;
                        var lines = FileUtil.ReadFile(file);
                        ParseTable(lines, table);
                    }
                }
            }
            catch (DirectoryNotFoundException) { }
        }
        private static void ParseTable(string[] lines, Table table)
        {
            var columns = lines.Length >0 ?lines[0]: null;
            var cols = FileUtil.ParseColumns(columns);
            if (cols == null)
                return;
            foreach (var col in cols)
            {
                table.AddColumn(col.Key, col.Value, false);
            }
            var rows = FileUtil.ParseRows(lines);
            foreach (var row in rows)
            {
                table.AddRows(row, false);
            }
        }
    }

}
