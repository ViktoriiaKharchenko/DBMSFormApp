using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DatabaseControl;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Before()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Database";
            DatabaseFileSystem.SetPath(path);
        }
        [TestMethod]
        public void TestDB()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            var db2 = dbSystem.GetDatabase(dbName);
            Assert.IsNotNull(db);
            Assert.AreEqual(dbName, db.Name);
            Assert.AreEqual(db, db2);
            dbSystem.DeleteDatabase(dbName, db.Id);
            Assert.IsNull(dbSystem.GetDatabase(dbName));
        }

        [TestMethod]
        public void TestTable()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            string tableName = "Test Table";
            var table = db.AddTable(tableName);
            var table2 = db.GetTable(tableName);
            Assert.IsNotNull(table);
            Assert.AreEqual(tableName, table.Name);
            Assert.AreEqual(table, table2);
            db.DeleteTable(tableName, table.Id);
            Assert.IsNull(db.GetTable(tableName));
        }

        [TestMethod]
        public void TestColumn()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            string tableName = "Test Table";
            var table = db.AddTable(tableName);
            string columnName = "Test column";
            table.AddColumn<int>(columnName);
            var column = table.GetColumn(columnName);
            Assert.IsNotNull(column);
            Assert.AreEqual(columnName, column.Name);
            Assert.AreEqual(typeof(int).FullName, column.TypeFullName);
            table.DeleteColumn(columnName);
            Assert.IsNull(table.GetColumn(columnName));
        }

        [TestMethod]
        public void TestRow()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            string tableName = "Test Table";
            var table = db.AddTable(tableName);
            string columnName = "Test column";
            table.AddColumn<int>(columnName);
            table.AddRow();
            Assert.AreEqual(table.Rows.Count, 1);
            int rowValue = 3;
            table.EditRow(rowValue, columnName, 0);
            Assert.AreEqual(rowValue.ToString(), table.Rows[0][0]);
            table.DeleteRow(0);
            Assert.AreEqual(table.Rows.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestInvalidRowValue()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            string tableName = "Test Table";
            var table = db.AddTable(tableName);
            table.AddColumn<float>("Test column");
            table.AddRow();
            table.EditRow('f', "Test column", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestInterval()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            string dbName = "Test Database";
            var db = dbSystem.AddDatabase(dbName);
            string tableName = "Test Table";
            var table = db.AddTable(tableName);
            table.AddColumn("Test column","StringInvl(c,l)");
            table.AddRow();
            table.EditRow("ccclzzz", "Test column", 0);
        }

        [TestMethod]
        public void TableJoin()
        {
            DatabaseSystem dbSystem = new DatabaseSystem();
            DatabaseFileSystem.LoadDatabases(dbSystem);
            var db = dbSystem.GetDatabase("Products");
            var joinedTable = db.JoinTables("Product", "Categories", "CategoryId", "CatId");
            Assert.IsNotNull(joinedTable);
            Assert.AreEqual(joinedTable.Rows.Count, 1);
            Assert.AreEqual(joinedTable.Columns.Count, 3);
        }
    }
}
