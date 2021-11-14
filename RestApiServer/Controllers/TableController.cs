using DatabaseControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiServer.Controllers
{
    [Route("api/database/{dbId}/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly DatabaseSystem context_;
        public TableController(DatabaseSystem context)
        {
            context_ = context;
        }
        [HttpGet]
        public JsonResult GetTables(int dbId)
        {
            var db = context_.GetDatabase(dbId);
            if(db==null) return new JsonResult(BadRequest("Database does not exist"));
            return new JsonResult(db.Tables);
        }
        [HttpGet("{id}")]
        public JsonResult GetTable(int dbId, int id)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            return new JsonResult(db.GetTable(id));
        }
        [HttpPost]
        public JsonResult CreateTable(int dbId, [FromBody] Table table)
        {
            try
            {
               var db = context_.GetDatabase(dbId);
                db.AddTable(table);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok());
        }
        [HttpPost("join")]

        public JsonResult JoinTables(int dbId, [FromBody] JObject data)
        {
            Table table;
            try
            {
                var db = context_.GetDatabase(dbId);
                int table1 = Int32.Parse(data["table1"].ToString());
                int table2 = Int32.Parse(data["table2"].ToString());
                string col1 = data["col1"].ToString();
                string col2 = data["col2"].ToString();
                var tabl1 = db.GetTable(table1);
                var tabl2 = db.GetTable(table2);
                if(tabl1.Columns.Find(t=>t.Name.Equals(col1)) == null) 
                    return new JsonResult(BadRequest(string.Format("Table {0} does not contain column {1}", tabl1.Name, col1)));
                if (tabl2.Columns.Find(t => t.Name.Equals(col2)) == null)
                    return new JsonResult(BadRequest(string.Format("Table {0} does not contain column {1}", tabl2.Name, col2)));
                table = db.JoinTables(tabl1.Name, tabl2.Name, col1, col2);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(table);
        }
        [HttpDelete("{id}")]
        public JsonResult DeleteDatabase(int dbId, int id)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(id);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            db.DeleteTable(table.Name, id);
            return new JsonResult(Ok());
        }
    }
}
