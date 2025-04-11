using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Dtos.VideosDto;
using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IVideosService
    {
        Task<IEnumerable<VideosDto>> GetAllVideosAsync();
        Task<VideosDto> GetVideoByIdAsync(int videoId);
    }
}
