using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentDatabaseAPI.DAL;
using StudentDatabaseAPI.Models;

namespace StudentDatabaseAPI.Controllers
{

    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private UserRepository ur;
        public StudentsController()
        {
            ur = new UserRepository();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SearchResult> Get(string[] searchWords)
        {
                return ur.SearchUsers(searchWords);
         
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return ur.GetSingleUser(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User newUser)
        {

            ur.InsertUser(newUser);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string imageURL)
        {
            ur.InsertImage(imageURL, id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
