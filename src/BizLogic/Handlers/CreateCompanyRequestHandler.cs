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
    public class CreateCompanyRequestHandler : IRequestHandler<CreateCompanyRequest, CreateCompanyResponse>
    {
        private readonly CompanyDbContext dbContext;

        public CreateCompanyRequestHandler(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CreateCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                CompanyId = request.Id ?? Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            dbContext.Companies.Add(company);
            await dbContext.SaveChangesAsync();

            return new CreateCompanyResponse
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Description = company.Description
            };
        }
    }
}
