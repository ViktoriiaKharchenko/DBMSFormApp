using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DatabaseControl
{
    static class FileUtil
    {
        public static void ToCSV(Table table, string strFilePath)
        {
            strFilePath += ".csv";
            StreamWriter sw = new StreamWriter(strFilePath, false);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sw.Write(table.Columns[i].Name+"("+table.Columns[i].TypeFullName+")");
                if (i < table.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            for (int j = 0; j < table.Rows.Count;j++)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string rowVal = table.Rows[j][i];
                    if (rowVal != null)
                    {
                        if (rowVal.Contains(';'))
                        {
                            rowVal = String.Format("\"{0}\"", rowVal);
                            sw.Write(rowVal);
                        }
                        else
                        {
                            sw.Write(rowVal);
                        }
                    }
                    if (i < table.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public static void WriteToBinary<T>(List<T> data, string path)
        {
            Stream ms = File.OpenWrite(path);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, data);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }
        public static List<T> ReadFromBinary<T>(string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Open(filePath, FileMode.Open);
            object obj = formatter.Deserialize(fs);
            List<T> list = (List<T>)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();
            return list;
        }
        public static void CreateFolder(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        public static void DeleteFolder (string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
                dirInfo.Delete(true);
            else throw new DirectoryNotFoundException ();
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
        public static string[] GetSubDirs(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetDirectories(path);
            }
            throw new DirectoryNotFoundException();
        }
        public static string[] GetFiles(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path);
            }
            throw new DirectoryNotFoundException();
        }
        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
        public static Dictionary<string,string> ParseColumns(string colums)
        {
            Dictionary<string, string> columnTypes = new Dictionary<string, string>();
            string[] cols = colums?.Split(';');
            if (cols == null) return null;
            foreach(var col in cols)
            {
                if (col == "")
                    return null;
                string colName = col.Split('(')[0];
                string type = col.Substring(col.IndexOf('(')+1, col.Length-col.IndexOf('(')-2) ;//.Substring(0, col.Split('(')[1].Length);
                columnTypes.Add(colName, type);
            }
            return columnTypes;
        }
        public static List<List<string>> ParseRows(string[] lines)
        {
            List<List<string>> rows = new List<List<string>>();
            for(int i = 1; i<lines.Length; i++)
            {
                List<string> row = new List<string>();
                row.AddRange(lines[i].Split(';'));
                rows.Add(row);
            }
            return rows;
        }
       
    }
}
