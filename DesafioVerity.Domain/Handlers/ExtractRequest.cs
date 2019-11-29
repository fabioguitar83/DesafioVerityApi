using MediatR;
using System;

namespace DesafioVerity.Domain.Handlers
{
    public class ExtractRequest : IRequest<ExtractResponse>
    {
        public int AccountHolderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
