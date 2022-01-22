using Store.WebApp.MVC.Extensions;
using System;
using System.Net.Http;

namespace Store.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode(); // Se chegar até aqui e for NÃO FOR um StatusCode de sucesso, haverá uma Exception. 
            return true;
        }
    }
}
