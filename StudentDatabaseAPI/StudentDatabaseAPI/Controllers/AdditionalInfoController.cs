using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDatabaseAPI.DAL;
using StudentDatabaseAPI.Models;

namespace StudentDatabaseAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/AdditionalInfo")]
    public class AdditionalInfoController : Controller
    {
        private UserRepository ur;

        public AdditionalInfoController()
        {
            ur = new UserRepository();
        }
        // GET: api/AdditionalInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "skills", "value2" };
        }

        // GET: api/AdditionalInfo/5
        [HttpGet("{id}")]
        public AdditionalInfo Get(int id)
        {
            return ur.getUserInfo(id);
        }

        // POST: api/AdditionalInfo
        [HttpPost]
        public void Post([FromBody]AdditionalInfo info)
        {
            ur.InsertAdditionalInfo(info);
        }

        // PUT: api/AdditionalInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
