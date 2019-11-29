using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.Interfaces.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DesafioVerity.Domain.Entity.TransactionType;

namespace DesafioVerity.Handler.Account
{
    public class ExtractHandler : IRequestHandler<ExtractRequest, ExtractResponse>
    {

        public readonly IExtractRepository _extractRepository;
        public readonly IAccountRepository _accountRepository;

        public ExtractHandler(IAccountRepository accountRepository, IExtractRepository extractRepository)
        {
            _extractRepository = extractRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ExtractResponse> Handle(ExtractRequest request, CancellationToken cancellationToken)
        {
            var response = new ExtractResponse();

            var account = _accountRepository.Get(request.AccountHolderId);

            if (account == null)
            {
                response.Success = false;
                response.Error = "Essa conta não existe";
                return response;
            }

            response.Value = account.Value;

            var detailsExtract = _extractRepository.List(request);

            detailsExtract.ToList().ForEach(detail =>
            {
                response.Details.Add(new ExtractResponse.ExtractItem()
                {
                    Date = detail.TransactionDate,
                    Value = (detail.TransactionTypeId == eTransactionType.Debito.GetHashCode()) ? detail.Value * (-1) : detail.Value,
                    TransactionType = (detail.TransactionTypeId == eTransactionType.Credito.GetHashCode()) ? "Crédito" : "Débito"
                });
            });

            response.Success = true;

            return response;
        }
    }
}
