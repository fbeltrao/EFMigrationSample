using BizLogic.Data;
using BizLogic.Model;
using BizLogic.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizLogic.Handlers
{
    public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, GetCompanyResponse>
    {
        private readonly CompanyDbContext dbContext;

        public GetCompanyRequestHandler(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetCompanyResponse> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = await this.dbContext.Companies.FindAsync(request.CompanyId);
            if (company == null)
                throw new EntityNotFoundException();

            return new GetCompanyResponse
            {
                CompanyId = company?.CompanyId ?? Guid.Empty,
                Name = company?.Name,
                Description = company.Description,
                City = company.City,
            };
        }
    }
}
