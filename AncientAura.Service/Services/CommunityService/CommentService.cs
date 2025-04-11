using AncientAura.Core;
using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Dtos.CommentDto;
using AncientAura.Core.Dtos.ProfileDto;
using AncientAura.Core.Dtos.ReactsDto;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts.ComunityContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.CommunityService
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public CommentService(IUnitOfWork unitOfWork , UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<bool> AddCommentAsync(CreateCommentDto createCommentDto , string userId)
        {
            if (createCommentDto == null) return false;

            var post = await _unitOfWork.Repository<Post, int>().GetByIdAsync(createCommentDto.PostId.Value);
            if (post == null) return false;

            if (createCommentDto.File == null)
            {
                var comment = new Comment()
                {
                    Content = createCommentDto.Content,
                    CreateAt = DateTime.UtcNow,
                    ParentCommentId = createCommentDto.ParentCommentId,
                    PostId = createCommentDto.PostId,
                    UserId = userId,

                };
                await _unitOfWork.Repository<Comment,int>().AddAsync(comment);
            }
            else if(createCommentDto.Content == null)
            {
                var comment = new Comment()
                {
                    CreateAt = DateTime.UtcNow,
                    ParentCommentId = createCommentDto.ParentCommentId,
                    PostId = createCommentDto.PostId,
                    UserId = userId,
                    CommentImages = new CommentImages()
                    {
                        ImageUrl = $"\\Images\\Comments\\{createCommentDto.File.FileName}",
                    }
                };
                await _unitOfWork.Repository<Comment, int>().AddAsync(comment);
            }
            else
            {
                var comment = new Comment()
                {
                    Content = createCommentDto.Content,
                    CreateAt = DateTime.UtcNow,
                    ParentCommentId = createCommentDto.ParentCommentId,
                    PostId = createCommentDto.PostId,
                    UserId = userId,
                    CommentImages = new CommentImages()
                    {
                        ImageUrl = $"\\Images\\Comments\\{createCommentDto.File.FileName}",
                    }

                };
                await _unitOfWork.Repository<Comment, int>().AddAsync(comment);
            }



            if (createCommentDto.File != null) Upload(createCommentDto.File);
           var result = await _unitOfWork.CompleteAsync();
            if(result == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteCommentAsync(int commentId, string userId)
        {
            var comment = await _unitOfWork.Repository<Comment,int>().GetByIdAsync(commentId);
            if (comment == null) return false;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && comment.UserId == userId)
            {
                if (comment.CommentImages?.ImageUrl is not null)
                {
                    Delete(comment.CommentImages.ImageUrl);
                    _unitOfWork.Repository<CommentImages, int>().Delete(comment.CommentImages);
                }

                 _unitOfWork.Repository<Comment, int>().Delete(comment);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CommentsDto>> GetAllCommentsByPostIdAsync(int postId,string userId)
        {
            var post = await _unitOfWork.Repository<Post, int>().GetByIdAsync(postId);
            if (post == null) return null;
            var comment = await _unitOfWork.Repository<Comment, int>().GetAllByIdAsync(postId);
            if (comment == null) return null;

            var user = await _userManager.FindByIdAsync(userId);

            var commentDto = comment.Select(C => new CommentsDto()
            {
                Id = C.Id,
                Content = C.Content,
                UserName = C.User.FullName,
                CommentFile = new CommentImagesDto() { ImageUrl = _configuration["BASEURL"] + C.CommentImages.ImageUrl},
                PostId = C.PostId.Value,
                ReactCount = C.Reacts?.Count(),
                RepliesCount = C.Replies?.Count(),
                
            }).ToList();
            return commentDto;
        }

        public async Task<bool> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var comment = await _unitOfWork.Repository<Comment, int>().GetByIdAsync(updateCommentDto.CommentId.Value);
            if (comment == null) return false;

            if (updateCommentDto.Content == null && updateCommentDto.File == null) return false;
            if (updateCommentDto.File == null)
            {
                comment.Content = updateCommentDto.Content;
                comment.CreateAt = DateTime.UtcNow;
               _unitOfWork.Repository<Comment,int>().Update(comment);
               var result = await _unitOfWork.CompleteAsync();
               if(result == 0) return false;
               return true;
            }
            else if(updateCommentDto.Content == null)
            {
                Delete(comment.CommentImages.ImageUrl);
                comment.CommentImages.ImageUrl = Upload(updateCommentDto.File);
                comment.CreateAt = DateTime.UtcNow;
                _unitOfWork.Repository<Comment, int>().Update(comment);
                var result = await _unitOfWork.CompleteAsync();
                if (result == 0) return false;
                return true;
            }
            else
            {
                comment.Content = updateCommentDto.Content;
                comment.CreateAt = DateTime.UtcNow;
                Delete(comment.CommentImages.ImageUrl);
                comment.CommentImages.ImageUrl = Upload(updateCommentDto.File);
                _unitOfWork.Repository<Comment, int>().Update(comment);
                var result = await _unitOfWork.CompleteAsync();
                if (result == 0) return false;
                return true;
            }
        }


        public string Upload(IFormFile file)
        {
            // D:\AncientAura\AncientAura\AncientAura.APIs\wwwroot\Images\ProfileImage\
            // string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Images\\{profileImageDto.FolderName}");
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string folderPath = (CurrentDirectory + $"\\wwwroot\\Images\\Comments");

            string fileName = file.FileName;

            string filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return $"\\Images\\Comments\\{fileName}";

        }

        public void Delete(string file)
        {
                //D:\AncientAura\AncientAura\AncientAura.APIs\wwwroot\Images\Posts\Khufu.jpg
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string filePath = (CurrentDirectory + "\\wwwroot" + file);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
        }

        public async Task<IEnumerable<RepliesDto>> GetRepliesByCommentIdAsync(int commentId)
        {
            var comment = await _unitOfWork.Repository<Comment, int>().GetByIdAsync(commentId);
            if (comment == null) return null;
            var replies = await _unitOfWork.Repository<Comment, int>().GetAllRepliesByCommentIdAsync(commentId);
            if (replies == null) return null;
            var repliesDto = replies.Select(C => new RepliesDto()
            {
                Id = C.Id,
                Content = C.Content,
                UserName = C.User.FullName,
                CommentFile = new CommentImagesDto() { ImageUrl = _configuration["BASEURL"] + C.CommentImages.ImageUrl },
                ParentCommentId = C.ParentCommentId.Value,
                ReactCount = C.Reacts?.Count(),
                RepliesCount = C.Replies?.Count(),

            }).ToList();
            return repliesDto;
        }
    }
}
