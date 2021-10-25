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
    public partial class AddDBForm : Form
    {
        public DatabaseSystem dbSystem;
        private DataGridView dbGridView;
        private Database db;
        public AddDBForm()
        {
            InitializeComponent();
        }
        public void PassValue(DatabaseSystem databaseSystem, DataGridView view)
        {
            dbSystem = databaseSystem;
            dbGridView = view;
            Confirm.Click += new EventHandler(ConfirmDB_Click);

        }
        public void PassValue(Database database, DataGridView view)
        {
            db = database;
            dbGridView = view;
            Confirm.Click += new EventHandler(ConfirmTable_Click);

        }
        private void ConfirmDB_Click(object sender, EventArgs e)
        {
            if (DbName.Text != "")
            {
                dbSystem.AddDatabase(DbName.Text);
                Close();
                var bindingSource1 = new BindingSource { DataSource = dbSystem.Databases };
                dbGridView.DataSource = bindingSource1;
            }
        }
        private void ConfirmTable_Click(object sender, EventArgs e)
        {
            if (DbName.Text != "")
            {
                db.AddTable(DbName.Text);
                Close();
                var bindingSource1 = new BindingSource { DataSource = db.Tables };
                dbGridView.DataSource = bindingSource1;
            }
        }
    }
}
