using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_hero.Data;
using dotnet_hero.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_hero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public ProductsController(DatabaseContext databaseContext)=>this.databaseContext = databaseContext;

        // https://localhost:5001/products (GET)
        [HttpGet] // httos method: get,post,put,patch,delete
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
;
            return databaseContext.Products.OrderByDescending(p => p.ProductId).ToList(); //200
        }

        [HttpGet("{id}")] //https://localhost:5001/products/123
        public ActionResult<Product> GetProductById(int id) {
            var result = databaseContext.Products.Find(id);
            if(result == null){
                return NotFound();
            }
            return result;
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> SearchProducts([FromQuery] string name = "")
        {
            var result = databaseContext.Products
            .Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
          return result;
        }

        [HttpPost("")]
        public ActionResult<Product> AddProduct([FromForm] Product model)
        {
            return CreatedAtAction(nameof(GetProductById), new { id = 111 }, model);
        }
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromForm] Product model)
        {
            if (id != model.ProductId)
            {
                return BadRequest(); //400
            }

            if (id != 1150)
            {
                return NotFound();
            }
            return model;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (id != 1150)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
