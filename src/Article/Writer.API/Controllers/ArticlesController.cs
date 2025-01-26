using Article.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly List<ArticleModel> _articles;

        public ArticlesController()
        {
            _articles = new List<ArticleModel>
            {
                new ArticleModel { Id = 1, Name = "Article 1" },
                new ArticleModel { Id = 2, Name = "Article 2" },
                new ArticleModel { Id = 3, Name = "Article 3" },
                new ArticleModel { Id = 4, Name = "Article 4" }
            };
        }

        // in this endpoint, if you call it within 10 seconds after the first call, you will be using the caching data in ocelot.
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_articles);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ArticleModel article)
        {
            _articles.Add(article);
            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }
    }
}
