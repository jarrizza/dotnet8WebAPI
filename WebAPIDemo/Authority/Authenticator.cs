﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPIDemo.Authority
{
    public static class Authenticator
    {

        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepository.GetApplicationByClientId(clientId);
            if (app == null) return false;
            return (app.ClientId == clientId && app.Secret == secret);
        }

        public static string CreateToken(string clientId, DateTime expiresAt, string strSecretKey)
        {

            // Security Algorithm (SigningCredentials
            // Playload (claims)
            // Signing Key

            var app = AppRepository.GetApplicationByClientId(clientId);

            var claims = new List<Claim>
            {
                new Claim("AppName", app?.Name??string.Empty)
            };

            var scopes = app?.Scopes?.Split(',');
            if (scopes != null && scopes.Length > 0)
            {
                foreach (var scope in scopes)
                {
                    claims.Add(new Claim(scope.ToLower(), "true"));
                }
            }

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static IEnumerable<Claim>? VerifyToken(string token, string strSecretKey)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            if (token.StartsWith("Bearer")) {
                token = token.Substring(6).Trim();
            }

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);

            SecurityToken securityToken;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out securityToken);

                if (securityToken != null)
                {
                    var tokenObject = tokenHandler.ReadJwtToken(token);
                    return tokenObject.Claims ?? (new List<Claim>());
                }
                else
                {
                    return null;
                }
            }
            catch (SecurityTokenException)
            {
                return null;
            }
            catch
            {
                throw ;
            }

        }
    }
}
