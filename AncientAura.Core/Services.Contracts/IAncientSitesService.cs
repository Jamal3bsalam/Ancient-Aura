using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Dtos.ArticlesDto;
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
    public interface IAncientSitesService
    {
        public Task<PaginationResponse<AncientSitesDtos>> GetAllAncientSitesAsync(SpecParameters parameters);
        public Task<AncientSiteDto> GetAncientSiteByIdAsync(int id);
        public Task<IEnumerable<ReviewsDto>> GetAllReviewsForAncientSitesById(int id);
        public Task<IEnumerable<AncientSitesDtos>> GetTheMostViewdAncientSites(int count);
        Task<List<AncientSitesDtos>> GetRecommendedSitesAsync(List<string> siteNames);
    }
}
