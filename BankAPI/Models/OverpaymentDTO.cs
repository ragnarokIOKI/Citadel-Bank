namespace BankAPI.Models
{
    namespace BankAPI.Models
    {
        public class OverpaymentDTO
        {
            public decimal Credit_Amount { get; set; }
            public decimal Credit_Rate { get; set; }
            public DateTime Date_Drawing { get; set; }
            public DateTime Date_Termination { get; set; }
            public decimal Overpayment { get; set; }
        }
    }
}
