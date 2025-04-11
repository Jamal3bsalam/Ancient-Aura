using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts.ComunityContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Community - |Posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<AppUser> _userManager;

        public PostsController(IPostService postService, UserManager<AppUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpPost("createPost")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(PostsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PostsDto>> CreatePost(CreatePostDto createPostDto)
        {
            if (createPostDto == null) return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var postDto = await _postService.CreatePostAsync(createPostDto, user);
            return Ok(postDto);

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<PostsDto>>> GetAllPosts() 
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null) return BadRequest();
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostById(int postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null) return BadRequest();
            return Ok(post);
        }


        [HttpDelete("deletePost")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PostsDto>> DeletePost(int postId)
        {
            if (postId == null) return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var result = await _postService.DeletePostAsync(postId,userId);
            if(result == false) return BadRequest();
            return Ok(new { message = "Post Deleted successfully!" });

        }

        [HttpPut("updatePost")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PostsDto>> UpdatePost(UpdatePostDto updatePostDto)
        {
            if (updatePostDto == null) return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var postDto = await _postService.UpdatePostAsync(updatePostDto, userId);
            return Ok(postDto);

        }

        [HttpGet("userPosts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(PostsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostsDto>>> GetAllUserPosts()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var post = await _postService.GetPostsByUserAsync(userId);
            if (post == null) return BadRequest();
            return Ok(post);
        }

        [HttpPost("sharePosts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PostsDto>> SharePosts(int postId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var sharePost = await _postService.SharePostsAsync(postId,userId);
            if (sharePost == null) return BadRequest();
            return Ok(sharePost);
        }
    }
}
