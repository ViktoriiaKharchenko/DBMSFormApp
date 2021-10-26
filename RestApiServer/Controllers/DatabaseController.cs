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
        [HttpGet]
        public JsonResult GetDatabases()
        {
            return new JsonResult(context_.Databases);
        }
        [HttpGet("{id}")]
        public JsonResult GetDatabase(int id)
        {
            return new JsonResult(context_.GetDatabase(id));
        }
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
