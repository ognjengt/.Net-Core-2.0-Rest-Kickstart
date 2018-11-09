using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public static class TokenHandler
    {
        /// <summary>
        /// Decodes the jwt and returns JwtSecurityToken object
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static JwtSecurityToken DecodeJwt(string jwt)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;
        }

        /// <summary>
        /// Returns the requested claim from the JWT body payload
        /// </summary>
        /// <param name="claim">Requested claim</param>
        /// <param name="jwt">Json web token, from the request header</param>
        /// <returns>Value of the claim requested</returns>
        public static string GetClaimFromToken(string claim, string jwt)
        {
            var tokenData = DecodeJwt(jwt);
            object returningClaim;
            tokenData.Payload.TryGetValue(claim, out returningClaim);

            if (String.IsNullOrEmpty(returningClaim.ToString()))
            {
                return "";
            }

            return returningClaim.ToString();
        }
    }
}
