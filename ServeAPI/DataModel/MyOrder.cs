using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServeAPI.DataModel
{
    [PrimaryKey("myorderid", "productid", "email")]
    public class MyOrder
	{
        
        [StringLength(50)]
        public string myorderid { get; set; }
        public int productid { get; set; }
        public int num { get; set; }
        [StringLength(100)]
        public string email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime time { get; set; }
        public Product? product { get; set; }
        public string method { get; set; }
        public int state { get; set; } = 0;

        public MyOrder(string myorderid, int productid, int num, string email, DateTime time,string method)
        {
            this.myorderid = myorderid;
            this.productid = productid;
            this.num = num;
            this.email = email;
            this.time = time;
            this.method = method;
        }

        
    }
}

