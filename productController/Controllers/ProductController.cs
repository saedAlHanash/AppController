using AutoMapper;
using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productController.Services;

namespace productController.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDtoForList>>> GetProducts()
        {
            var list = await context.Products.Include(e => e.FileRecord).ToListAsync();
            var response = mapper.Map<List<ProductDtoForList>>(list);
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await context.Products
                .Include(e => e.FileRecord)
                .Include(e => e.CustomParm)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductDto>(product));
        }


        [HttpPut()]
        public async Task<IActionResult> PutProduct(UpdateProduct product)
        {
            if (product.Id == 0)
            {
                return BadRequest();
            }

            var p = mapper.Map<Product>(product);

            context.Entry(p).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(p.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProduct createProduct)
        {
            var file = await context.FileRecords.FindAsync(createProduct.FileId);

            if (file == null)
            {
                return NotFound();
            }

            var result = (await context.Products.AddAsync(new Product
                {
                    Name = createProduct.Name,
                    FileRecordId = createProduct.FileId,
                }
            )).Entity;

            await context.SaveChangesAsync();


            return Ok(new
            {
                Naame = result.Name,
            });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}