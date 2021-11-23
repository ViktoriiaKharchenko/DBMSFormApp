using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseControl;
using System.IO;
using DatabaseControl.DBClasses;
using Microsoft.AspNetCore.Routing;

namespace RestApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseSystem context_;
        private LinkGenerator linkGenerator_;

        public DatabaseController(DatabaseSystem context, LinkGenerator linkGenerator)
        {
            context_ = context;
            linkGenerator_ = linkGenerator;
        }
        /// <summary>
        /// Get databases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDatabases()
        {
            foreach(var db in context_.Databases)
            {
                db.Links = CreateDatabaseLinks(nameof(GetDatabases), db.Id);
            }
            return new JsonResult(context_.Databases);
        }
        /// <summary>
        /// Get database
        /// </summary>
        /// <param name="id" example="2">Database id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetDatabase(int id)
        {
            var db = context_.GetDatabase(id);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            db.Links = CreateDatabaseLinks(nameof(GetDatabase), db.Id);
            return new JsonResult(db);
        }
        /// <summary>
        /// Create database
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateDatabase([FromBody] Database db)
        {
            Database database;
            try
            {
                foreach(var tbl in db.Tables)
                {
                    tbl.Database = db.Name;
                }
                context_.AddDatabase(db);
                database = context_.GetDatabase(db.Name);
                database.Links = CreateDatabaseLinks(nameof(CreateDatabase), database.Id);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(database);
        }
        /// <summary>
        /// Delete database
        /// </summary>
        /// <param name="id" example="3">Database id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult DeleteDatabase(int id)
        {
            var db = context_.GetDatabase(id);
            if (db == null) return new JsonResult(BadRequest("Database does not exist"));
            context_.DeleteDatabase(db.Name, id);
            var links = CreateDatabaseLinks(nameof(DeleteDatabase), id);
            return new JsonResult(links);
        }

        private List<Link> CreateDatabaseLinks(string method, int id)
        {
            var links = new List<Link>
            {
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetDatabases)),
                method == nameof(GetDatabases) ? "self" : "databases_get",
                "GET"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(CreateDatabase)),
                method == nameof(CreateDatabase) ? "self" : "database_post",
                "POST"),
                new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(DeleteDatabase), values: new { id}),
                method == nameof(DeleteDatabase) ? "self" : "database_delete",
                "DELETE")
            };
            if (method != nameof(DeleteDatabase))
            {
                links.Insert(1, new Link(linkGenerator_.GetUriByAction(HttpContext, nameof(GetDatabase), values: new { id }),
                   method == nameof(GetDatabase) ? "self" : "database_get",
                   "GET"));
            }
            return links;
        }

    }
}
