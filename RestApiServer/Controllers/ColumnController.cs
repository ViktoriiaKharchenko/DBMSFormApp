using DatabaseControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiServer.Controllers
{
    [Route("api/database/{dbId}/table/{tblId}/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly DatabaseSystem context_;
        public ColumnController(DatabaseSystem context)
        {
            context_ = context;
        }
        [HttpGet]
        public JsonResult GetColumns(int dbId, int tblId)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.Columns);
        }
        [HttpGet("{name}")]
        public JsonResult GetTable(int dbId, int tblId, string name)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.GetColumn(name));
        }
        [HttpPost]
        public JsonResult CreateColumn(int dbId, int tblId, [FromBody] Column column)
        {
            try
            {
                var db = context_.GetDatabase(dbId);
                var table = db.GetTable(tblId);
                table.AddColumn(column.Name, column.TypeFullName);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok());
        }
        [HttpDelete("{name}")]
        public JsonResult DeleteDatabase(int dbId, int tblId, string name)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            if (table.GetColumn(name) == null) return new JsonResult(BadRequest("Column does not exist"));
            table.DeleteColumn(name);
            return new JsonResult(Ok());
        }

    }
}
