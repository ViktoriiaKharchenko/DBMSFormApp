using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseControl
{
    public static class DatabaseFileSystem
    {
        private static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Database";
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
                    var db = dbSystem.AddDatabase(dir, false);
                    var files = FileUtil.GetFiles(path+"\\"+dir);
                    foreach (var file in files)
                    {
                        var table = db.AddTable(file.Split('.')[0], false);
                        table.Database = db.Name;
                        var lines = FileUtil.ReadFile(path+"\\"+ dir +"\\"+file);
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
        public static void SetPath(string otherPath)
        {
            path = otherPath;
        }
    }

}
