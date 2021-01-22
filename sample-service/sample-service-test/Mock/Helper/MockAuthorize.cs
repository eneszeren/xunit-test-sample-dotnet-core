using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace sample_service_test.Mock.Helper
{
    public static class MockAuthorize
    {
        public static void Authorize(this ControllerBase controller,
                                     string UserID = "1",
                                     string UserEmail = "kakcil@trabilisim.com",
                                     string Name = "Kemal Akçıl")
        {

            byte[] key = Encoding.ASCII.GetBytes("i'mXR$$1+W%1,?m+<+[_9BBeBfP+'.QjYBVN$'(,z$U6eNp+[&G(mG;(.SAD|6T");

            List<Claim> claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserID),
                new Claim(ClaimTypes.Email, UserEmail),
                new Claim(ClaimTypes.NameIdentifier, Name)
            };

            new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsList));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
            };

        }

    }
}
