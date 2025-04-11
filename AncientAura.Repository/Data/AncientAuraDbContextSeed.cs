using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Identity;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data
{
    public class AncientAuraDbContextSeed
    {
        public async static Task SeedAppUser(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "gamalwork81@gmail.com",
                    FullName = "Jamal Abdelsalam Mohamed",
                    UserName = "Jamal_11",
                    PhoneNumber = "123456789",
                    ProfileImage = "Images\\ProfileImage\\Gamal.jpg"

                };
                await _userManager.CreateAsync(user, "Jamal@123");
            }

        }
        public async static Task SeedDataAsync(AncientAuraDbContext context)
        {
            if (context.Articles.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var articlesData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\Articals.json");

                var articles = JsonSerializer.Deserialize<List<Articles>>(articlesData);

                if (articles is not null && articles.Count() > 0)
                {
                    await context.AddRangeAsync(articles);
                    await context.SaveChangesAsync();
                }
            }

            if (context.Documentries.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var documentriesData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\Documentries.json");

                var documentries = JsonSerializer.Deserialize<List<Documentries>>(documentriesData);

                if (documentries is not null && documentries.Count() > 0)
                {
                    await context.AddRangeAsync(documentries);
                    await context.SaveChangesAsync();
                }
            }

            if (context.Books.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var booksData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\Books.json");

                var books = JsonSerializer.Deserialize<List<Books>>(booksData);

                if (books is not null && books.Count() > 0)
                {
                    await context.AddRangeAsync(books);
                    await context.SaveChangesAsync();
                }
            }

            if(context.AncientSites.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var ancientSitesData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\AncientSites.json");

                var ancientSites = JsonSerializer.Deserialize<List<AncientSites>>(ancientSitesData);

                if (ancientSites is not null && ancientSites.Count() > 0)
                {
                    await context.AddRangeAsync(ancientSites);
                    await context.SaveChangesAsync();
                }
            }

            if (context.ImageURLs.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var ancientSitesImagesData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\AncientSitesImages.json");

                var ancientSitesImages = JsonSerializer.Deserialize<List<ImageURLs>>(ancientSitesImagesData);

                if (ancientSitesImages is not null && ancientSitesImages.Count() > 0)
                {
                    await context.AddRangeAsync(ancientSitesImages);
                    await context.SaveChangesAsync();
                }
            }
            if (context.Videos.Count() == 0)
            {
                //D:\AncientAura\AncientAura\AncientAura.Repository\Data\DataSeed\Articals.json
                var videoData = File.ReadAllText(@"..\AncientAura.Repository\Data\DataSeed\Videos.json");

                var videosData = JsonSerializer.Deserialize<List<Videos>>(videoData);

                if (videosData is not null && videosData.Count() > 0)
                {
                    await context.AddRangeAsync(videosData);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
