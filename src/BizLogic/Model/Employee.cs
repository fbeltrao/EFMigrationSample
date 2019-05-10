using System;

namespace BizLogic.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }        
        public Address HomeAddress { get; set; }
    }
}
