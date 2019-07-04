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
    [Route("api/Language")]
    public class LanguageController : Controller
    {
        private UserRepository ur;

        public LanguageController()
        {
            ur = new UserRepository();
        }
        // GET: api/Language
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Language/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Language
        [HttpPost]
        public int Post([FromBody]Language newLanguage)
        {
            return ur.AddLanguage(newLanguage);
        }
        
        // PUT: api/Language/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UpdateLanguage newValue)
        {
            ur.UpdateLanguage(id, newValue);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id, int languageID)
        {
            ur.DeleteLanguage(id, languageID);
        }
    }
}
