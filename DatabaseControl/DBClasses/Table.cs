using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseControl
{
    public enum Invl { charInvl, stringInvl}
    public class Table
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Database { get; set; }
        public List<Column> Columns { get; private set; }
        public List<List<string>> Rows { get; private set; }
        public Table(string name, int key)
        {
            Name = name;
            Id = key;
            Columns = new List<Column>();
            Rows = new List<List<string>>();
        }
        public void AddColumn<T>(string name)
        {
            Column col = new Column(name, typeof(T));
            Columns.Add(col);
            if(Rows.Count != 0)
            {
                foreach(var row in Rows)
                {
                    row.Add(null);
                }
            }
            DatabaseFileSystem.SaveTable(this,Database);
        }
        public void AddColumn(string name, string typeName, bool save = true)
        {
            Regex stringInvl = new Regex(@"StringInvl\({1,1}\w,\w\)");
            Regex charrgx = new Regex(@"CharInvl\({1,1}\w,\w\)");
            if (typeName.Contains("Invl") && !charrgx.IsMatch(typeName) && !stringInvl.IsMatch(typeName))
            {
                throw new InvalidCastException();
            }
            else if (!typeName.Contains("Invl"))
            {
                try
                {
                    var type = Type.GetType(typeName);
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }
            Column col = new Column(name, typeName);
            Columns.Add(col);
            if (Rows.Count != 0)
            {
                foreach (var row in Rows)
                {
                    row.Add(null);
                }
            }
            if(save)
                DatabaseFileSystem.SaveTable(this, Database);
        }
        public void AddColumn(string name, Type type, bool save = true)
        {
            Column col = new Column(name, type);
            Columns.Add(col);
            if(save)
                DatabaseFileSystem.SaveTable(this, Database);
        }
        public Column GetColumn(string colName)
        {
            return Columns.FindLast(t=>t.Name.Equals(colName,StringComparison.OrdinalIgnoreCase));
        }
        public void DeleteColumn(string colName)
        {
            var col = GetColumn(colName);
            if(col != null)
            {
                int colIndex = Columns.IndexOf(col);
                foreach (var row in Rows)
                {
                    row.RemoveAt(colIndex);
                }
                Columns.Remove(col);
                DatabaseFileSystem.SaveTable(this, Database);
            }
        }
        public void AddRow()
        {
            List<string> row = new List<string>();
            for(int i = 0; i<Columns.Count; i++)
            {
                row.Add(null);
            }
            Rows.Add(row);
        }
        public void AddRows(List<string> rows, bool save = true)
        {
            Rows.Add(rows);
            if(save)
                DatabaseFileSystem.SaveTable(this, Database);
        }
        public void EditRow<T>(T value, string colName, int rowNum)
        {
            var col = GetColumn(colName);
            if (col == null) return;
            int colNum = Columns.IndexOf(col);
            if (col.TypeFullName.Contains("Invl"))
            {
                Invl invl = col.TypeFullName.Contains("String") ? Invl.stringInvl : Invl.charInvl;
                char from = col.TypeFullName.Split('(')[1].Substring(0, 1).ToCharArray()[0];
                char to = col.TypeFullName.Split(',')[1].Substring(0, 1).ToCharArray()[0];
                if (col.CheckValue(value.ToString(), invl, from, to))
                {
                    Rows[rowNum][colNum] = value.ToString();
                    DatabaseFileSystem.SaveTable(this, Database);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else if (col.CheckCast(value))
            {
                Rows[rowNum][colNum] = value.ToString();
                DatabaseFileSystem.SaveTable(this, Database);
            }
            else throw new InvalidCastException();
        }
        public void DeleteRow(int rowNum)
        {
            Rows.RemoveAt(rowNum);
            DatabaseFileSystem.SaveTable(this, Database);
        }
    }
}
