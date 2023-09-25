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
        public string name { get; set; }
        [Required]
        [Display(Name = "Fee Type Code")]
        public string FeeTypeCode { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string currency { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Order ID")]
        public string order_id { get; set; }
        [Required]
        [Display(Name = "Order Desc")]
        public string order_desc { get; set; }
        [Required]
        [Display(Name = "Mobile Network")]
        public string mobile_network { get; set; }
        [Required]
        [Display(Name = "Return URL")]
        public string return_url { get; set; }
    }
}
