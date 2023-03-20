using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServeAPI.Db;
using ServeAPI.DataModel;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        private readonly int pageSize = 12; 

        public ProductListController(MyDbContext dbContext) {
            this.dbContext = dbContext;
        }

      

        // GET api/values/5
        [HttpGet]
        public async Task<ProductPackage> Get(int pageIndex)
        {
           
            var products = from p in dbContext.product_list select p;
            var query = products.Skip(pageIndex* pageSize).Take(pageSize).AsNoTracking();
            Product[] list = await query.ToArrayAsync();

            var total = products.Count();


            return new ProductPackage(list,total,pageSize);

        }
    }
    public class ProductPackage
    {
        public Product[] list { get; set; }
        public int total { get; set; } = 0;
        public int pageSize { get; set; } = 12;

        public ProductPackage(Product[] list, int total,int pageSize)
        {
            this.list = list;
            this.total = total;
            this.pageSize = pageSize;
        }
    }
}

