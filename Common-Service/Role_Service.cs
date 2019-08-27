using Electricity_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class Role_Service
    {
        private Electricity_DAL.Role_DAL _role = null;
        public Role_Service(ConnectionStrings connectionString)
        {
            _role = new Electricity_DAL.Role_DAL(connectionString);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _role.Get_Roles();
        }
    }
}
