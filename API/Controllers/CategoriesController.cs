using API.DAL;
using API.DTOs.CategoryDtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id?}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpGet("all")]
        public IActionResult GetAll(int id)
        {
            List<Category> category = _context.Categories.ToList();

            CategoryGetAllDto model = new()
            {
                CategoryListItemDtos = category.Select(p => new CategoryListItemDto()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),

                TotalCount = category.Count
            };
            return Ok(model);
        }
        [HttpPost("create")]
        public IActionResult Create(CategoryPostDto categorydto)
        {
            Category category = new Category
            {
                Name = categorydto.Name,
            };
            _context.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut("update/{id}")]

        public IActionResult Update(CategoryPostDto categorydto, int id)
        {
            Category existedcategory = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (existedcategory == null) return NotFound();

            existedcategory.Name = categorydto.Name;
            _context.SaveChanges();
            return Ok(existedcategory);

        }

        [HttpDelete("delete")]

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
