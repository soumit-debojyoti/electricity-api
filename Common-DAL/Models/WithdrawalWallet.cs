namespace Electricity_DAL.Models
{
    public class WithdrawalWallet
    {
        public int withdrawalid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string comment { get; set; }
        public bool approved { get; set; }
        public bool rejected { get; set; }
        public string admincomment { get; set; }
    }
}
