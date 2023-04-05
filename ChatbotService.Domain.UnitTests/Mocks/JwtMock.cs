using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ChatbotService.Domain.UnitTests.Mocks;

//special thanks to Stefán Jökull from https://stebet.net for this code snippet.
//You can find the same snippet of code on https://stebet.net/mocking-jwt-tokens-in-asp-net-core-integration-tests/
[ExcludeFromCodeCoverage]
public static class JwtMock
{
    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
    private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
    private static readonly byte[] s_key = new byte[32];

    static JwtMock()
    {
        s_rng.GetBytes(s_key);
        SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims, DateTime expires)
    {
        return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, expires, SigningCredentials));
    }
}