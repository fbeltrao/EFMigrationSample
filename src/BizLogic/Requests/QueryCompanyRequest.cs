using BizLogic.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Requests
{
    public class QueryCompanyRequest : IRequest<QueryCompanyResponse>
    {
        public string Name { get; set; }
    }

    public class QueryCompanyResponse
    {
        // TODO: replace with Dto
        public List<Company> Companies { get; set; }
    }
}
