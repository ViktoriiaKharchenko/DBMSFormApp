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
    [Route("api/database/{dbId}/table/{tblId}/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private readonly DatabaseSystem context_;
        public RowController(DatabaseSystem context)
        {
            context_ = context;
        }
        [HttpGet]
        public JsonResult GetRows(int dbId, int tblId)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.Rows);
        }
        [HttpGet("{num}")]
        public JsonResult GetRow(int dbId, int tblId, int num)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.GetRow(num));
        }
        [HttpPost]
        public JsonResult CreateRow(int dbId, int tblId, [FromBody] JObject data)
        {
            List<string> row = new List<string>();
            int num = 0;
            try
            {
                var db = context_.GetDatabase(dbId);
                var table = db.GetTable(tblId);
                foreach (var col in table.Columns)
                {
                    row.Add(data[col.Name]?.ToString());
                    num += data[col.Name] != null ? 1 : 0;
                }
                if (num != data.Count) return new JsonResult(BadRequest("Unknown columns"));
                table.AddRow(row);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok());
        }
        [HttpDelete("{num}")]
        public JsonResult DeleteDatabase(int dbId, int tblId, int num)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            if (table.GetRow(num) == null) return new JsonResult(BadRequest("Row does not exist"));
            table.DeleteRow(num);
            return new JsonResult(Ok());
        }
    }
}
