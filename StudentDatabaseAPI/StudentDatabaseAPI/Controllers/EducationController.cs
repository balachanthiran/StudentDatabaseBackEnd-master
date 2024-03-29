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
    [Route("api/Education")]
    public class EducationController : Controller
    {
        private UserRepository userRepo;

        public EducationController()
        {
            userRepo = new UserRepository();
        }
        // GET: api/Education
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Education/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Education
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Education/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UpdateEducation newEducation)
        {
            userRepo.UpdateEducation(id, newEducation);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
