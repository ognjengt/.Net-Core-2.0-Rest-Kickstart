using Microsoft.AspNetCore.Authorization;
using Server.Repositories.Implementations;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    // Custom authorization attribute
    // Checks if the user who requested the resource, has provided his email in the Json web token
    public class HasEmail : AuthorizationHandler<HasEmail>, IAuthorizationRequirement
    {
        IUserRepository userRepository = new UserRepository();
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasEmail requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Email"))
            {
                context.Fail();
            }

            context.Succeed(requirement);
            return Task.Delay(0);
        }

    }
}
