using System.ComponentModel.DataAnnotations;

namespace recharge.api.Dtos
{
    public class WithdrawDto
    {
        [Required(ErrorMessage="Account Number is required"), 
        RegularExpression("^[0-9]+$", ErrorMessage="Invalid Account Number"),
        StringLength(12, MinimumLength=10, ErrorMessage="Invalid Account Number lenght")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage="Account Name is required"),
        RegularExpression("^[a-zA-Z0-9-_ ]+$", ErrorMessage="Invalid Name. Only Alphabets, numbers, - and _")]
        public string AccountName { get; set; }

        [Required(ErrorMessage="Bank is required"),
        RegularExpression("^[a-zA-Z0-9-_ ]+$", ErrorMessage="Invalid Bank entered")]
        public string AccountBank { get; set; }

        [Required(ErrorMessage="Amount is required"),
        Range(100,50000, ErrorMessage="Should be between 100 - 50000")]
        public int Amount { get; set; }

        [Required(ErrorMessage="Pin is required"), 
        RegularExpression("^[0-9]+$", ErrorMessage="Invalid Pin"),
        StringLength(8, MinimumLength=8, ErrorMessage="Invalid Pin lenght")]
        public string Pin { get; set; }
    }
}