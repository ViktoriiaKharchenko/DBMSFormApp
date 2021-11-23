using DatabaseControl;
using DatabaseControl.DBClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private LinkGenerator linkGenerator_;

        public TableController(DatabaseSystem context, LinkGenerator linkGenerator)
        {
            context_ = context;
            linkGenerator_ = linkGenerator;
        }
        /// <summary>
        /// Get tables
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTables(int dbId)
        {
            var db = context_.GetDatabase(dbId);
            if(db==null) return new JsonResult(BadRequest("Database does not exist"));
            foreach(var tbl in db.Tables)
            {
                tbl.Links = CreateTableLinks(nameof(GetTables), dbId, tbl.Id);
            }
            return new JsonResult(db.Tables);
        }
        /// <summary>
        /// Get table
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="id" example="1">Table id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetTable(int dbId, int id)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(id);
            table.Links = CreateTableLinks(nameof(GetTable), dbId, id);
            return new JsonResult(table);
        }
        /// <summary>
        /// Create table
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateTable(int dbId, [FromBody] Table table)
        {
            Table tbl;
            try
            {
               var db = context_.GetDatabase(dbId);
                db.AddTable(table);
                tbl = db.GetTable(table.Name);
                tbl.Links = CreateTableLinks(nameof(CreateTable), dbId, tbl.Id);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(tbl);
        }
        /// <summary>
        /// Join tables
        /// </summary>
        /// <param name="dbId" example="1">Database id</param>
        /// <param name="data"></param>
        /// <returns></returns>
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
                table.Links = CreateTableLinks(nameof(JoinTables), dbId, -1, table1, table2);
            }
            catch (Exception e)
            {
                return new (StatusCode(500, e.Message));
            }
            return new JsonResult(table);
        }
        /// <summary>
        /// Delete database
        /// </summary>
        /// <param name="dbId" example="2">Database id</param>
        /// <param name="id" example ="3">Table id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult DeleteTable(int dbId, int id)
        {
            var db = context_.GetDatabase(dbId);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            var table = db.GetTable(id);
            if (table == null) return new JsonResult(BadRequest("Table does not exist"));
            db.DeleteTable(table.Name, id);
            var links = CreateTableLinks(nameof(DeleteTable), dbId, id);
            return new JsonResult(links);
        }

        private List<Link> CreateTableLinks(string method, int dbId, int id, int firstTable = 0, int secondTable = 0)
        {
            if (method == nameof(JoinTables))
            {
                var link = new List<Link>
                {
                    new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(JoinTables), values: new { dbId }),
                    method == nameof(JoinTables) ? "self" : "join_tables",
                    "POST"),
                    new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetTables), values: new { dbId}),
                    method == nameof(GetTables) ? "self" : "tables_get",
                    "GET")
                };
                return link;
            }

            var links = new List<Link>
            {
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetTables), values: new { dbId }),
                method == nameof(GetTables) ? "self" : "tables_get",
                "GET"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(CreateTable), values: new { dbId }),
                method == nameof(CreateTable) ? "self" : "table_post",
                "POST"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(DeleteTable), values: new { dbId, id }),
                method == nameof(DeleteTable) ? "self" : "table_delete",
                "DELETE"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(JoinTables), values: new { dbId }),
                method == nameof(JoinTables) ? "self" : "join_tables",
                 "POST")
            };
            if (method != nameof(DeleteTable))
            {
                links.Insert(1, new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetTable), values: new { dbId, id }),
                   method == nameof(GetTable) ? "self" : "table_get",
                   "GET"));
            }
            return links;
        }
    }
}
