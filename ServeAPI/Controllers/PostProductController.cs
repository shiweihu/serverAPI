using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServeAPI.Db;
using ServeAPI.DataModel;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostProductController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly MyDbContext dbContext;

        public PostProductController(IWebHostEnvironment env, MyDbContext dbContext)
        {
            this.env = env;
            this.dbContext = dbContext;
            
        }

        private async Task<List<string>> OnPostUploadAsync(IReadOnlyList<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<string> paths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    string trustedFileNameForFileStorage = Path.GetRandomFileName() + $"{formFile.FileName}";
                    var path = Path.Combine(env.ContentRootPath,"MyStaticFiles","images",
                        trustedFileNameForFileStorage);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await formFile.CopyToAsync(stream);
                        paths.Add(Request.Scheme+"://"+Request.Host+"/"+"StaticFiles/images/" + trustedFileNameForFileStorage);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return paths;
        }

        private async Task<int> InsertNewProduct(string title, string? description,decimal price ,List<string> paths)
        {

            Product newProduct = new Product();
            newProduct.title = title;
            newProduct.description = description;
            newProduct.price = price;
            //var utfEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //var gbkEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
          
            //var bytes = utfEncoding.GetBytes(newProduct.title);
            //newProduct.title = gbkEncoding.GetString(bytes);
            //if (newProduct.description != null) {
            //    bytes = utfEncoding.GetBytes(newProduct.description);
            //    newProduct.description = gbkEncoding.GetString(bytes);
            //}
            paths.ForEach(delegate (string path)
            {
                if (newProduct.image_url!= null && !newProduct.image_url.Equals(""))
                {
                    newProduct.image_url += ";";
                }
                newProduct.image_url += path;
            });
            dbContext.product_list.Add(newProduct);
            return await dbContext.SaveChangesAsync();
        }

        //// POST api/values
        [HttpPost]
        public async Task<IActionResult> PostFiles([FromForm] IEnumerable<IFormFile> files, [FromForm] string title, [FromForm] string description,[FromForm] decimal price)
        {
            List<string> paths = await OnPostUploadAsync(files.ToList());
            if (paths.Count != 0)
            {

                int result = await InsertNewProduct(title, description,price, paths);

                return Ok(new { result });
            }


            return Problem("");
        }
    }
}