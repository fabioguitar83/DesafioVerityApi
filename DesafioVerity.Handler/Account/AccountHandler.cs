using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.Interfaces.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static DesafioVerity.Domain.Entity.TransactionType;

namespace DesafioVerity.Handler.Account
{
    public class AccountHandler : IRequestHandler<CreditRequest, CreditResponse>,
                                  IRequestHandler<DebitRequest, DebitResponse>

    {

        private readonly IAccountRepository _accountRepository;
        private readonly IExtractRepository _extractRepository;

        public AccountHandler(IAccountRepository accountRepository, IExtractRepository extractRepository)
        {
            _accountRepository = accountRepository;
            _extractRepository = extractRepository;
        }

        public async Task<CreditResponse> Handle(CreditRequest request, CancellationToken cancellationToken)
        {
            var accountFind = _accountRepository.Get(request.AccountHolderId);

            if (accountFind == null)
            {
                return new CreditResponse()
                {
                    Success = false,
                    Error = "Essa conta não existe"
                };
            }

            UpdateAccount(accountFind, request.Value, eTransactionType.Credito);
            InsertExtract(request.Value, request.AccountHolderId, eTransactionType.Credito);

            return new CreditResponse()
            {
                Success = true
            };
        }

        public async Task<DebitResponse> Handle(DebitRequest request, CancellationToken cancellationToken)
        {
            var accountFind = _accountRepository.Get(request.AccountHolderId);

            if (accountFind == null)
            {
                return new DebitResponse()
                {
                    Success = false,
                    Error = "Essa conta não existe"
                };
            }

            if ((accountFind.Value - request.Value) < 0)
            {
                return new DebitResponse()
                {
                    Success = false,
                    Error = "Essa conta não possui saldo suficiente para efetuar esse débito"
                };
            }

            UpdateAccount(accountFind, request.Value, eTransactionType.Debito);
            InsertExtract(request.Value, request.AccountHolderId, eTransactionType.Debito);

            return new DebitResponse()
            {
                Success = true
            };
        }

        private void UpdateAccount(Domain.Entity.Account accountFind, decimal value, eTransactionType transactionType)
        {

            if (transactionType == eTransactionType.Credito)
                accountFind.Value += value;
            else
                accountFind.Value -= value;

            accountFind.UpdateDate = DateTime.Now;

            _accountRepository.Update(accountFind);
        }

        private void InsertExtract(decimal value, int acoountHolderId, eTransactionType transactionType)
        {
            var extract = new Extract()
            {
                AccountHolderId = acoountHolderId,
                TransactionDate = DateTime.Now,
                Value = value,
                TransactionTypeId = transactionType.GetHashCode()
            };

            _extractRepository.Insert(extract);
        }

    }
}
