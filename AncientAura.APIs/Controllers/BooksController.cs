using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Core.Specification.ReviewSpecification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Library - Books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("books")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<BooksDto>>> GetAllBooks([FromQuery]SpecParameters parameters)
        {
            var books = await _bookService.GetAllBooksAsync(parameters);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<BookDto>> GetBookById(int? id)
        {
            if (id == null) return BadRequest("invalid Operation");
            var book = await _bookService.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpGet("reviews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReviewsDto>>> GetAllReviewsByBookID([FromQuery] int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound("Invalid BookId");
            var reviews = await _bookService.GetAllReviewsForBookById(id);
            if (reviews == null) return BadRequest("The Book Has No Reviews");
            return Ok(reviews);
        }

        [HttpGet("mostViews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<BooksDto>>> GetTheMostViewsBooks([FromQuery] int count)
        {
            var books = await _bookService.GetTheMostViewdBooks(count);
            if (books == null) return NotFound();   
            return Ok(books);
        }
    }
}
