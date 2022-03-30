using Microsoft.AspNetCore.Http;
using Store.WebAPI.Service.User.Extensions;
using Store.WebAPI.Service.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Store.WebAPI.Service.User
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetId()
        {
            return IsAuthenticate() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetEmail()
        {
            return IsAuthenticate() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public string GetToken()
        {
            return IsAuthenticate() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool HasRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public bool IsAuthenticate()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}
