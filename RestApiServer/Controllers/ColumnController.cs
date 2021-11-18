using DatabaseControl;
using DatabaseControl.DBClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private LinkGenerator _linkGenerator;

        public ColumnController(DatabaseSystem context, LinkGenerator linkGenerator)
        {
            context_ = context;
            _linkGenerator = linkGenerator;
        }
        /// <summary>
        /// Get columns
        /// </summary>
        /// <remarks>Schema consists of columns with name and type</remarks>

        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="1">Table id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetColumns(int dbId, int tblId)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.Columns);
        }
        /// <summary>
        /// Get column
        /// </summary>
        /// <remarks>Schema consists of column with name and type</remarks>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="1">Table id</param>
        /// <param name="name" example="col1">Column name</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public JsonResult GetColumn(int dbId, int tblId, string name)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            return new JsonResult(table.GetColumn(name));
        }
        /// <summary>
        /// Create column
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example ="1">Table id</param>
        /// <param name="column" example="col1">Column name</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateColumn(int dbId, int tblId, [FromBody] Column column)
        {
            try
            {
                var db = context_.GetDatabase(dbId);
                if (db == null) return new JsonResult(BadRequest("Database does not exist"));
                var table = db.GetTable(tblId);
                if (table == null) return new JsonResult(BadRequest("Table does not exist"));
                table.AddColumn(column.Name, column.TypeFullName);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok());
        }

        /// <summary>
        /// Delete column
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="1">Table id</param>
        /// <param name="name" example="col1">Column name</param>
        /// <returns></returns>
        [HttpDelete("{name}")]
        public ActionResult<List<Link>> DeleteColumn(int dbId, int tblId, string name)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            if (table.GetColumn(name) == null) return new JsonResult(BadRequest("Column does not exist"));
            table.DeleteColumn(name);
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteColumn), values: new { dbId, tblId, name }),
                "self",
                "DELETE")
            };

            return links;
        }
        private List<Link> CreateLinksForTableColumn(string method, int dbId, string tblId, string name, int type = 0, string newName = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetColumn), values: new { dbId, tblId }),
                method == nameof(GetColumn) ? "self" : "table_get",
                "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateColumn), values: new { dbId, tblId, name }),
                method == nameof(CreateColumn) ? "self" : "table_post",
                "PUT"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteColumn), values: new { dbId, tblId, name }),
                method == nameof(DeleteColumn) ? "self" : "table_delete",
                "DELETE")
            };
            return links;
        }

    }
}
