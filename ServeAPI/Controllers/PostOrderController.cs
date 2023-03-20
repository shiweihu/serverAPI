using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServeAPI.DataModel;
using ServeAPI.Db;
using ServeAPI.Paypal;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PostOrderController : ControllerBase
    {

        private readonly IWebHostEnvironment env;
        private readonly MyDbContext dbContext;
        private readonly PaypalService paypalService;


        public PostOrderController(IWebHostEnvironment env, MyDbContext dbContext,PaypalService paypalService)
        {
            this.env = env;
            this.dbContext = dbContext;
            this.paypalService = paypalService;
        }



        // POST api/values
        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<int> PostAsync([FromBody] OrderInfo info)
        {

            PaypalOrder? paypalOrder = await paypalService.CheckOrder(info.orderid);

            if (paypalOrder?.status == "COMPLETED")
            {
                var email = Request.Headers["email"];
                for (int i = 0; i < info.productids.Length; i++)
                {
                    MyOrder o = new MyOrder(info.orderid, info.productids[i], info.productnum[i], email.ToString(), DateTime.Now, info.method);
                    dbContext.myorders.Add(o);
                }
                return await dbContext.SaveChangesAsync();
            }
            return 0;
            

        }
        public class OrderInfo
        {
            public int[] productids { get; set; }
            public int[] productnum { get; set; }
            public string orderid { get; set; }
            public string method { get; set; }
            public OrderInfo(int[] productids, int[] productnum, string orderid,string method)
            {
                this.productids = productids;
                this.productnum = productnum;
                this.method = method;
                this.orderid = orderid;
            }
        }
    }
}

