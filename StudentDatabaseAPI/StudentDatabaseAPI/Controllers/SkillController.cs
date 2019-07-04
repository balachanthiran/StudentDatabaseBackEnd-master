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
    [Route("api/Skill")]
    public class SkillController : Controller
    {
        private UserRepository ur;

        public SkillController()
        {
            ur = new UserRepository();
        }

        // GET: api/Skill
        [HttpGet]
        public IEnumerable<Skill> Get()
        {
            return ur.GetAllSkillNames();
        }

        // GET: api/Skill/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Skill
        [HttpPost]
        public int Post([FromBody]Skill newSkill)
        {
            return ur.AddSkill(newSkill);
        }
        
        // PUT: api/Skill/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UpdateSkill newValue)
        {
            ur.UpdateSkill(id, newValue);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id, int skillID)
        {
            ur.DeleteSkill(id, skillID);
        }
    }
}
