using Demo.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles()
        {
            return [
                new ApplicationRole
                {
                    Id = new Guid("5E76C76D-4CF6-4784-8796-FD9F5C8CF29D"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = new DateTime(2025, 4, 19, 1, 2, 1).ToString(),
                },
                new ApplicationRole
                {
                    Id = new Guid("23FE4E81-5015-42A0-8D76-D1F08C6B227A"),
                    Name = "HR",
                    NormalizedName = "HR",
                    ConcurrencyStamp = new DateTime(2025, 4, 19, 1, 2, 3).ToString(),
                },
                new ApplicationRole
                {
                    Id = new Guid("B7A80D7E-24D8-4CC3-8981-EE4E1B5B1B3A"),
                    Name = "Author",
                    NormalizedName = "AUTHOR",
                    ConcurrencyStamp = new DateTime(2025, 4, 19, 1, 2, 4).ToString(),
                }
            ];
        }
    }
}