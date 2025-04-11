using AncientAura.Core.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.PictureUrlResolver
{
    public class BaseUrlResolver<TSource> : IValueResolver<TSource, object, string> where TSource : BaseEntitiy<int>
    {
        private readonly IConfiguration _configuration;

        public BaseUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(TSource source, object destination, string destMember, ResolutionContext context)
        {
            if(typeof(TSource) == typeof(Books))
            {
                var book = source as Books;
                if (!string.IsNullOrEmpty(book.BookPictureUrl)) { return $"{_configuration["BASEURL"]}{book.BookPictureUrl}";}
            }
            if (typeof(TSource) == typeof(Articles))
            {
                var article = source as Articles;
                if (!string.IsNullOrEmpty(article.ArticlesPicUrl)) { return $"{_configuration["BASEURL"]}{article.ArticlesPicUrl}";}
            }
            if (typeof(TSource) == typeof(Documentries))
            {
                var documentries = source as Documentries;  
                if (!string.IsNullOrEmpty(documentries.DocPictureUrl)) { return $"{_configuration["BASEURL"]}{documentries.DocPictureUrl}";}
            }
            if (typeof(TSource) == typeof(AncientSites))
            {
                var ancientSites = source as AncientSites;
                if (!string.IsNullOrEmpty(ancientSites.PictureUrl)) { return $"{_configuration["BASEURL"]}{ancientSites.PictureUrl}"; }
            }
            return string.Empty;
        }
    }
}
