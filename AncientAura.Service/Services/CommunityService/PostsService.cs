using AncientAura.Core;
using AncientAura.Core.Dtos.CommentDto;
using AncientAura.Core.Dtos.PostDtos;
using AncientAura.Core.Dtos.ProfileDto;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts.ComunityContract;
using AncientAura.Repository;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AncientAura.Service.Services.CommunityService
{
    public class PostsService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AncientAuraDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public PostsService(IUnitOfWork unitOfWork,AncientAuraDbContext context,IConfiguration configuration ,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<PostsDto> CreatePostAsync(CreatePostDto createpostDto, AppUser user)
        {
            if(createpostDto.File is null)
            {
               createpostDto.File = null;
            }

            var files = createpostDto.File;
            List<string> filesName = Upload(files);


            var posts = new Post()
            {
                Content = createpostDto.Content,
                UserId = user.Id
            };
            await _unitOfWork.Repository<Post, int>().AddAsync(posts);
            await _unitOfWork.CompleteAsync();


            foreach (var filename in filesName)
            {
                var postImages = new PostImages()
                {
                    ImageUrl = filename,
                    PostId = posts.Id
                };
            await _unitOfWork.Repository<PostImages, int>().AddAsync(postImages);
            }
           
            await _unitOfWork.CompleteAsync();

            var postDto = new PostsDto()
            {
                Id = posts.Id,
                Content = posts.Content,
                AuthorName = user.FullName,
                CreatedAt = posts.CreatedAt,
                ReactCount = posts.Reacts.Count(),
                CommentCount = posts.Comments.Count(),
                Images = posts.Images.Where(I => I.PostId == posts.Id).Select(I => new PostImagesDto() { Id = I.Id,ImageUrl = _configuration["BASEURL"]+I.ImageUrl, PostId = I.PostId }).ToList()

            };
            return postDto;
        }

        public List<string> Upload(List<IFormFile> files)
        {
            // D:\AncientAura\AncientAura\AncientAura.APIs\wwwroot\Images\ProfileImage\
            // string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Images\\{profileImageDto.FolderName}");
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string folderPath = (CurrentDirectory + $"\\wwwroot\\Images\\Posts");
            List<string> fileNames = new List<string>(); 
            foreach(IFormFile file in files)
            {
                string fileName = file.FileName;
                string filePath = Path.Combine(folderPath, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileNames.Add($"\\Images\\Posts\\{fileName}");
            }
            return fileNames;

        }
        public void Delete(List<string> files)
        {
            List<string> fileNames = new List<string>();
            foreach (string file in files)
            {
            //D:\AncientAura\AncientAura\AncientAura.APIs\wwwroot\Images\Posts\Khufu.jpg
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string filePath = (CurrentDirectory + "\\wwwroot" +file);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }  
        }

        public async Task<bool> DeletePostAsync(int postId, string userId)
        {
            if (userId == null || postId == null) return false;
            var post = await _unitOfWork.Repository<Post,int>().GetByIdAsync(postId);
            if (post == null) return false;

            List<string> files = new List<string>();
            foreach (var url in post.Images)
            {
                files.Add(url.ImageUrl);
            }
            Delete(files);

           _context.Set<PostImages>().RemoveRange(post.Images);
           _unitOfWork.Repository<Post, int>().Delete(post);
           await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<PostsDto>> GetAllPostsAsync()
        {
            var posts = await _unitOfWork.Repository<Post, int>().GetAllAsync();
            if (posts == null) return null;
            var postDto = posts.Select(P => new PostsDto()
            {
                Id = P.Id,
                Content = P.Content,
                AuthorName = P.User.FullName,
                ReactCount = P.Reacts.Count(),
                CommentCount = P.Comments.Count(),
                Images = P.Images.Where(I => I.PostId == P.Id).Select(I => new PostImagesDto() { Id = I.Id, ImageUrl = _configuration["BASEURL"] + I.ImageUrl, PostId = I.PostId }).ToList(),

            });
            return postDto;
        }

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await _unitOfWork.Repository<Post,int>().GetByIdAsync(postId);
            if (post == null) return null;

            var postDto = new PostDto()
            {
                Id = post.Id,
                Content = post.Content,
                AuthorName = post.User.FullName,
                ReactCount = post.Reacts.Count(),
                CommentCount = post.Comments.Count(),
                Images = post.Images.Where(I => I.PostId == post.Id).Select(I => new PostImagesDto() { Id = I.Id, ImageUrl = _configuration["BASEURL"] + I.ImageUrl, PostId = I.PostId }).ToList(),
                Comments = post.Comments.Where(C => C.PostId == post.Id).Select(C => new CommentsDto() { Id = C.Id, Content = C.Content, CommentFile = new CommentImagesDto() { ImageUrl = _configuration["BASEURL"] + C.CommentImages?.ImageUrl } }).ToList()
            };

            return postDto;

        }

        public async Task<PostDto> UpdatePostAsync( UpdatePostDto postDto, string userId)
        {
            var post = await _unitOfWork.Repository<Post, int>().GetByIdAsync(postDto.postId.Value);
            if (post == null) return null;

            if (userId != post.UserId) return null;

            if (postDto.Image == null && postDto.Content == null) return null;
            
            if(postDto.Image == null)
            {
                post.Content = postDto.Content;
                await _unitOfWork.CompleteAsync();
            }
            else if(postDto.Content == null)
            {

                List<string> files = new List<string>();
                foreach (var url in post.Images)
                {
                    files.Add(url.ImageUrl);
                }
                Delete(files);

                _context.Set<PostImages>().RemoveRange(post.Images);
                await _unitOfWork.CompleteAsync();

                var images = postDto.Image;
                List<string> filesName = Upload(images);

                foreach (var filename in filesName)
                {
                    var postImages = new PostImages()
                    {
                        ImageUrl = filename,
                        PostId = post.Id
                    };
                    await _unitOfWork.Repository<PostImages, int>().AddAsync(postImages);
                }

                await _unitOfWork.CompleteAsync();

               
            }
            else
            {
                post.Content = postDto.Content;
                await _unitOfWork.CompleteAsync();

                List<string> files = new List<string>();
                foreach (var url in post.Images)
                {
                    files.Add(url.ImageUrl);
                }
                Delete(files);

                _context.Set<PostImages>().RemoveRange(post.Images);
                await _unitOfWork.CompleteAsync();

                var images = postDto.Image;
                List<string> filesName = Upload(images);

                foreach (var filename in filesName)
                {
                    var postImages = new PostImages()
                    {
                        ImageUrl = filename,
                        PostId = post.Id
                    };
                    await _unitOfWork.Repository<PostImages, int>().AddAsync(postImages);
                }

                await _unitOfWork.CompleteAsync();
            }
            var postDtos = new PostDto()
            {
                Id = post.Id,
                Content = post.Content,
                AuthorName = post.User.FullName,
                CreatedAt = post.CreatedAt,
                ReactCount = post.Reacts.Count(),
                CommentCount = post.Comments.Count(),
                Images = post.Images.Where(I => I.PostId == post.Id).Select(I => new PostImagesDto() { Id = I.Id, ImageUrl = _configuration["BASEURL"] + I.ImageUrl, PostId = I.PostId }).ToList()

            };
            return postDtos;
        }

        public async Task<IEnumerable<PostsDto>> GetPostsByUserAsync(string userId)
        {
            var posts = await _unitOfWork.Repository<Post, int>().GetAllForSpecificAsync(userId);
            if (posts == null) return null;
            var postDto = posts.Select(P => new PostsDto()
            {
                Id = P.Id,
                Content = P.Content,
                AuthorName = P.User.FullName,
                CreatedAt = P.CreatedAt,
                ReactCount = P.Reacts.Count(),
                CommentCount = P.Comments.Count(),
                Images = P.Images.Where(I => I.PostId == P.Id).Select(I => new PostImagesDto() { Id = I.Id, ImageUrl = _configuration["BASEURL"] + I.ImageUrl, PostId = I.PostId }).ToList()

            });
            return postDto;
        }

        public async Task<PostsDto> SharePostsAsync(int postId , string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var originalPost = await _unitOfWork.Repository<Post,int>().GetByIdAsync(postId);
            if (originalPost == null) return null;

            var sharePost = new Post()
            {
                Content = originalPost.Content,
                UserId = userId,
                ShareCount = originalPost.ShareCount,
                CreatedAt = DateTime.UtcNow,
                Images = originalPost.Images.Select(img => new PostImages
                {
                    ImageUrl = img.ImageUrl,
                }).ToList()
            };

            if (originalPost.ShareCount == null) { originalPost.ShareCount = 0; }
            originalPost.ShareCount++;
            _unitOfWork.Repository<Post, int>().Update(originalPost);

            await _unitOfWork.Repository<Post, int>().AddAsync(sharePost);
            await _unitOfWork.CompleteAsync();

            var postDto = new PostsDto()
            {
                Id = sharePost.Id,
                Content = sharePost.Content,
                AuthorName = user.FullName,
                CreatedAt  = sharePost.CreatedAt,
                ReactCount = sharePost.Reacts.Count(),
                CommentCount = sharePost.Comments.Count(),
                ShareCount = originalPost.ShareCount,
                Images = sharePost.Images.Where(I => I.PostId == sharePost.Id).Select(I => new PostImagesDto() { Id = I.Id, ImageUrl = _configuration["BASEURL"] + I.ImageUrl, PostId = I.PostId }).ToList()

            };
            return postDto;

        }
    }
}
