using AncientAura.Core;
using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Dtos.VideosDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.VideosService
{
    public class VideoService : IVideosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public VideoService(IUnitOfWork unitOfWork , IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<IEnumerable<VideosDto>> GetAllVideosAsync()
        {
            var videos = await _unitOfWork.Repository<Videos,int>().GetAllAsync();
            if (videos == null) return null;
            var videoDto = videos.Select( V => new VideosDto()
            {
                Id = V.Id,
                Name = V.Name,
                VideoUrl = _configuration["BASEURL"] + V.VideoUrl
            }).ToList();
            return videoDto;
        }

        public async Task<VideosDto> GetVideoByIdAsync(int videoId)
        {
            var videos = await _unitOfWork.Repository<Videos, int>().GetByIdAsync(videoId);
            if (videos == null) return null;
            var videoDto = new VideosDto()
            {
                Id = videos.Id,
                Name = videos.Name,
                VideoUrl = _configuration["BASEURL"] + videos.VideoUrl
            };
            return videoDto;
        }
    }
}
