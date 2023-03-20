using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServeAPI.Db;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [Route("[controller]")]
    public class ProductListCountController : ControllerBase
    {

        private readonly MyDbContext dbContext;

        public ProductListCountController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public int GetTotalCount()
        {

            var products = from p in dbContext.product_list select p;

            var total = products.Count();

            return total;

        }
    }
}

