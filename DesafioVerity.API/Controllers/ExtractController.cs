using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DesafioVerity.Domain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioVerity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExtractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna o extrato da conta 
        /// </summary>
        /// <param name="startDate">Formato: dd-mm-yyyy</param>
        /// <param name="endDate">Formato: dd-mm-yyyy</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("start-date/{startDate}/end-date/{endDate}")]
        public async Task<IActionResult> ExtractDetail(string startDate, string endDate)
        {
            var response = await _mediator.Send(new ExtractRequest()
            {
                AccountHolderId = GetAccountHolderId(),
                StartDate = Convert.ToDateTime(startDate),
                EndDate = Convert.ToDateTime(endDate)
            });
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