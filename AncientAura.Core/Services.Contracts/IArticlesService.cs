using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Helper;
using AncientAura.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IArticlesService
    {
        public Task<PaginationResponse<ArticlesDto>> GetAllArticlesAsync(SpecParameters parameters);
        public Task<ArticleDto> GetArticleByIdAsync(int id);
        public Task<IEnumerable<ReviewsDto>> GetAllReviewsForArticleById(int id);
        public Task<IEnumerable<ArticlesDto>> GetTheMostViewdArticles(int count);


    }
}
