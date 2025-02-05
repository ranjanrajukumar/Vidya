using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Vidya.Core.Security
{
    public class JwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        //public string GenerateToken(string userId, string username)
        //{
        //    // Check if the JWT Key is available
        //   // var key = _config["Jwt:Key"];
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        //    if (string.IsNullOrEmpty(key))
        //    {
        //        throw new Exception("JWT Key is missing from configuration");
        //    }

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //    var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, userId),   // User ID claim
        //        new Claim(ClaimTypes.Name, username),              // Username claim
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique Token ID
        //    };

        //    // Fetch expiration time from configuration (default to 1 hour if not found)
        //    double tokenExpiry = Convert.ToDouble(_config["Jwt:ExpiryHours"] ?? "1");
        //    var expires = DateTime.UtcNow.AddHours(tokenExpiry);

        //    // Create the token
        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims: claims,
        //        expires: expires,
        //        signingCredentials: creds
        //    );

        //    // Write the token as a string and return
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


        public string GenerateToken(string userId, string username)
        {
            // Retrieve the JWT Key (Ensure it's Base64 encoded)
            var key = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT Key is missing from configuration");
            }

            // Decode the key if it's Base64 encoded
            var keyBytes = Convert.FromBase64String(key);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId),   // User ID claim
        new Claim(ClaimTypes.Name, username),              // Username claim
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique Token ID
    };

            // Fetch expiration time from configuration (default to 1 hour if not found)
            double tokenExpiry = Convert.ToDouble(_config["Jwt:ExpiryHours"] ?? "1");
            var expires = DateTime.UtcNow.AddHours(tokenExpiry);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            // Write the token as a string and return
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
