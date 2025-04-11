using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("AddReview")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddReview([FromBody]AddReviewDto dto)
        {
            if(dto.BooksId == 0 && dto.ArticlesId == 0 && dto.DocumentriesId == 0 && dto.AncientSitesId == 0)
            {
                return BadRequest(new { error = "You must specify a BookId, ArticleId, or DocumentaryId." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) { return Unauthorized(new { error = "User is not authenticated." }); }

            var result = await _reviewService.AddReviewAsync(dto);
            if(result == 0) BadRequest(new { error = "Invalid Operation" });
            return Ok(new { message = "Review added successfully!" });

        }

        [HttpPut("UpdateReview{reviewId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateReview(int reviewId,[FromBody] UpdateReviewDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            if (userId == null) { return Unauthorized(new { error = "User is not authenticated." }); }

           var result =  await _reviewService.UpdatReviewAsync(reviewId, dto,userId);
            if (result == null) return BadRequest("Failed to update review.");

            return Ok(new { message = "Review Updated Successfully" });

        }

        [HttpDelete("DeleteReview{reviewId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            if (userId == null) { return Unauthorized(new { error = "User is not authenticated." }); }

            var result = await _reviewService.DeleteReviewAsync(reviewId, userId);
            if (result == null) return BadRequest("Failed to Delete review.");

            return Ok(new { message = "Review Deleted Successfully" });

        }
    }
}
