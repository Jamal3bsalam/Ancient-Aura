using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Service.Services.ArticlesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AncientSitesController : ControllerBase
    {
        private readonly IAncientSitesService _ancientSitesService;

        public AncientSitesController(IAncientSitesService ancientSitesService)
        {
            _ancientSitesService = ancientSitesService;
        }

        [HttpGet("ancientSites")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AncientSitesDtos>>> GetAllAncientSites([FromQuery] SpecParameters parameters)
        {
            var ancientSites = await _ancientSitesService.GetAllAncientSitesAsync(parameters);
            return Ok(ancientSites);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AncientSiteDto>> GetAncientSiteById(int? id)
        {
            if (id == null) return BadRequest("invalid Operation");
            var ancientSite = await _ancientSitesService.GetAncientSiteByIdAsync(id.Value);
            if (ancientSite == null) return NotFound();
            return Ok(ancientSite);
        }

        [HttpGet("reviews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReviewsDto>>> GetAllReviewsByAncientSitesID([FromQuery] int id)
        {
            var ancientSite = await _ancientSitesService.GetAncientSiteByIdAsync(id);
            if (ancientSite == null) return NotFound("Invalid BookId");
            var reviews = await _ancientSitesService.GetAllReviewsForAncientSitesById(id);
            if (reviews == null) return BadRequest("The AncientSite Has No Reviews");
            return Ok(reviews);
        }

        [HttpGet("mostViews")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AncientSitesDtos>>> GetTheMostViewdAncientSites([FromQuery] int count)
        {
            var articles = await _ancientSitesService.GetTheMostViewdAncientSites(count);
            if (articles == null) return NotFound();
            return Ok(articles);
        }

        [HttpPost("recommended-sites")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRecommendedSites([FromBody]List<string> siteNames)
        {
            var recommendedSites = await _ancientSitesService.GetRecommendedSitesAsync(siteNames);
            return Ok(recommendedSites);
        }
    }
}
