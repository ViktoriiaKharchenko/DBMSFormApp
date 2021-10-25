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
    public partial class JoinTablesForm : Form
    {
        Database database;
        public JoinTablesForm(Database db)
        {
            InitializeComponent();
            database = db;
            Table1.DropDownStyle = ComboBoxStyle.DropDownList;
            Table2.DropDownStyle = ComboBoxStyle.DropDownList;
            Column1.DropDownStyle = ComboBoxStyle.DropDownList;
            Column2.DropDownStyle = ComboBoxStyle.DropDownList;
            Table1.Items.AddRange(db.Tables.Select(t=>t.Name).ToArray());
            Table2.Items.AddRange(db.Tables.Select(t => t.Name).ToArray());
        }

        private void Join_Click(object sender, EventArgs e)
        {
            string selectedTable1 = Table1.SelectedItem?.ToString();
            var table1 = database.GetTable(selectedTable1);
            string selectedTable2 = Table2.SelectedItem?.ToString();
            var table2 = database.GetTable(selectedTable2);
            string selectedColumn1 = Column1.SelectedItem?.ToString();
            var column1 = table1?.GetColumn(selectedColumn1);
            string selectedColumn2 = Column2.SelectedItem?.ToString();
            var column2 = table2?.GetColumn(selectedColumn2);
            var newTable = database.JoinTables(table1?.Name, table2?.Name, column1?.Name, column2?.Name);
            if (newTable == null) return;
            var form = new TableForm(newTable);
            form.Show();
        }

        private void Table1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = Table1.SelectedItem.ToString();
            var table = database.GetTable(selectedTable);
            if(table != null)
            {
                Column1.Items.Clear();
                Column1.SelectedIndex = -1;
                Column1.Items.AddRange(table.Columns.Select(t => t.Name).ToArray());
            }
        }

        private void Table2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = Table2.SelectedItem.ToString();
            var table = database.GetTable(selectedTable);
            if (table != null)
            {
                Column2.Items.Clear();
                Column2.SelectedIndex = -1;
                Column2.Items.AddRange(table.Columns.Select(t => t.Name).ToArray());
            }

        }
    }
}
