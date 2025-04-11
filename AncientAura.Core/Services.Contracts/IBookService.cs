using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Helper;
using AncientAura.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IBookService
    {
        public Task<PaginationResponse<BooksDto>> GetAllBooksAsync(SpecParameters parameters);
        public Task<BookDto> GetBookByIdAsync(int id);
        public Task<IEnumerable<ReviewsDto>> GetAllReviewsForBookById(int id);
        public Task<IEnumerable<BooksDto>> GetTheMostViewdBooks(int count);
    }
}
