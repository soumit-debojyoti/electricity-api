namespace Electricity_DAL.Models
{
    public class AddWalletResponse
    {
        public string message { get; set; }
        public int user_id { get; set; }
        public decimal amount_wallet_for_registration { get; set; }
    }

    public class AddWalletTrnsactionResponse
    {
        public string message { get; set; }
    }

    public class WalletWidthdrawalResponse
    {
        public decimal amount_wallet_widthdraw { get; set; }
        public string message { get; set; }
    }

    public class BalanceRequestResponse
    {
        public decimal amount_requested { get; set; }
        public string message { get; set; }
    }
}
