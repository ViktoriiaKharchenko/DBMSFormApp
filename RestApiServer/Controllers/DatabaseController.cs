using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseControl;
using System.IO;

namespace RestApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseSystem context_;
        public DatabaseController(DatabaseSystem context)
        {
            context_ = context;
        }
        /// <summary>
        /// Get databases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDatabases()
        {
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
            try
            {
                context_.AddDatabase(db);
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok());
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
            return new JsonResult(Ok());
        }

     }
}
