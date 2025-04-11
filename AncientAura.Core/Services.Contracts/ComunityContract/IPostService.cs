using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts.ComunityContract
{
    public interface IPostService
    {
        Task<PostsDto> CreatePostAsync(CreatePostDto postDto ,AppUser user);
        Task<PostDto> UpdatePostAsync(UpdatePostDto postDto, string userId);
        Task<bool> DeletePostAsync(int postId, string userId);
        Task<IEnumerable<PostsDto>> GetAllPostsAsync();
        Task<PostDto> GetPostByIdAsync(int postId);
        Task<IEnumerable<PostsDto>> GetPostsByUserAsync(string userId);
        Task<PostsDto> SharePostsAsync(int postId , string userId);
    }
}
