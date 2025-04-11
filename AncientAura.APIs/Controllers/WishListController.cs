using AncientAura.Core.Dtos.WishlistDto;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using AncientAura.Service.Services.WishListService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        private readonly UserManager<AppUser> _userManager;

        public WishListController(IWishListService wishListService,UserManager<AppUser> userManager)
        {
            _wishListService = wishListService;
            _userManager = userManager;
        }

        [HttpGet("wishList")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<List<WishlistDto>>> GetWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return NotFound();
            var wishlist = await _wishListService.GetWishlistAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost("add/{ancientSiteId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddToWishlist(int ancientSiteId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return NotFound();

            var result = await _wishListService.AddToWishlistAsync(userId, ancientSiteId);
            return result ? Ok("Added successfully") : BadRequest("Already in wishlist");
        }

        [HttpDelete("remove/{ancientSiteId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveFromWishlist(int ancientSiteId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return NotFound();
            var result = await _wishListService.RemoveFromWishlistAsync(userId, ancientSiteId);
            return result ? Ok("Removed successfully") : BadRequest("Item not found in wishlist");
        }

        [HttpDelete("clear")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ClearWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return NotFound();
            var result = await _wishListService.ClearWishlistAsync(userId);
            return result ? Ok("Wishlist cleared successfully") : BadRequest("Wishlist already empty");
        }
    }
}
