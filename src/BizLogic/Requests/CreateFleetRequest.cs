using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Requests
{
    public class CreateCompanyRequest : IRequest<CreateCompanyResponse>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateCompanyResponse
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
    }
}
