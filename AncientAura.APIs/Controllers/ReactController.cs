using AncientAura.Core.Dtos.ReactsDto;
using AncientAura.Core.Services.Contracts.ComunityContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Community - Reacts")]
    public class ReactController : ControllerBase
    {
        private readonly IReactService _reactService;

        public ReactController(IReactService reactService)
        {
            _reactService = reactService;
        }

        [HttpPost("AddReact")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ReactDto>> AddReact([FromForm]CreateReactDto reactDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var result = await _reactService.AddReactAsync(reactDto,userId);
            if (result == null) return BadRequest("Unable to add react.");
            return Ok(result);
        }

        [HttpDelete("RemoveReact/{reactId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveReact(int reactId)
        {
            var result = await _reactService.RemoveReactAsync(reactId);
            if (!result) return NotFound("React not found.");
            return Ok("React removed successfully.");
        }

        [HttpGet("postReacts/{postId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReactDto>>> GetReactsByPost(int postId)
        {
            var reacts = await _reactService.GetReactsByPostIdAsync(postId);
            if (reacts == null || !reacts.Any()) return NotFound("No reacts found for this post.");
            return Ok(reacts);
        }

        [HttpGet("commentReacts{commentId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<ReactDto>>> GetReactsByComment(int commentId)
        {
            var reacts = await _reactService.GetReactsByCommentIdAsync(commentId);
            if (reacts == null || !reacts.Any()) return NotFound("No reacts found for this comment.");
            return Ok(reacts);
        }
    }
}
