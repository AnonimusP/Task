using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Models;


namespace Task1.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Phone", Price = 12 });
                _context.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Chocolate", Price = 100 });
                _context.SaveChanges();
            }
        }
        // GET: api/product
        [HttpGet]
        public async Task <IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        

        // GET api/product/5
        [HttpGet("{id}")]
        public async Task<Product> Get(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        // POST api/values
        [HttpPost]
        public Guid Post([FromBody]ProductCreateInputModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(new Product { Id = model.Id, Name = model.Name, Price = model.Price });
                _context.SaveChanges();
                return model.Id;
            }
            else
            {
                throw new Exception("Provided informations doesn't meet requirements (name and price cannot be empty, name cannot be longer than 100 characters, price must be higher than 0)");
            }
        }

        
        // PUT api/values/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProductUpdateInputModel model)
        {
            if (model.Id != Guid.Empty && ModelState.IsValid)
            {
                _context.Products.Update(new Product { Id = model.Id, Name = model.Name, Price = model.Price });
                await _context.SaveChangesAsync();
            }
            else if (model.Id == Guid.Empty)
            {
                throw new Exception("Id cannot be all zeros, must be provided in Body Request");
            }
            else if (!ModelState.IsValid)
            {
                throw new Exception("Provided informations doesn't meet requirements (name and price cannot be empty, name cannot be longer than 100 characters, price must be higher than 0)");
            }
            return NoContent();
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
