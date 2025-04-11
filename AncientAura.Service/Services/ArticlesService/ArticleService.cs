using AncientAura.Core;
using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Helper;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Core.Specification.ArticlesSpecification;
using AncientAura.Core.Specification.CountSpecificaiton;
using AncientAura.Core.Specification.ReviewSpecification;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.ArticlesService
{
    public class ArticleService:IArticlesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ArticleService(IUnitOfWork unitOfWork , IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PaginationResponse<ArticlesDto>> GetAllArticlesAsync(SpecParameters parameters)
        {
            var spec = new ArticleSpecification(parameters);
            var articles = await _unitOfWork.Repository<Articles, int>().GetAllWithSpecAsync(spec);
            var articlesDtos = _mapper.Map<IEnumerable<ArticlesDto>>(articles);

            var countSpec = new ArticleCountSpecification(parameters);
            var count = await _unitOfWork.Repository<Articles,int>().GetCountAsync(countSpec);

            return new PaginationResponse<ArticlesDto>(parameters.pageSize,parameters.pageIndex,count,articlesDtos);

        }

       

        public async Task<ArticleDto> GetArticleByIdAsync(int id)
        {
            var spec = new ArticleSpecification(id);
            var articles = await _unitOfWork.Repository<Articles, int>().GetWithSpecAsync(spec);
            if (articles == null) return null;

            if (articles.ViewCount == null) articles.ViewCount = 0;
            articles.ViewCount++;

            _unitOfWork.Repository<Articles, int>().Update(articles);
            await _unitOfWork.CompleteAsync();
            var atrticlesDtos = new ArticleDto()
            {
                Id = articles.Id,
                Name = articles.Name,
                Publisher = articles.Publisher,
                ArticlesPicUrl = $"{_configuration["BASEURL"]}"+articles.ArticlesPicUrl,
                Contenet = articles.Contenet,
                Description = articles.Discription,
                ViewCount = articles.ViewCount,
                Reviews = articles.Reviews.Select(R => new ReviewsDto()
                {
                    Id = R.Id,
                    Rating = R.Rating,
                    Comment = R.Comment,
                    CreatedAt = R.CreatedAt,
                    UserName = R.UserName
                }).ToList()
            };
           
            return atrticlesDtos;
        }

        public async Task<IEnumerable<ReviewsDto>> GetAllReviewsForArticleById(int id)
        {
            var spec = new ReviewSpecificationForArticle(id);
            var reviews = await _unitOfWork.Repository<Reviews, int>().GetAllWithSpecAsync(spec);
            if (reviews == null) return null;
            var reviewDto = reviews.Select(R => new ReviewsDto()
            {
                Id = R.Id,
                UserName = R.UserName,
                Rating = R.Rating,
                Comment = R.Comment,
                CreatedAt = R.CreatedAt,
            }).ToList();
            return reviewDto;
        }

        public async Task<IEnumerable<ArticlesDto>> GetTheMostViewdArticles(int count)
        {
            var articles = await _unitOfWork.Repository<Articles, int>().GetTheMostViewAsync(count);
            if (articles == null) return null;
            var articlesDto = _mapper.Map<IEnumerable<ArticlesDto>>(articles);
            return articlesDto ;
        }
    }
}
