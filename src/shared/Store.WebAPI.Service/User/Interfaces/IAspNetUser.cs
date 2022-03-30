using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Store.WebAPI.Service.User.Interfaces
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetId();
        string GetEmail();
        string GetToken();
        bool IsAuthenticate();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();

    }
}
