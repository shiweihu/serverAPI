using System;
using Microsoft.EntityFrameworkCore;
using ServeAPI.DataModel;

namespace ServeAPI.Db
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        
        public DbSet<Product> product_list { get; set; }
        public DbSet<MyOrder> myorders { get; set; }
    }
}

