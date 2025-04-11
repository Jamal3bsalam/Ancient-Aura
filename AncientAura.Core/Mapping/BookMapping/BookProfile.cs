using AncientAura.Core.Dtos.BookDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Mapping.PictureUrlResolver;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.BookMapping
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<Books, BooksDto>()
                .ForMember(B => B.BookPictureUrl, option => option.MapFrom<BaseUrlResolver<Books>>());
            CreateMap<Books, BookDto>()
                .ForMember(d => d.Description, option => option.MapFrom(s => s.Discription))
                .ForMember(B => B.BookPictureUrl, option => option.MapFrom<BaseUrlResolver<Books>>());

        }
    }
}
