using System.Collections.Generic;

namespace Electricity_DAL.Models
{
    public class PagePermissionResponse
    {
        public List<PagePermissionModel> pagePermissionEdit { get; set; }
        public List<PagePermissionModel> pagePermissionView { get; set; }
        public string message { get; set; }
    }
}
