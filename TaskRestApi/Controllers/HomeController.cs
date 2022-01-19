using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AppCon;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
            if (!db.Persons.Any())
            {
                db.Persons.Add(new Person {name = "ADMIN", email = "tmpmail@yandex.ru", password = "GVFH88elzS6GVYx9mf", date = "24.11.2021"});
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get() //ok
        {
            return await db.Persons.ToListAsync();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) //ok
        {
            Person user = db.Persons.FirstOrDefault(x => x.Id == id);
            db.Persons.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}