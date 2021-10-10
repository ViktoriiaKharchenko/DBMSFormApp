using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseControl
{
    public partial class TableForm : Form
    {
        private Table currentTable;
        private DataTable myTable;
        public TableForm(Table table)
        {
            InitializeComponent();
            currentTable = table;
            GenerateTable();
        }
        private void GenerateTable()
        {
            myTable = new DataTable();
            for (int i = 0; i < currentTable.Columns.Count; i++)
            {
                DataColumn myColumn = new DataColumn(currentTable.Columns[i].Name);
                myColumn.DataType = currentTable.Columns[i].TypeFullName.Contains("Invl") ?
                            (currentTable.Columns[i].TypeFullName.Contains("Char") ? typeof(char) : typeof(string)) :
                            Type.GetType(currentTable.Columns[i].TypeFullName);
                myColumn.ReadOnly = false;
                myTable.Columns.Add(myColumn);
            }
            for (int i = 0; i < currentTable.Rows.Count; i++)
            {
                var newRow = myTable.NewRow();
                for (int j = 0; j < currentTable.Columns.Count; j++)
                {
                    var value = currentTable.Rows[i][j]?.Replace('.', ',');
                    if (value != null && value != "")
                    {
                        string type = currentTable.Columns[j].TypeFullName;
                        newRow[currentTable.Columns[j].Name] = Convert.ChangeType(value, type.Contains("Invl") ?
                            (type.Contains("Char") ? typeof(char) : typeof(string)) : Type.GetType(type));
                    }
                }
                myTable.Rows.Add(newRow);
            }
            dataGridView2.DataSource = myTable;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
