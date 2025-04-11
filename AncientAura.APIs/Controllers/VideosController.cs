using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Dtos.VideosDto;
using AncientAura.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<VideosDto>>> GetAllVideos()
        {
            var videos = await _videosService.GetAllVideosAsync();
            if (videos == null) return BadRequest();
            return Ok(videos);
        }

        [HttpGet("{videoId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<VideosDto>>> GetVideoById(int videoId)
        {
            var video = await _videosService.GetVideoByIdAsync(videoId);
            if (video == null) return BadRequest();
            return Ok(video);
        }
    }
}
