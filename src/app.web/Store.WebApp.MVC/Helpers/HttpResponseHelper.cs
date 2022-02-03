using Store.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Helpers
{
    public abstract class HttpResponseHelper
    {
        protected StringContent GetContent(object data)
        {
            return new StringContent(
               JsonSerializer.Serialize(data),
               Encoding.UTF8,
               "application/json");
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

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
