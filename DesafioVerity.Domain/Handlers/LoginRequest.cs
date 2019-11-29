using MediatR;

namespace DesafioVerity.Domain.Handlers
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public int Agency { get; set; }
        public int AccountNumber { get; set; }
        public string Password { get; set; }
    }
}
