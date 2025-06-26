using Demo.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Seeds
{
    public class ClaimSeed
    {
        public ApplicationUserClaim[] GetClaims()
        {
            return [
                new ApplicationUserClaim
                {
                    UserId = new Guid("83a74dcd-28de-4e38-d4c8-08ddacc2647a"),
                    ClaimType = "create_user",
                    ClaimValue = "allowed"
                },

            ];
        }
    }
}
