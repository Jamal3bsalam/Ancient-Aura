using AncientAura.Core;
using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Helper;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Core.Specification.AncientSitesSpecification;
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

namespace AncientAura.Service.Services.AncientSitesService
{
    public class AncientSitesService : IAncientSitesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AncientSitesService(IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<PaginationResponse<AncientSitesDtos>> GetAllAncientSitesAsync(SpecParameters parameters)
        {
            var spec = new AncientSitesSpecification(parameters);
            var ancientSites = await _unitOfWork.Repository<AncientSites, int>().GetAllWithSpecAsync(spec);
            var ancientSitesDtos = _mapper.Map<IEnumerable<AncientSitesDtos>>(ancientSites);

            var countSpec = new AncientSiteCountSpecification(parameters);
            var count = await _unitOfWork.Repository<AncientSites, int>().GetCountAsync(countSpec);

            return new PaginationResponse<AncientSitesDtos>(parameters.pageSize, parameters.pageIndex, count, ancientSitesDtos);
        }


        public async Task<AncientSiteDto> GetAncientSiteByIdAsync(int id)
        {
            var spec = new AncientSitesSpecification(id);
            var ancientSites = await _unitOfWork.Repository<AncientSites, int>().GetWithSpecAsync(spec);
            if (ancientSites == null) return null;

            if (ancientSites.ViewCount == null) ancientSites.ViewCount = 0;
            ancientSites.ViewCount++;

            _unitOfWork.Repository<AncientSites, int>().Update(ancientSites);
            //await _unitOfWork.CompleteAsync();
            var ancientSiteDto = new AncientSiteDto()
            {
                Id = ancientSites.Id,
                Name = ancientSites.Name,
                Description = ancientSites.Description,
                PictureUrl = _configuration["BASEURL"] + ancientSites.PictureUrl,
                VoiceUrl = _configuration["BASEURL"] + ancientSites.VoiceUrl,
                Adddress = ancientSites.Adddress,
                OpeningTime = ancientSites.OpeningTime,
                ClosedTime = ancientSites.ClosedTime,
                ImageURLs = ancientSites.ImageURLs.Where(I => I.AncientSitesId == id).Select(I => new ImagesDto() { AncientSiteId = I.AncientSitesId , ImageUrl = _configuration["BASEURL"] +I.ImageUrl , Id = I.Id}).ToList(),
                Reviews = ancientSites.Reviews.Select(R => new ReviewsDto()
                {
                    Id = R.Id,
                    Rating = R.Rating,
                    Comment = R.Comment,
                    CreatedAt = R.CreatedAt,
                    UserName = R.UserName
                }).ToList()
                
            };
            await _unitOfWork.CompleteAsync();
            return ancientSiteDto;
        }

        public async Task<IEnumerable<ReviewsDto>> GetAllReviewsForAncientSitesById(int id)
        {
            var spec = new ReviewSpecificationForAncientSites(id);
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

        public async Task<IEnumerable<AncientSitesDtos>> GetTheMostViewdAncientSites(int count)
        {
            var ancientSites = await _unitOfWork.Repository<AncientSites, int>().GetTheMostViewAsync(count);
            if (ancientSites == null) return null;
            var ancientSitesDtos = _mapper.Map<IEnumerable<AncientSitesDtos>>(ancientSites);
            return ancientSitesDtos;
        }

        public async Task<List<AncientSitesDtos>> GetRecommendedSitesAsync(List<string> siteNames)
        {
            var ancientSites = await _unitOfWork.Repository<AncientSites,int>().GetRecommendedAsync(siteNames);
            if (ancientSites == null) return null;
            var recommendedSites = _mapper.Map<List<AncientSitesDtos>>(ancientSites);
            return recommendedSites;
        }
    }
}
