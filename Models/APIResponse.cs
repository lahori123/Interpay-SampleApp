namespace SampleAppForWebRouting.Models
{
    public class APIResponse
    {
        public Int32 status_code { get; set; }
        public string status_message { get; set; }
        public string trans_ref_no { get; set; }
        public string Token { get; set; }
        public string redirect_url { get; set; }
    }
}
