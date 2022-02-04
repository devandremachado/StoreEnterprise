using Microsoft.AspNetCore.Mvc;
using Store.WebApp.MVC.Models;

namespace Store.WebApp.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Title = "Ocorreu um erro!";
                modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Title = "Ops! Página não encontrada.";
                modelErro.Message = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Title = "Acesso Negado";
                modelErro.Message = "Você não tem permissão para fazer isto.";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }

        [Route("sistema-indisponivel")]
        public IActionResult SystemUnavailable()
        {
            var modelErro = new ErrorViewModel
            {
                Title = "Sistema indisponível",
                Message = "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
                ErrorCode = 500
            };

            return View("Error", modelErro);
        }
    }
}
