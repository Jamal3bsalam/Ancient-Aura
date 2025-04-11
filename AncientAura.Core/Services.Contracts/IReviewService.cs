using AncientAura.Core.Dtos.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IReviewService
    {
        public Task<int> AddReviewAsync(AddReviewDto addReviewDto);
        public Task<string> UpdatReviewAsync(int ReviewId, UpdateReviewDto updateReviewDto , string UserId);
        Task<string> DeleteReviewAsync(int reviewId, string userId);
    }
}
