using DesafioVerity.Domain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DesafioVerity.API.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountHolderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountHolderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Faz o login e retorna o token para acesso ao outros endpoints
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var response = await _mediator.Send(request);

            if (!response.Success)
            {
                return Unauthorized(response.Error);
            }

            return Ok(response.Token);

        }
    }
}