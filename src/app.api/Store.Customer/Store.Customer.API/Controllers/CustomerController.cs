using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Customers.Domain.Entities.Commands;
using Store.Shared.Core.Mediator;
using Store.WebAPI.Service.Controllers;
using System;
using System.Threading.Tasks;

namespace Store.Customers.API.Controllers
{
    [Route("api/customers")]
    public class CustomerController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var result = await _mediatorHandler.SendCommand(
                new CreateCustomerCommand(Guid.NewGuid(), "Andre", "mtz.andremachado@gmail.com", "45455717863"));

            return CustomResponse(result);
        }
    }
}
