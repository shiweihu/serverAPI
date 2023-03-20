using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServeAPI.DataModel;
using ServeAPI.Db;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderListController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly MyDbContext dbContext;


        public OrderListController(IWebHostEnvironment env, MyDbContext dbContext)
        {
            this.env = env;
            this.dbContext = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<MyOrder> Get()
        {
            var email = Request.Headers["email"].ToString();
            List<MyOrder> list = new List<MyOrder>();

            var query = from order in this.dbContext.myorders
                        join producttable in dbContext.product_list on order.productid equals producttable.id
                        where order.email == email
                        select new {
                            order,
                            producttable
                        };

            foreach (var v in query) {
                v.order.product = v.producttable;
                list.Add(v.order);
            }

            return list;
        }

       
    }
}

