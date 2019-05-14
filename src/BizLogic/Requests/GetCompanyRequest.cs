using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Requests
{
    public class GetCompanyRequest : IRequest<GetCompanyResponse>
    {
        public Guid CompanyId { get; set; }

        public GetCompanyRequest()
        {
        }

        public GetCompanyRequest(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetCompanyResponse
    {
        // TODO: replace with Dto
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }

        public string Address { get; set; }
    }
}
