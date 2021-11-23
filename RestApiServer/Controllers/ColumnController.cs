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
        private LinkGenerator linkGenerator_;

        public ColumnController(DatabaseSystem context, LinkGenerator linkGenerator)
        {
            context_ = context;
            linkGenerator_ = linkGenerator;
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
            foreach(var col in table.Columns)
            {
                col.Links = CreateColumnLinks(nameof(GetColumns), dbId, tblId, col.Name);
            }
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
            var column = table.GetColumn(name);
            column.Links = CreateColumnLinks(nameof(GetColumn), dbId, tblId, name);
            return new JsonResult(column);
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
                column.Links = CreateColumnLinks(nameof(CreateColumn), dbId, tblId, column.Name);
                table.AddColumn(column.Name, column.TypeFullName);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(column);
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
            var links = CreateColumnLinks(nameof(DeleteColumn), dbId, tblId, name);
       
            return links;
        }
        private List<Link> CreateColumnLinks(string method, int dbId, int tblId, string name)
        {
            var links = new List<Link>
            {
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetColumns), values: new { dbId, tblId }),
                method == nameof(GetColumns) ? "self" : "columns_get",
                "GET"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(DeleteColumn), values: new { dbId, tblId, name }),
                method == nameof(DeleteColumn) ? "self" : "column_delete",
                "DELETE")
            };
            if (method != nameof(DeleteColumn)) {
                links.Add(new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetColumn), values: new { dbId, tblId, name }),
                   method == nameof(GetColumn) ? "self" : "column_get",
                   "GET"));
                links.Add(new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(CreateColumn), values: new { dbId, tblId }),
                method == nameof(CreateColumn) ? "self" : "column_post",
                "POST"));
            }
            return links;
        }

    }
}
