using API.DAL;
using API.DTOs.ProductDtos;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id?}")]
        //[Route("get")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound()       /*NotFound(new {status = 50000 })*/ ;
            return Ok(product);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _context.Products.Where(p => p.DisplayStatus == true).ToListAsync();
            //foreach (var item in products)
            //{
            //    ProductListItemDto itemDto = new()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        SoldPrice = item.SoldPrice
            //    };
            //    model.Products.Add(itemDto);
            //}
            ProductGetAllDto model = new()
            {
                Products = products.Select(p => new ProductListItemDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    SoldPrice = p.SoldPrice
                }).ToList(),

                TotalCount = products.Count
            };
            return Ok(model);
        }

        [HttpPost("create")]
        public IActionResult Create(ProductPostDto productDto)
        {
            Product product = new()
            {
                Name = productDto.Name,
                SoldPrice = productDto.SoldPrice,
                CostPrice = productDto.CostPrice,
                DisplayStatus = productDto.DisplayStatus
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, ProductPostDto product)
        {
            Product existed = _context.Products.Find(id);
            if (existed == null) return NotFound();

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

        [HttpPatch("status/{id}")]
        public IActionResult ChangeDisplayStatus(int id, string statusStr)
        {
            Product product = _context.Products.Find(id);
            if (product == null) return NotFound();

            bool status;
            bool result = bool.TryParse(statusStr, out status);

            if (!result) return BadRequest();


            product.DisplayStatus = status;
            _context.SaveChanges();
            return Ok();
        }
    }   
}
