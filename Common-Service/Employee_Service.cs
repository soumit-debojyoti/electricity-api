using Electricity_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class Employee_Service
    {
        private Electricity_DAL.Employee_DAL _emp = null;
        public Employee_Service(ConnectionStrings connectionString) 
        {
            _emp = new Electricity_DAL.Employee_DAL(connectionString);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _emp.Get_Employees();
        }
    }
}
