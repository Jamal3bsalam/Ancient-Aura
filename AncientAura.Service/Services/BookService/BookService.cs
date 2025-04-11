using AncientAura.Core;
using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Helper;
using AncientAura.Core.Services.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Core.Specification.BooksSpecification;
using AncientAura.Core.Specification.CountSpecificaiton;
using AncientAura.Core.Specification.ReviewSpecification;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace AncientAura.Service.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public BookService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<PaginationResponse<BooksDto>>GetAllBooksAsync(SpecParameters parameters)
        {
            var spec = new BookSpecification(parameters);
            var books = await _unitOfWork.Repository<Books,int>().GetAllWithSpecAsync(spec);
            var booksDto = _mapper.Map<IEnumerable<BooksDto>>(books);
            
            var countSpec = new BookCountSpecification(parameters);
            var count = await _unitOfWork.Repository<Books,int>().GetCountAsync(countSpec);
            return new PaginationResponse<BooksDto>(parameters.pageSize,parameters.pageIndex,count,booksDto);
            
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var spec = new BookSpecification(id);
            var book = await _unitOfWork.Repository<Books,int>().GetWithSpecAsync(spec);

            if (book == null) return null;
            if(book.ViewCount == null) book.ViewCount = 0;
            book.ViewCount++;
            _unitOfWork.Repository<Books, int>().Update(book);
            await _unitOfWork.CompleteAsync();
            //var bookDto = _mapper.Map<BookDto>(book);
            var bookDto = new BookDto()
            {
                BookId = book.Id,
                Name = book.Name,
                Author = book.Author,
                BookPictureUrl = $"{_configuration["BASEURL"]}" + book.BookPictureUrl,
                BookUrl = book.BookURL,
                Description = book.Discription,
                ViewsCount = book.ViewCount,
                Reviews = book.Reviews.Select(R => new ReviewsDto
                {
                    Id = R.Id,
                    Rating = R.Rating,
                    Comment = R.Comment,
                    CreatedAt = R.CreatedAt,
                    UserName = R.UserName
                }).ToList()
            };
            return bookDto;
        }

        public async Task<IEnumerable<ReviewsDto>> GetAllReviewsForBookById(int id)
        {
           var spec = new ReviewSpecification(id);
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

        public async Task<IEnumerable<BooksDto>> GetTheMostViewdBooks(int count)
        {
            var books = await _unitOfWork.Repository<Books,int>().GetTheMostViewAsync(count);
            if (books == null) return null;
            var booksDto = _mapper.Map<IEnumerable<BooksDto>>(books);
            return booksDto;
        }
    }
}
