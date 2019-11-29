using DesafioVerity.Domain.Common;
using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.Interfaces.Entity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioVerity.Handler.Account
{
    public class AccountHolderHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAccountHolderRepository _accountHolderRepository;
        private readonly JwtConfigurations _jwtConfigurations;

        public AccountHolderHandler(IAccountHolderRepository accountHolderRepository, IConfiguration configuration, IOptions<JwtConfigurations> jwtConfigurations)
        {
            _accountHolderRepository = accountHolderRepository;
            _jwtConfigurations = jwtConfigurations.Value;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = new LoginResponse();

            var accountHolder = _accountHolderRepository.Login(request);
            if (accountHolder == null)
            {
                response.Success = false;
                response.Error = "Usuario ou senha invalido";
                return response;
            }

            var token = await CreateJwt(accountHolder);

            response.Success = true;
            response.Token = token;



            return response;

        }

        private async Task<string> CreateJwt(AccountHolder accountHolder)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfigurations.Secret);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(_jwtConfigurations.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfigurations.Issuer,
                Audience = _jwtConfigurations.Audience,
                Subject = GetUserClaims(accountHolder)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptior));
        }

        private ClaimsIdentity GetUserClaims(AccountHolder accountHolder)
        {

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, accountHolder.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, accountHolder.Name));

            return claims;
        }
    }
}
