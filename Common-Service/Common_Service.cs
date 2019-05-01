using Electricity_DAL;
using Electricity_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class Common_Service
    {
        private Electricity_DAL.Common_DAL _common = null;
        
        public Common_Service(ConnectionStrings connectionString)
        {
            _common = new Electricity_DAL.Common_DAL(connectionString);
        }

        public async Task<List<AddressProof>> GetAddressProof()
        {
            return await _common.GetAddressProof();
        }

        public async Task<List<IdProof>> GetIdProof()
        {
            return await _common.GetIdProof();
        }

        public async Task<List<State>> GetStates()
        {
            return await _common.GetStates();
        }

        public async Task<PagePermissionResponse> PagePermission(string roleId)
        {
            return await _common.PagePermission(roleId);
        }
        public async Task<AddWalletResponse> AddWallet(string user_security_stamp)
        {
            AddWalletResponse response = new AddWalletResponse();
            response.message = await _common.AddWallet(user_security_stamp);
            return response;
        }
    }
}
