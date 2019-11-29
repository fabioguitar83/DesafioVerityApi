using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DesafioVerity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Efetua o crédito(depósito) na conta
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("credit")]
        public async Task<IActionResult> Credit(CreditViewModel viewModel)
        {
            var request = new CreditRequest()
            {
                AccountHolderId = GetAccountHolderId(),
                Value = viewModel.Value
            };

            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Efetua o debito(saque) na conta
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("debit")]
        public async Task<IActionResult> Debit(DebitViewModel viewModel)
        {
            var request = new DebitRequest()
            {
                AccountHolderId = GetAccountHolderId(),
                Value = viewModel.Value
            };

            var response = await _mediator.Send(request);
            return Ok(response);
        }


        private int GetAccountHolderId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var accountHolderId = claimsIdentity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
            return Convert.ToInt32(accountHolderId.Value);
        }

    }
}