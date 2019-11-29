using MediatR;

namespace DesafioVerity.Domain.Handlers
{
    public class DebitRequest : IRequest<DebitResponse>
    {
        public int AccountHolderId { get; set; }
        public decimal Value { get; set; }
    }
}
