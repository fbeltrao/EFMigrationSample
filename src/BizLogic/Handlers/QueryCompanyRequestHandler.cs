using BizLogic.Data;
using BizLogic.Model;
using BizLogic.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizLogic.Handlers
{
    public class QueryCompanyRequestHandler : IRequestHandler<QueryCompanyRequest, QueryCompanyResponse>
    {
        private readonly CompanyDbContext dbContext;

        public QueryCompanyRequestHandler(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<QueryCompanyResponse> Handle(QueryCompanyRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Company> companies = dbContext.Companies;

            if (!string.IsNullOrEmpty(request.Name))
            {
                companies = companies.Where(x => x.Name.StartsWith(request.Name));
            }

            var list = await companies.ToListAsync(cancellationToken);
            return new QueryCompanyResponse
            {
                Companies = list
            };
        }
    }
}
