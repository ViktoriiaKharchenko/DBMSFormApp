using DatabaseControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
