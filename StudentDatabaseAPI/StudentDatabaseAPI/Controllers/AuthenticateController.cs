using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDatabaseAPI.DAL;
using Microsoft.AspNetCore.Authorization;
using StudentDatabaseAPI.Models;

namespace StudentDatabaseAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Authenticate")]
    public class AuthenticateController : Controller
    {
        private UserRepository userRepo;

        public AuthenticateController()
        {
            userRepo = new UserRepository();
        }

        // GET: api/Authenticate
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authenticate/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Authenticate
        [HttpPost]
        public int Post([FromBody] Login login)
        {
            int id = userRepo.AuthenticateUser(login.Email, login.Password);
            return id;
        }
        
        // PUT: api/Authenticate/5
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
