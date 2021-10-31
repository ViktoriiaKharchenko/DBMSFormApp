using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseControl
{
    public class Database
    {
        private int tableKey = 0;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Table> Tables { get; private set; }
        public Database (string name, int key)
        {
            Name = name;
            Id = key;
            Tables = new List<Table>();
        }
        public Table AddTable(string name, bool save = true)
        {
            Table table = new Table(name, tableKey++)
            {
                Database = Name
            };
            if(save)
                DatabaseFileSystem.SaveTable(table, Name);
            Tables.Add(table);
            return table;
        }
        public void AddTable(Table table)
        {
            if (!CheckTable(table)) throw new Exception("Invalid table parameters");
            var newTable = AddTable(table.Name);
            foreach (var column in table.Columns)
            {
                newTable.AddColumn(column.Name, column.TypeFullName);
            }
            foreach (var row in table.Rows)
            {
                newTable.AddRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    newTable.EditRow(row[i], table.Columns[i].Name, table.Rows.IndexOf(row));
                }
            }
        }
        public bool CheckTable (Table table)
        {
            foreach (var column in table.Columns)
            {
                var names = table.Columns.FindAll(t => t.Name.Equals(table.Name, StringComparison.OrdinalIgnoreCase));
                if (names.Count != 0) throw new Exception(string.Format("Column with name {0} already exists", table.Name));
                if (!table.CheckColumn(column.TypeFullName)) throw new Exception("Unknown column type");
            }
            foreach (var row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (row[i] == null) row[i] = "";
                    if (!table.CheckRow(row[i], table.GetColumn(table.Columns[i].Name)))
                        throw new Exception(string.Format("Wrong value {0} for column {1}", row[i], table.Columns[i].Name));
                }
            }
            return true;
        }
        public Table GetTable(string name)
        {
            return Tables.FindLast(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public Table GetTable(string name, int id)
        {
            return Tables.FindLast(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && t.Id == id);
        }
        public Table GetTable(int id)
        {
            return Tables.FindLast(t => t.Id == id);
        }
        public void DeleteTable (string name, int id)
        {
            var table = GetTable(name, id);
            DatabaseFileSystem.DeleteTable(table, Name);
            Tables.Remove(table);
        }
        public Table JoinTables(string table1, string table2, string column1, string column2)
        {
            var firstTable = GetTable(table1);
            var secondTable = GetTable(table2);
            if (firstTable == null || secondTable == null) return null;
            var table = new Table(table1 + "&" + table2, -1);
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
            return table;
        }
    }
}
