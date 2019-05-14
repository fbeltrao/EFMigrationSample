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
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; internal set; }
    }

    public class CreateCompanyResponse
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
    }
}
