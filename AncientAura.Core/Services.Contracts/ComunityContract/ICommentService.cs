using AncientAura.Core.Dtos.CommentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts.ComunityContract
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CreateCommentDto createCommentDto, string userId);
        Task<bool> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<bool> DeleteCommentAsync(int commentId, string userId);
        Task<IEnumerable<CommentsDto>> GetAllCommentsByPostIdAsync(int postId,string userId);
        Task<IEnumerable<RepliesDto>> GetRepliesByCommentIdAsync(int commentId);
    }
}
