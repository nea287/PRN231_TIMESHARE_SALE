using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface ITokenService
    {
        public string GenerateRefreshToken(string email, string roleName);
        public (string accessToken, string refreshToken) GenerateAccessToken(string email, string roleName);
    }
}
