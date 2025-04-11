using AncientAura.Core.Dtos.ArticlesDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Mapping.PictureUrlResolver;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.ArticlesMapping
{
    public class ArticlesProfile:Profile
    {
        public ArticlesProfile()
        {
            CreateMap<Articles,ArticlesDto>()
                  .ForMember(A => A.ArticlesPicUrl, option => option.MapFrom<BaseUrlResolver<Articles>>());
            CreateMap<Articles,ArticleDto>()
                  .ForMember(d => d.Description, option => option.MapFrom(s => s.Discription))
                  .ForMember(A => A.ArticlesPicUrl, option => option.MapFrom<BaseUrlResolver<Articles>>());

        }
    }
}
