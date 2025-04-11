using AncientAura.Core;
using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.DocumetriesDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Helper;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Core.Specification.CountSpecificaiton;
using AncientAura.Core.Specification.DocumentriesSpecification;
using AncientAura.Core.Specification.ReviewSpecification;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.DocumetriesService
{
    public class DocumentrisService : IDocumentriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DocumentrisService(IUnitOfWork unitOfWork, IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<PaginationResponse<DocumentriesDto>> GetAllDocumetriesAsync(SpecParameters parameters)
        {
            var spec = new DocumentrySpecification(parameters);
            var documentries = await _unitOfWork.Repository<Documentries, int>().GetAllWithSpecAsync(spec);
            var documentriesDtos = _mapper.Map<IEnumerable<DocumentriesDto>>(documentries);

            var documentrySpec = new DocumntriesCountSpecification(parameters);
            var count = await _unitOfWork.Repository<Documentries,int>().GetCountAsync(documentrySpec);

            return new PaginationResponse<DocumentriesDto>(parameters.pageSize,parameters.pageIndex,count, documentriesDtos);
        }


        public async Task<DocumentryDto> GetDocumentryByIdAsync(int id)
        {
            var spec = new DocumentrySpecification(id);
            var documentry = await _unitOfWork.Repository<Documentries, int>().GetWithSpecAsync(spec);
            // var documentryDto = _mapper.Map<DocumentryDto>(documentry);
            if (documentry.ViewCount == null) documentry.ViewCount = 0;
            documentry.ViewCount++;
            _unitOfWork.Repository<Documentries,int>().Update(documentry);
            await _unitOfWork.CompleteAsync();
            var documentryDto = new DocumentryDto()
            {
                Id = documentry.Id,
                Name = documentry.Name,
                ContentCreator = documentry.ContentCreator,
                DocPictureUrl = $"{_configuration["BASEURL"]}" + documentry.DocPictureUrl,
                DocumentryUrl = documentry.DocumentryUrl,
                Description = documentry.Discription,
                ViewCount = documentry.ViewCount,
                Reviews = documentry.Reviews.Select(R => new ReviewsDto()
                {
                    Id = R.Id,
                    Rating = R.Rating,
                    Comment = R.Comment,
                    CreatedAt = R.CreatedAt,
                    UserName = R.UserName
                }).ToList()

            };
            return documentryDto;
        }

        public async Task<IEnumerable<ReviewsDto>> GetAllReviewsForDocumentryById(int id)
        {
            var spec = new ReviewSpecificationForDocumentry(id);
            var reviews = await _unitOfWork.Repository<Reviews, int>().GetAllWithSpecAsync((spec));
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

        public async Task<IEnumerable<DocumentriesDto>> GetTheMostViewdDocumentries(int count)
        {
            var documentries = await _unitOfWork.Repository<Documentries, int>().GetTheMostViewAsync(count);
            if (documentries == null) return null;
            var documentriesDto = _mapper.Map<IEnumerable<DocumentriesDto>>(documentries);
            return documentriesDto;
        }
    }
}
