using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AppCon;
using System.Threading.Tasks;

namespace TaskRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        ApplicationContext db;
        public ArticleController(ApplicationContext context)
        {
            db = context;
            if (!db.Articles.Any())
            {
                db.Articles.Add(new Article { Name = "something", Text = "a lot of something", Author = "ADMIN" });
                db.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostArticle(Article article)    //ok
        {
            var Check = db.Articles.ToList();
            foreach (Article tmp in Check)
            {
                if (tmp.Name == article.Name)
                {
                    return BadRequest();
                }
            }
            db.Articles.Add(article);
            await db.SaveChangesAsync();
            return Ok();
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Article>>> Get() //ok
        //{
        //    return await db.Articles.ToListAsync();
        //}
        [HttpGet("{name}")]
        public async Task<ActionResult> GetArticle(string name)   //ok
        {
            Article article = db.Articles.FirstOrDefault(x => x.Name == name);
            return Ok(article);
        }
        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)   //ok
        {
            Article article = db.Articles.FirstOrDefault(x => x.Name == name);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
