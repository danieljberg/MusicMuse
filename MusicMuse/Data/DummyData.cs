using Microsoft.AspNetCore.Identity;
using MusicMuse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminId1 = "";
            string adminId2 = "";
            string adminId3 = "";

            string role1 = "Band";
            string decs1 = "This is the role for the admin user in a band";

            string role2 = "Musician";
            string decs2 = "This is the role for a single musician";

            string role3 = "Business";
            string decs3 = "This is a role for a business owner";

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, decs1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, decs2, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role3) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role3, decs3, DateTime.Now));
            }
            if (await userManager.FindByNameAsync("band@band.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "band@band.com",
                    Email = "band@band.com",
                    RoleString = "band"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("musician@musician.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "musician@musician.com",
                    Email = "musician@musician.com",
                    RoleString = "musician"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                adminId2 = user.Id;
            }

            if (await userManager.FindByNameAsync("business@business.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "business@business.com",
                    Email = "business@business.com",
                    RoleString = "business"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role3);
                }
                adminId3 = user.Id;
            }

        }
    }
}
