using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using PayPal.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaypalCheckOutController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var apiContext = new APIContext(Paypal.getInstance().accessToken);
            //apiContext.Config = ConfigManager.Instance.GetProperties();

            //// Define any custom configuration settings for calls that will use this object.
            //apiContext.Config["connectionTimeout"] = "1000"; // Quick timeout for testing purposes

            //// Define any HTTP headers to be used in HTTP requests made with this APIContext object
            //if (apiContext.HTTPHeaders == null)
            //{
            //    apiContext.HTTPHeaders = new Dictionary<string, string>();
            //}
            //var payment = Payment.Get(apiContext, "PAY-0XL713371A312273YKE2GCNI");

            return new string[] { "value1", "value2" };
        }

        
    }
}

