using AncientAura.Core;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;

        public ReviewService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContext,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<int> AddReviewAsync(AddReviewDto addReviewDto)
        {
            var userId = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if(addReviewDto.BooksId == 0) { addReviewDto.BooksId = null; }
            if(addReviewDto.DocumentriesId == 0) { addReviewDto.DocumentriesId = null; }
            if(addReviewDto.ArticlesId == 0) { addReviewDto.ArticlesId = null; }
            if(addReviewDto.AncientSitesId == 0) { addReviewDto.AncientSitesId = null; }


            var review = new Reviews()
            {
                Rating = addReviewDto.Rating,
                Comment = addReviewDto.Comment,
                AppUserId = userId,
                DocumentriesId = addReviewDto.DocumentriesId,
                BooksId = addReviewDto.BooksId,
                ArticlesId = addReviewDto.ArticlesId,
                AncientSitesId = addReviewDto.AncientSitesId,
                UserName = user.UserName
            };
             await _unitOfWork.Repository<Reviews,int>().AddAsync(review);
            var result = await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<string> UpdatReviewAsync(int ReviewId, UpdateReviewDto updateReviewDto, string UserId)
        {
            var review = await _unitOfWork.Repository<Reviews,int>().GetByIdAsync(ReviewId);
            if (review == null) { return "Review Not found"; }

            if (review.AppUserId != UserId) { return "You can only update your own reviews."; }

            review.Comment = updateReviewDto.Comment;
            review.Rating = updateReviewDto.Rating;
            review.CreatedAt = DateTime.UtcNow;

             _unitOfWork.Repository<Reviews,int>().Update(review);
            var result = await _unitOfWork.CompleteAsync();
            if (result == 0) { return "Invalid Operation";}
            return "Review Updated Successfully";


        }

        public async Task<string> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await _unitOfWork.Repository<Reviews, int>().GetByIdAsync(reviewId);
            if (review == null) { return "Review Not found"; }

            if (review.AppUserId != userId) { return "You can only Delete your own reviews."; }
             _unitOfWork.Repository<Reviews,int>().Delete(review);
            var result = await _unitOfWork.CompleteAsync();
            if (result == 0) { return "Invalid Operation"; }
            return "Review Deleted Successfully";
            
        }
    }
}
