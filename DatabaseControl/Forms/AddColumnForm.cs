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
    public partial class AddColumnForm : Form
    {
        private Table table;
        private DataGridView dbGridView;
        private DataTable dataTable;
        public AddColumnForm()
        {
            InitializeComponent();
        }
        public void PassValue(Table table, DataGridView view, DataTable dataTable)
        {
            dbGridView = view;
            this.table = table;
            this.dataTable = dataTable;
        }
        private void Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                DataColumn column = new DataColumn(ColumnName.Text);
                column.DataType = TypeVal.Text.Contains("Invl") ?
                    (TypeVal.Text.Contains("Char") ? typeof(char) : typeof(string)) : Type.GetType(TypeVal.Text);
                table.AddColumn(ColumnName.Text, TypeVal.Text);
                dataTable.Columns.Add(column);
                if (table.Rows.Count == 0) 
                { 
                    table.AddRow();
                    dataTable.Rows.Add();
                }
                Close();
                dbGridView.DataSource = dataTable;
                dbGridView.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
            catch (Exception)
            {
               MessageBox.Show("Invalid Type Exception.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
