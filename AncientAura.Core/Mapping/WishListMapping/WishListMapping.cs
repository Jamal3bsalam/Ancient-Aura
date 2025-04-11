using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Dtos.WishlistDto;
using AncientAura.Core.Entities.WishLists;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Mapping.WishListMapping
{
    public class WishListMapping:Profile
    {

        public WishListMapping() 
        {
            CreateMap<WishList, WishlistDto>();
            //.AfterMap((src, dest) =>
            //{
            //    dest.AncientSites = src.Items.Select(i => new AncientSitesDtos
            //    {
            //        Id = i.AncientSites?.Id,
            //        Name = i.AncientSites?.Name,
            //        PictureUrl = _configuration["BASEURL"] +i.AncientSites?.PictureUrl
            //    }).ToList();
            //});
           
        }
    }
}
