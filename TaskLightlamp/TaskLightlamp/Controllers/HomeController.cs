using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLightlamp.models;

namespace TaskLightlamp.Controllers
{
    [Route("api/[controller]")]

    public class HomeController : Controller
    {
        private readonly Dbcontextq _context;

        public HomeController(Dbcontextq context)
        {
            _context = context;
        }
        // GET: api/products
        [HttpGet("getall")]
           public async Task<ActionResult<IEnumerable<products>>> GetAllProducts()
        {
            return await _context.products.ToListAsync();
        }

        // GET: api/products/5
        [HttpGet("{id}")]
 
        public async Task<ActionResult<products>> GetProductBYId(int id)
        {
            var @product = await _context.products.FindAsync(id);

            if (@product == null)
            {
                return NotFound();
            }

            return @product;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([Bind("NameProdect,url,ApplicationId")] products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Addact = await _context.products.AddAsync(product);
            var savs = await _context.SaveChangesAsync();
            return NoContent();
        }
  

    // PUT: api/product/5
    [HttpPut("{id}")]

        public async Task<IActionResult> Editproduct(int id, ProductViewModel products)
        {
            var @product = await _context.products.FindAsync(id);
            @product.NameProdect = products.NameProdect;
            @product.url = products.url;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproduct(int id)
        {
            var @product = await _context.products.FindAsync(id);
            if (@product == null)
            {
                return NotFound();
            }

            _context.products.Remove(@product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool productExists(int id)
        {
            return _context.products.Any(e => e.id == id);
        }

    }
}
