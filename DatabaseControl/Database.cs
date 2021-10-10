using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseControl
{
    public class Database
    {
        private static int uniqKey = 0;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Table> Tables { get; private set; }
        public Database (string name)
        {
            Name = name;
            Id = uniqKey++;
            Tables = new List<Table>();
        }
        public Table AddTable(string name, bool save = true)
        {
            Table table = new Table(name)
            {
                Database = this.Name
            };
            if(save)
                DatabaseFileSystem.SaveTable(table, Name);
            Tables.Add(table);
            return table;
        }
        public Table GetTable(string name)
        {
            return Tables.FindLast(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public void DeleteTable (string name)
        {
            var table = GetTable(name);
            DatabaseFileSystem.DeleteTable(table, Name);
            Tables.Remove(table);
        }
        public Table JoinTables(string table1, string table2, string column1, string column2)
        {
            var firstTable = GetTable(table1);
            var secondTable = GetTable(table2);
            if (firstTable == null || secondTable == null) return null;
            var table = AddTable(table1 + "&" + table2, false);
            foreach (var col in firstTable.Columns)
            {
                if (!col.Name.Equals(column1, StringComparison.OrdinalIgnoreCase))
                    table.AddColumn(col.Name, col.TypeFullName, false);
            }
            foreach (var col in secondTable.Columns)
            {
                if (!col.Name.Equals(column2, StringComparison.OrdinalIgnoreCase))
                    table.AddColumn(col.Name, col.TypeFullName, false);
            }
            int colIndex1 = firstTable.Columns.IndexOf(firstTable.GetColumn(column1));
            int colIndex2 = secondTable.Columns.IndexOf(secondTable.GetColumn(column2));
            foreach (var row in secondTable.Rows) 
            {
                var rows = firstTable.Rows.FindAll(r => r[colIndex1] == row[colIndex2]);
                foreach (var r in rows)
                {
                    var tableRow = new List<string>();
                    tableRow.AddRange(r);
                    tableRow.AddRange(row);
                    tableRow.RemoveAt(r.Count + colIndex2);
                    tableRow.RemoveAt(colIndex1);
                    table.AddRows(tableRow, false);
                }
            }
            //DatabaseFileSystem.SaveTable(table, Name);
            return table;
        }
    }
}
