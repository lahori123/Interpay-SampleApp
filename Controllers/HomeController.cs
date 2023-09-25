using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleAppForWebRouting.Models;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

namespace SampleAppForWebRouting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            PaymentProcessing model = new PaymentProcessing();
            //CSG testing
            model.app_id = "8412217316";
            model.app_key = "32075727";
            //Local testing
            //model.app_id = "8380762017";
            //model.app_key = "51274726";
            model.name = "Test";
            model.FeeTypeCode = "GENERALPAYMENT";
            model.mobile = "+233249567265";
            model.currency = "GHS";
            model.amount = 1;
            model.email = "name@gmail.com";
            model.order_id = "23klklk";
            model.order_desc = "Test Order";
            model.mobile_network = "MTN";
            model.return_url = "https://webhook.site/46ab2302-f14d-4b9b-8205-3cb14a80fd3f";

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PaymentProcessing model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            string URI = _configuration.GetValue<string>("ProcessPaymentURL");           
            string result = string.Empty;
            APIResponse response = new APIResponse();

            try
            {
                result = HttpPost(JsonConvert.SerializeObject(model), URI);
                response = JsonConvert.DeserializeObject<APIResponse>(result);
            }
            catch(Exception ex)
            {
                response.status_code = 0;
                response.status_message = "Error on parsing " + ex.Message + " and response from the API is " + result;
            }

            if (response.redirect_url != null)
            {
                return Redirect(response.redirect_url);
            }
            else
            {
                if(response.status_message == null)
                {
                    response.status_message = "Error while getting the response from the API.";
                }
                return Json(response.status_message);
            }
            
        }

        private static string HttpPost(string data, string Url)
        {
            // Stopwatch stopwatch = new Stopwatch();
            string response = "";
            Stream myWriter = null;
            string headers = string.Empty;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(Url);
                // objRequest.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);
                objRequest.Proxy = null;
                objRequest.UseDefaultCredentials = true;
                objRequest.Method = "POST";
                objRequest.ContentLength = byteArray.Length;
                objRequest.ContentType = "application/json";
                objRequest.Accept = "application/json";
                // objRequest.Headers.Add("X-API-KEY", ElevyConfigs.X_API_KEY);

                // original logic that was there 
                //  var cert = CertificateConfigs.Certificate;
                // objRequest.ClientCertificates.Add(cert);
                headers = objRequest.Headers.ToString();
                //Logging.GenericloggingwithHeaders(FuncName, "Info", "URL" + Url + " Request Parsed: " + data + "Headers:" + headers);
                //stopwatch.Start();
                using (myWriter = objRequest.GetRequestStream())
                {
                    myWriter.Write(byteArray, 0, byteArray.Length);
                }
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr =
                new StreamReader(objResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                    sr.Close();
                }
                //stopwatch.Stop();
                //Logging.GenericloggingwithHeaders(FuncName, "Info", "URL" + Url + " Response: " + JsonConvert.SerializeObject(response) + " Time Taken: " + stopwatch.Elapsed.TotalMilliseconds);
            }
            catch (WebException e)
            {
                //stopwatch.Stop();
                var resp = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                response += resp;

                //Logging.GenericloggingwithHeaders(FuncName, "Exception", "API Name: " + ApiName + "Function Name: " + FuncName + "URL Called: " + Url + "Excemption: " + e + " Error Response " + response + "Request API: " + data + "Trans Id: " + transid + "Time Taken: " + stopwatch.Elapsed.TotalMilliseconds + "Headers:" + headers);
                return response;
            }
            finally
            {
                if (myWriter != null)
                    myWriter.Close();
            }
            return response;
        }
    }
}