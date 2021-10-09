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
        private Database database;
        public TableForm()
        {
            InitializeComponent();
        }
        public void PassValue(Database db)
        {
            database = db;
        }

    }
}
