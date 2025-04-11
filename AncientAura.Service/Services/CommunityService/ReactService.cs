using AncientAura.Core;
using AncientAura.Core.Dtos.ReactsDto;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts.ComunityContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.CommunityService
{
    public class ReactService : IReactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public ReactService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<ReactDto> AddReactAsync(CreateReactDto createReactDto,string userId)
        {
            if (createReactDto == null) return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;
            if (createReactDto.PostId is not null)
            {
                var post = await _unitOfWork.Repository<Post, int>().GetByIdAsync(createReactDto.PostId.Value);

                var react = new React()
                {
                    Type = createReactDto.ReactType,
                    UserId = userId,
                    PostId = createReactDto.PostId
                };
                await _unitOfWork.Repository<React, int>().AddAsync(react);
                await _unitOfWork.CompleteAsync();

                var reactDto = new ReactDto()
                {
                    ReactionType = react.Type.Value.ToString(),
                    UserName = user.FullName,
                    PostId = react.PostId
                
                };
                return reactDto;
            }
            else
            {
                var react = new React()
                {
                    Type = createReactDto.ReactType,
                    UserId = userId,
                    CommentId = createReactDto.CommentId
                };
                await _unitOfWork.Repository<React, int>().AddAsync(react);
                await _unitOfWork.CompleteAsync();

                var reactDto = new ReactDto()
                {
                    ReactionType = react.Type.Value.ToString(),
                    UserName = user.FullName,
                    CommentId = react.CommentId

                };
                return reactDto;
            }  
        }
        public async Task<bool> RemoveReactAsync(int reactId)
        {
            var react = await _unitOfWork.Repository<React,int>().GetByIdAsync(reactId);
            if (react == null) return false;

            _unitOfWork.Repository<React,int>().Delete(react);
            var result = await _unitOfWork.CompleteAsync();

            if(result == 0) return false;
            return true;
        }

        public async Task<IEnumerable<ReactDto>> GetReactsByCommentIdAsync(int commentId)
        {
            var comment = await _unitOfWork.Repository<Comment,int>().GetByIdAsync(commentId);
            if (comment == null) return null;
            var reacts = await _unitOfWork.Repository<React, int>().GetAllByIdAsync(commentId);
            var reactsDto = reacts.Select(r => new ReactDto
            {
                ReactionType = r.Type.ToString(), 
                UserName = r.User.FullName, 
                PostId = r.PostId,
                CommentId = r.CommentId,
            }).ToList();
            return reactsDto;
        }

        public async Task<IEnumerable<ReactDto>> GetReactsByPostIdAsync(int postId)
        {
            var post = await _unitOfWork.Repository<Post, int>().GetByIdAsync(postId);
            if (post == null) return null;
            var reacts = await _unitOfWork.Repository<React, int>().GetAllByIdAsync(postId);
            var reactsDto = reacts.Select(r => new ReactDto
            {
                ReactionType = r.Type.ToString(),
                UserName = r.User.FullName,
                PostId = r.PostId,
                CommentId = r.CommentId,
            }).ToList();
            return reactsDto;
        }

        
    }
}
