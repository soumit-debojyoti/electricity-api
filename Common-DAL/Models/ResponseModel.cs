namespace Electricity_DAL.Models
{
    public class ResponseModel
    {
        public string access_token { get; set; }
        public int role_id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public bool isLoginSuccess { get; set; }
    }
}
