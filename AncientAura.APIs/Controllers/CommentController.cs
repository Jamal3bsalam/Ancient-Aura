using AncientAura.Core.Dtos.CommentDto;
using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Services.Contracts.ComunityContract;
using AncientAura.Service.Services.CommunityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Community - Comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("addComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddComment(CreateCommentDto commentDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null) return BadRequest("Unable to add comment.");
            var comment = await _commentService.AddCommentAsync(commentDto, userId);
            return comment ? Ok("Comment Added successfully") : BadRequest("Unable to add comment.");
        }

        [HttpPut("updateComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null) return BadRequest("Unable to add comment.");
            var comment = await _commentService.UpdateCommentAsync(updateCommentDto);
            return comment ? Ok("Comment Updated successfully") : BadRequest("Unable to Update comment.");
        }

        [HttpDelete("deleteComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null) return BadRequest("Unable to add comment.");
            var comment = await _commentService.DeleteCommentAsync(commentId, userId);
            return comment ? Ok("Comment Deleted successfully") : BadRequest("Unable to Delete comment.");
        }

        [HttpGet("comments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<CommentsDto>>> GetAllComments(int postId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null) return BadRequest();

            var comments = await _commentService.GetAllCommentsByPostIdAsync(postId,userId);
            if (comments == null) return BadRequest();
            return Ok(comments);
        }

        [HttpGet("replies")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<RepliesDto>>> GetAllReplies(int commentId)
        {
            var replies = await _commentService.GetRepliesByCommentIdAsync(commentId);
            if (replies == null) return BadRequest();
            return Ok(replies);
        }
    }
}
