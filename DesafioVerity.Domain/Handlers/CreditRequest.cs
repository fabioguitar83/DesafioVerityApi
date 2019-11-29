using MediatR;

namespace DesafioVerity.Domain.Handlers
{
    public class CreditRequest : IRequest<CreditResponse>
    {
        public int AccountHolderId { get; set; }
        public decimal Value { get; set; }
    }
}
