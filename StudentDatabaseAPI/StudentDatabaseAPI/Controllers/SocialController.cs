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
    [Route("api/Social")]
    public class SocialController : Controller
    {
        private UserRepository ur;

        public SocialController()
        {
            ur = new UserRepository();
        }
        // GET: api/Social
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Social/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Social
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Social/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UpdateSocial newSocial)
        {
            ur.UpdateSocial(id, newSocial);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
