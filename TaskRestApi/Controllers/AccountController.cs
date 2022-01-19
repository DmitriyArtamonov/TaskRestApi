using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AppCon;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace TaskRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<ActionResult> Login(string email, string password) //ok 
        {
            var Check = db.Persons.ToList();
            foreach (Person tmp in Check)
            {
                if ((tmp.email == email) && (tmp.password == password))
                {
                    return new JsonResult(tmp);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult> Reg(Person user) //ok
        {
            var Check = db.Persons.ToList();
            foreach (Person tmp in Check)
            {
                if ((tmp.email == user.email)||(tmp.name == user.name)) 
                {
                    return BadRequest();
                }
            }
            db.Persons.Add(user);
            await db.SaveChangesAsync();                                                          // ctrl + k + c             ctrl + k + u
            return Ok(user);                 
        }
        [HttpPut]
        public async Task<ActionResult> Put(Person user) //ok
        {
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}