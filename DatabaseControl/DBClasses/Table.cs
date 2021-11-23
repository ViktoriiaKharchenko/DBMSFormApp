using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DatabaseControl.DBClasses;

namespace DatabaseControl
{
    public enum Invl { charInvl, stringInvl}
    public class Table : RestModelBase
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
            Column col = new Column(name, typeof(T).FullName);
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
            if(!CheckColumn(typeName)) throw new Exception("Unknown type");
            var names = Columns.FindAll(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (names.Count != 0) throw new Exception(string.Format("Column with name {0} already exists", name));

            Column col = new Column(name, typeName);
            Columns.Add(col);
            if (Rows.Count != 0)
            {
                foreach (var row in Rows)
                {
                    row.Add("");
                }
            }
            if(save)
                DatabaseFileSystem.SaveTable(this, Database);
        }
        public bool CheckColumn(string typeName) {

            Regex stringInvl = new Regex(@"StringInvl\({1,1}\w,\w\)");
            Regex charrgx = new Regex(@"CharInvl\({1,1}\w,\w\)");
            if (typeName.Contains("Invl") && !charrgx.IsMatch(typeName) && !stringInvl.IsMatch(typeName))
            {
                return false;
            }
            else if (!typeName.Contains("Invl"))
            {
                var type = Type.GetType(typeName);
                if (type == null)
                    return false;

            }
            return true;
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
                row.Add("");
            }
            Rows.Add(row);
        }
        public void AddRows(List<string> rows, bool save = true)
        {
            Rows.Add(rows);
            if(save)
                DatabaseFileSystem.SaveTable(this, Database);
        }
        public void AddRow(List<string> row)
        {
            if (row.Count > Columns.Count) throw new Exception("Row number does not match column number");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (row[i] == null) row[i] = "";
                if (!CheckRow(row[i], GetColumn(Columns[i].Name))) 
                    throw new Exception(string.Format("Wrong value {0} for column {1}", row[i], Columns[i].Name));
            }
            AddRows(row);
        }
        public List<string> GetRow(int num)
        {
            if (num >= Rows.Count) return null;
            return Rows[num];
        }
        public void EditRow<T>(T value, string colName, int rowNum)
        { 
            var col = GetColumn(colName);
            if (col == null) throw new Exception("Column does not exist");
            int colNum = Columns.IndexOf(col);
            if (CheckRow(value, col))
            {
                Rows[rowNum][colNum] = value.ToString();
                DatabaseFileSystem.SaveTable(this, Database);
            }
            else throw new InvalidCastException();
              
        }
        public bool CheckRow<T>(T value, Column col)
        {
            if (col.TypeFullName.Contains("Invl"))
            {
                Invl invl = col.TypeFullName.Contains("String") ? Invl.stringInvl : Invl.charInvl;
                char from = col.TypeFullName.Split('(')[1].Substring(0, 1).ToCharArray()[0];
                char to = col.TypeFullName.Split(',')[1].Substring(0, 1).ToCharArray()[0];
                if (col.CheckValue(value.ToString(), invl, from, to))
                    return true;
            }
            else if (col.CheckCast(value))
                return true;
            
            return false;
        }
        public void DeleteRow(int rowNum)
        {
            Rows.RemoveAt(rowNum);
            DatabaseFileSystem.SaveTable(this, Database);
        }
    }
}
