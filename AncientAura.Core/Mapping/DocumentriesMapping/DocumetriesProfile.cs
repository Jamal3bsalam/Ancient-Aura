using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Dtos.DocumetriesDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Mapping.PictureUrlResolver;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.DocumentriesMapping
{
    public class DocumetriesProfile:Profile
    {
        public DocumetriesProfile()
        {
            CreateMap<Documentries, DocumentriesDto>()
                  .ForMember(D => D.DocPictureUrl, option => option.MapFrom<BaseUrlResolver<Documentries>>());
            CreateMap<Documentries, DocumentryDto>()
                  .ForMember(d => d.Description, option => option.MapFrom(s => s.Discription))
                  .ForMember(D => D.DocPictureUrl, option => option.MapFrom<BaseUrlResolver<Documentries>>());
            
        }
    }
}
