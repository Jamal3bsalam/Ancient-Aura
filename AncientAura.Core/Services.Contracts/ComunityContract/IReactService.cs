using AncientAura.Core.Dtos.ReactsDto;
using AncientAura.Core.Entities.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts.ComunityContract
{
    public interface IReactService
    {
        Task<ReactDto> AddReactAsync(CreateReactDto createReactDto,string userId);
        Task<bool> RemoveReactAsync(int reactId);
        Task<IEnumerable<ReactDto>> GetReactsByPostIdAsync(int postId);
        Task<IEnumerable<ReactDto>> GetReactsByCommentIdAsync(int commentId);
    }
}
