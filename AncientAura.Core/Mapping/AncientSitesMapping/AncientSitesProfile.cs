using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Mapping.PictureUrlResolver;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.AncientSitesMapping
{
    public class AncientSitesProfile:Profile
    {
        public AncientSitesProfile()
        {
            CreateMap<AncientSites,AncientSitesDtos>().ForMember(A => A.PictureUrl, option => option.MapFrom<BaseUrlResolver<AncientSites>>());
            CreateMap<AncientSites,AncientSiteDto>()
                    .ForMember(A => A.PictureUrl, option => option.MapFrom<BaseUrlResolver<AncientSites>>());
            
        }
    }
}
