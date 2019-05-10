using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizLogic.Model;
using BizLogic.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CompaniesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET api/companies
        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] QueryCompanyRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response.Companies);
        }

        // GET api/companies/{id}
        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetAsync(Guid companyId)
        {
            try
            {
                var response = await this.mediator.Send(new GetCompanyRequest(companyId));
                return Ok(response);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/companies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCompanyRequest request)
        {
            var response = await this.mediator.Send(request);            
            return CreatedAtAction(nameof(GetAsync), new { companyId = response.CompanyId }, response);
        }
    }
}
