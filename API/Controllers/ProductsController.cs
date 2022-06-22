using API.DAL;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        //private List<Product> _products = new List<Product>()
        //{
        //    new Product
        //    {
        //        Id = 1,
        //        Name = "Cup",
        //        Price = 1.5m
        //    },
        //     new Product
        //    {
        //        Id = 2,
        //        Name = "Monitor",
        //        Price = 400.99m
        //    },
        //      new Product
        //    {
        //        Id = 3,
        //        Name = "Samsung S22 Ultra",
        //        Price = 2200.5m
        //      },
        //           new Product
        //    {
        //        Id = 4,
        //        Name = "Carpet",
        //        Price = 8.3m
        //    },

        //};
        public ProductsController(ApplicationDbContext context )
        {
            _context = context;
        }
        [HttpGet("get/{id?}")]
        //[Route("get")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();        /* NotFound(new { status = 50000 });*/
            return Ok(product);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
           List<Product> model =  await _context.Products.ToListAsync();
            return Ok(model);
        }
        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            if (product.Name.Length > 5) return StatusCode(StatusCodes.Status400BadRequest, new { title = "You have to set name property's length maximum 6 characters" });
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);

        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            Product existed = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(existed == null) return NotFound();

            //existed.Name = product.Name;
            //existed.Price = product.Price;
            _context.Entry(existed).CurrentValues.SetValues(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            Product existed = _context.Products.Find(id);
            if (existed == null) return NotFound();
            _context.Products.Remove(existed);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, new { id });
        }
    }   
}
