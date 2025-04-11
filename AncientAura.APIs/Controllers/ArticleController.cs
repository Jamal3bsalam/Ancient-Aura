using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Service.Services.BookService;
using AncientAura.Service.Services.DocumetriesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Library - Articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticlesService _articleService;

        public ArticleController(IArticlesService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet("articles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ArticlesDto>>> GetAllArticles([FromQuery] SpecParameters parameters)
        {
            var articles = await _articleService.GetAllArticlesAsync(parameters);
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ArticleDto>> GetArticleById(int? id)
        {
            if (id == null) return BadRequest("invalid Operation");
            var article = await _articleService.GetArticleByIdAsync(id.Value);
            if (article == null) return NotFound();
            return Ok(article);
        }

        [HttpGet("reviews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReviewsDto>>> GetAllReviewsByArticleID([FromQuery] int id)
        {
            var documentry = await _articleService.GetArticleByIdAsync(id);
            if (documentry == null) return NotFound("Invalid BookId");
            var reviews = await _articleService.GetAllReviewsForArticleById(id);
            if (reviews == null) return BadRequest("The Doumentry Has No Reviews");
            return Ok(reviews);
        }

        [HttpGet("mostViews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ArticlesDto>>> GetTheMostViewdArticles([FromQuery] int count)
        {
            var articles = await _articleService.GetTheMostViewdArticles(count);
            if (articles == null) return NotFound();
            return Ok(articles);
        }
    }
}
