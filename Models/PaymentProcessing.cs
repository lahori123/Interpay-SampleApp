using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleAppForWebRouting.Models
{
    public class PaymentProcessing
    {
        [Required]
        [Display(Name = "App ID")]
        public string app_id { get; set; }
        [Required]
        [Display(Name = "App Key")]
        public string app_key { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Fee Type Code")]
        public string FeeTypeCode { get; set; }
        [Required]
        public Int32 Mobile { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Order ID")]
        public string order_id { get; set; }
        [Required]
        [Display(Name = "Order Desc")]
        public string order_desc { get; set; }
        [Required]
        [Display(Name = "Mobile Network")]
        public string MobileNetwork { get; set; }
        [Required]
        [Display(Name = "Return URL")]
        public string ReturnURL { get; set; }
    }
}
