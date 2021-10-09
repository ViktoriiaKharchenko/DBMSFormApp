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
    public partial class Form1 : Form
    {
        private DatabaseSystem databaseSystem;
        private Database currentDatabase;
        private Table currentTable;
        private DataTable myTable;
        public Form1()
        {
            InitializeComponent();
            databaseSystem = new DatabaseSystem();
            AddRow.Visible = false;
            DeleteRow.Visible = false;
            DatabaseFileSystem.LoadDatabases(databaseSystem);
            Load += new EventHandler(Form1_Load);
        }
        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            dataGridView1.DataSource = databaseSystem.Databases;
        }

        private void AddDB_Click(object sender, EventArgs e)
        {
            var popup = new AddDBForm();
            popup.PassValue(databaseSystem, dataGridView1);
            popup.Show();
        }

        private void DeleteDB_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                if (dataGridView1.SelectedRows[i].Cells[1].Value != null)
                {
                    var dbName = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                    if (MessageBox.Show(string.Format("Are you sure you want to delete {0} database?",dbName ), "Message", MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        databaseSystem.DeleteDatabase(dbName); 
                    }
                }
            }
            var bindingSource1 = new BindingSource { DataSource = databaseSystem.Databases };
            dataGridView1.DataSource = bindingSource1;
        }

        private void DBSystem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            var dbName = dataGridView1.Rows[row].Cells[1].Value;
            currentDatabase = databaseSystem.GetDatabase(dbName.ToString());
            dataGridView1.AllowUserToAddRows = false;
            Back.Click += new EventHandler(Back_Click);
            button1.Click -= new EventHandler(AddDB_Click);
            button2.Click -= new EventHandler(DeleteDB_Click);
            button1.Click += new EventHandler(AddTable_Click);
            button2.Click += new EventHandler(DeleteTable_CLick);
            dataGridView1.CellContentClick -= new DataGridViewCellEventHandler(DBSystem_CellContentClick);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(DB_CellContentClick);
            var bindingSource1 = new BindingSource { DataSource = currentDatabase.Tables };
            dataGridView1.DataSource = bindingSource1;

        }
        private void DB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            var tableName = dataGridView1.Rows[row].Cells[1].Value;
            currentTable = currentDatabase.GetTable(tableName.ToString());
            dataGridView1.CellContentClick -= new DataGridViewCellEventHandler(DB_CellContentClick);
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dataGridView1.CellValueChanged+= new DataGridViewCellEventHandler(CellValueChanged);
            Back.Click += new EventHandler(Back_Click2);
            Back.Click -= new EventHandler(Back_Click);
            button1.Click -= new EventHandler(AddTable_Click);
            button2.Click -= new EventHandler(DeleteTable_CLick);
            button1.Click += new EventHandler(AddColumn_Click);
            button2.Click += new EventHandler(DeleteColumn_Click);
            button1.Text = "Add Column";
            button2.Text = "Delete Column";
            AddRow.Visible = true;
            DeleteRow.Visible = true;
            GenerateTable();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

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
                        newRow[currentTable.Columns[j].Name] = Convert.ChangeType(value, type.Contains("Invl")?
                            (type.Contains("Char") ? typeof(char) : typeof(string)) : Type.GetType(type));
                    }
                }
                myTable.Rows.Add(newRow);
            }
            dataGridView1.DataSource = myTable;
        }
        private void AddTable_Click(object sender, EventArgs e)
        {
            var popup = new AddDBForm();
            popup.PassValue(currentDatabase, dataGridView1);
            popup.Show();
        }
        private void AddColumn_Click(object sender, EventArgs e)
        {
            var popup = new AddColumnForm();
            popup.PassValue(currentTable, dataGridView1, myTable);
            popup.Show();
        }
        private void DeleteColumn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                var col = dataGridView1.SelectedCells[i].ColumnIndex;
                if (MessageBox.Show(string.Format("Are you sure you want to delete {0} table?", dataGridView1.Columns[col].Name), "Message", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    currentTable.DeleteColumn(dataGridView1.Columns[col].Name);
                }
            }
            GenerateTable();
        }
        private void AddRow_Click(object sender, EventArgs e)
        {
            currentTable.AddRow();
            myTable.Rows.Add();
            dataGridView1.DataSource = myTable;
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                currentTable.DeleteRow(dataGridView1.SelectedRows[i].Index);
                var gridRow = dataGridView1.SelectedRows[i];
                DataRow row = ((DataRowView)gridRow.DataBoundItem).Row;
                myTable.Rows.Remove(row);
            }
            dataGridView1.DataSource = myTable;
        }
        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var row = dataGridView1.Rows[e.RowIndex];
            var value = row.Cells[e.ColumnIndex].Value;
            var column = currentTable.GetColumn(dataGridView1.Columns[e.ColumnIndex].Name);
            try
            {
                currentTable.EditRow(value, column.Name, e.RowIndex);
                myTable.Rows[e.RowIndex][e.ColumnIndex] = value;
            }
            catch(InvalidCastException)
            {
                MessageBox.Show(string.Format("Invalid cast. Value should have {0} type", column.TypeFullName), 
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                row.Cells[e.ColumnIndex].Value = DBNull.Value;
            }
        }
        private void DeleteTable_CLick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                if (dataGridView1.SelectedRows[i].Cells[1].Value != null)
                {
                    var tableName = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                    if (MessageBox.Show(string.Format("Are you sure you want to delete {0} table?", tableName), "Message", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        currentDatabase.DeleteTable(tableName);
                    }
                }
            }
            var bindingSource1 = new BindingSource { DataSource = currentDatabase.Tables.Select(t => new { t.Id, t.Name })};
            dataGridView1.DataSource = bindingSource1;
        }
        private void Back_Click2(object sender, EventArgs e)
        {
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(DB_CellContentClick);
            Back.Click -= new EventHandler(Back_Click2);
            Back.Click += new EventHandler(Back_Click);
            button1.Click += new EventHandler(AddTable_Click);
            button2.Click += new EventHandler(DeleteTable_CLick);
            button1.Click -= new EventHandler(AddColumn_Click);
            button2.Click -= new EventHandler(DeleteColumn_Click);
            
            dataGridView1.CellValueChanged -= new DataGridViewCellEventHandler(CellValueChanged);
            AddRow.Visible = false;
            DeleteRow.Visible = false;
            button1.Text = "Add";
            button2.Text = "Delete";
            var bindingSource1 = new BindingSource { DataSource = currentDatabase.Tables };
            dataGridView1.DataSource = bindingSource1;
        }
        private void Back_Click(object sender, EventArgs e)
        {
            button1.Click += new EventHandler(AddDB_Click);
            button2.Click += new EventHandler(DeleteDB_Click);
            button1.Click -= new EventHandler(AddTable_Click);
            button2.Click -= new EventHandler(DeleteTable_CLick);
            Back.Click -= new EventHandler(Back_Click);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(DBSystem_CellContentClick);
            dataGridView1.CellContentClick -= new DataGridViewCellEventHandler(DB_CellContentClick);
            var bindingSource1 = new BindingSource { DataSource = databaseSystem.Databases };
            dataGridView1.DataSource = bindingSource1;
        }
    }
}
