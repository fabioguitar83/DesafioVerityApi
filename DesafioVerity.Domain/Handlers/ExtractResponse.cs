using System;
using System.Collections.Generic;

namespace DesafioVerity.Domain.Handlers
{
    public class ExtractResponse
    {

        public ExtractResponse()
        {
            Details = new List<ExtractItem>();
        }

        public bool Success { get; set; }
        public string Error { get; set; }
        public decimal Value { get; set; }
        public IList<ExtractItem> Details { get; set; }

        public class ExtractItem
        {
            public DateTime Date { get; set; }
            public decimal Value { get; set; }
            public string TransactionType { get; set; }
        }
    }
}
