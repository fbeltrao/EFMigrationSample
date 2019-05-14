using System;
using System.Collections.Generic;

namespace BizLogic.Model
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
