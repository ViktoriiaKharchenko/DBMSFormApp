using DatabaseControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiServer.Models;
using Microsoft.AspNetCore.Routing;
using DatabaseControl.DBClasses;

namespace RestApiServer.Controllers
{
    [Route("api/database/{dbId}/table/{tblId}/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private readonly DatabaseSystem context_;
        private LinkGenerator linkGenerator_;

        public RowController(DatabaseSystem context, LinkGenerator linkGenerator)
        {
            context_ = context;
            linkGenerator_ = linkGenerator;

        }
        /// <summary>
        /// Get rows
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="0">Table id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRows(int dbId, int tblId)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));

            List<RowModel> rows = new List<RowModel>();
            for( int i = 0; i< table.Rows.Count; i++)
            {
                var rowModel = new RowModel();
                rowModel.Row = table.Rows[i];
                rowModel.Links = CreateRowLinks(nameof(GetRows), dbId, tblId, i);
                rows.Add(rowModel);
            }
            return new JsonResult(rows);
        }
        /// <summary>
        /// Get row
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="0">Table id</param>
        /// <param name="num" example="2">Row number</param>
        /// <returns></returns>
        [HttpGet("{num}")]
        public JsonResult GetRow(int dbId, int tblId, int num)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            var row = table.GetRow(num);
            var rowModel = new RowModel()
            {
                Row = row,
                Links = CreateRowLinks(nameof(GetRow), dbId, tblId, num)
            };
            return new JsonResult(rowModel);
        }
        /// <summary>
        /// Create row
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="0">Table id</param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateRow(int dbId, int tblId, [FromBody] JObject data)
        {
            List<string> row = new List<string>();
            int num = 0;
            var rowModel = new RowModel();
            try
            {
                var db = context_.GetDatabase(dbId);
                if (db == null) return new JsonResult(BadRequest("Database does not exist"));
                var table = db.GetTable(tblId);
                if (table == null) return new JsonResult(BadRequest("Table does not exist"));
                foreach (var col in table.Columns)
                {
                    row.Add(data[col.Name]?.ToString());
                    num += data[col.Name] != null ? 1 : 0;
                }
                if (num != data.Count) return new JsonResult(BadRequest("Unknown columns"));
                table.AddRow(row);
                rowModel.Row = row;
                rowModel.Links = CreateRowLinks(nameof(CreateRow), dbId, tblId, table.Rows.IndexOf(row));
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(rowModel);
        }
        /// <summary>
        /// Delete row
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="tblId" example="0">Table id</param>
        /// <param name="num" example="2">Row number</param>
        /// <returns></returns>
        [HttpDelete("{num}")]
        public JsonResult DeleteRow(int dbId, int tblId, int num)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(tblId);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            if (table.GetRow(num) == null) return new JsonResult(BadRequest("Row does not exist"));
            table.DeleteRow(num);
            var links = CreateRowLinks(nameof(DeleteRow), dbId, tblId, num);
            return new JsonResult(links);
        }
        private List<Link> CreateRowLinks(string method, int dbId, int tblId, int num )
        {
            var links = new List<Link>
            {
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetRows), values: new { dbId, tblId }),
                method == nameof(GetRows) ? "self" : "rows_get",
                "GET"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(DeleteRow), values: new { dbId, tblId, num }),
                method == nameof(DeleteRow) ? "self" : "row_delete",
                "DELETE")
            };
            if (method != nameof(DeleteRow))
            {
                links.Add(new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetRow), values: new { dbId, tblId, num }),
                   method == nameof(GetRow) ? "self" : "row_get",
                   "GET"));
                links.Add(new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(CreateRow), values: new { dbId, tblId }),
                method == nameof(CreateRow) ? "self" : "row_post",
                "POST"));
            }
            return links;
        }
    }
}
