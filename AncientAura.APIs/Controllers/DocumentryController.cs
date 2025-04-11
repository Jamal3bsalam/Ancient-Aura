using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.DocumetriesDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Service.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Library - Documentries")]
    public class DocumentryController : ControllerBase
    {
        private readonly IDocumentriesService _documentriesService;

        public DocumentryController(IDocumentriesService documentriesService)
        {
            _documentriesService = documentriesService;
        }
        [HttpGet("documentries")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<DocumentriesDto>>> GetAllDocumentries([FromQuery] SpecParameters parameters)
        {
            var documentries = await _documentriesService.GetAllDocumetriesAsync(parameters);
            return Ok(documentries);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<DocumentryDto>> GetDocumentryById(int? id)
        {
            if (id == null) return BadRequest("invalid Operation");
            var documentry = await _documentriesService.GetDocumentryByIdAsync(id.Value);
            if (documentry == null) return NotFound();
            return Ok(documentry);
        }

        [HttpGet("reviews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReviewsDto>>> GetAllReviewsByDocumentryID([FromQuery] int id)
        {
            var documentry = await _documentriesService.GetDocumentryByIdAsync(id);
            if (documentry == null) return NotFound("Invalid BookId");
            var reviews = await _documentriesService.GetAllReviewsForDocumentryById(id);
            if (reviews == null) return BadRequest("The Doumentry Has No Reviews");
            return Ok(reviews);
        }

        [HttpGet("mostViews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<DocumentriesDto>>> GetTheMostViewsBooks([FromQuery] int count)
        {
            var documentries = await _documentriesService.GetTheMostViewdDocumentries(count);
            if (documentries == null) return NotFound();
            return Ok(documentries);
        }
    }
}
