using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Dtos.DocumetriesDto;
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
    public interface IDocumentriesService
    {
        public Task<PaginationResponse<DocumentriesDto>> GetAllDocumetriesAsync(SpecParameters parameters);
        public Task<DocumentryDto> GetDocumentryByIdAsync(int id);
        public Task<IEnumerable<ReviewsDto>> GetAllReviewsForDocumentryById(int id);
        public Task<IEnumerable<DocumentriesDto>> GetTheMostViewdDocumentries(int count);


    }
}
