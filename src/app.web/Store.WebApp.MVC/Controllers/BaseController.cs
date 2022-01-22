using Microsoft.AspNetCore.Mvc;
using Store.WebApp.MVC.Models;
using System.Linq;

namespace Store.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected bool HasResponseError(ResponseResult respose)
        {
            if(respose != null && respose.Errors.Violations.Any())
            {
                foreach (var message in respose.Errors.Violations)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }
    }
}
