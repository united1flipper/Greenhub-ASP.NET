using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
    public class AppDbInit
    {
        private static String COMMON_TMP_PASSWORD = "123456789";
        public static void Seed(IApplicationBuilder applicationBulider)
        {
            using (var serviceScope = applicationBulider.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()

                    {
                        new Category()
                        {
                            Icon = "/images/cloud-solid.svg",
                            Description = "shadow lover",
                        },
                          new Category()
                        {
                            Icon = "/images/sun-solid.svg",
                            Description = "sun lover",
                        },
                            new Category()
                        {
                            Icon = "/images/paw-solid.svg",
                            Description = "animal lover",
                        },
                              new Category()
                        {
                            Icon = "/images/infinity-solid.svg",
                            Description = "immortals",
                        },
                                      new Category()
                        {
                            Icon = "/images/skull-solid.svg",
                            Description = "deathly",
                        }
                    });
                    context.SaveChanges();

                }
                if (!context.Plants.Any())
                {
                    context.Plants.AddRange(new List<Plant>()

                    {
                        new Plant()
                        {
                            Name = "Esheveria",
                            Description = "Echeveria spp. stem from thick-leaved rosettes. The leaves are fleshy and have a waxy cuticle on the exterior. Often the leaves are colored and a firm touch can mar the skin and leave marks. The Echeveria succulent plant is slow growing and usually doesn’t exceed 12 inches (31 cm.) in height or spread.",
                            ImageURL = "https://i.pinimg.com/564x/ab/78/36/ab78363bbddcde9f018b9bfe764e7e43.jpg",
                            Quantity = 3,
                            Price = 5.99
                        },
                        new Plant()
                        {
                            Name = "Filodendron",
                              Description = "Philodendron, (genus Philodendron), approximately 450 species of stout-stemmed climbing herbs of the family Araceae, native to tropical America. Many species begin life as vines and then transform into epiphytes (plants that live upon other plants). Because many young philodendrons are adapted to the low light levels of rainforests, they are popular potted plants for homes and offices.",
                            ImageURL = "https://a.allegroimg.com/s512/11e239/d34304424817b3414a6c9927e3f5/Philodendron-birkin-rozmiar-S-od-Hands-of-Plants",
                            Quantity = 2,
                            Price = 9.99
                        },
                        new Plant()
                        {
                            Name = "Monstera deliciosa",
                            Description = "Monstera deliciosa, commonly called split-leaf philodendron or swiss cheese plant, is native to Central America. It is a climbing, evergreen perennial vine that is perhaps most noted for its large perforated leaves on thick plant stems and its long cord-like aerial roots.",
                            ImageURL = "https://a.allegroimg.com/original/114271/2526d6e84ba09998b0f6ab05059f/Ladna-Monstera-Deliciosa-Dziurawa-Filodendron",
                            Quantity = 2,
                            Price = 30.99
                        },
                        new Plant()
                        {
                            Name = "Ctenanthe burle-marxii Fishbone",
                            Description = "Ctenanthe burle-marxii is a compact houseplant that makes a bold tropical statement. Its bright green leaves are striped with alternating lance-shaped bands, and have deep purple undersides and stems. Ctenanthe burle-marxii is nicknamed Fisbone the due to the unique leaf pattern.",
                            ImageURL = "https://i.pinimg.com/564x/28/00/2c/28002cd83272a1b7bfb360b10db1cdd3.jpg",
                            Quantity = 2,
                            Price = 10.50
                        }
                    });
                    context.SaveChanges();

                }

                //PLANTS WITH CATEGORIES
                if (!context.Plants_Categories.Any())
                {
                    context.Plants_Categories.AddRange(new List<Plant_Category>()

                    {
                    new Plant_Category()
                    {
                        PlantId = 1,
                        CategoryId = 4
                    },

                       new Plant_Category()

                       {
                           PlantId = 1,
                           CategoryId = 2
                       } });
                    context.SaveChanges();

                }


            }
        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();


                string adminEmail = "admin@greenhub.com";
                var admin = await userManager.FindByEmailAsync(adminEmail);

                if (admin == null)
                {
                    var newAdmin = new User
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdmin, COMMON_TMP_PASSWORD);
                    await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
                }

                string userEmail = "user@greenhub.com";
                var user = await userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    var newUser = new User
                    {
                        FullName = "Application User",
                        UserName = "user",
                        Email = userEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newUser, COMMON_TMP_PASSWORD);
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }

            }
        }

    }
}

