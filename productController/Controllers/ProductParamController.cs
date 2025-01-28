using AutoMapper;
using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace productController.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductParamController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        // GET: api/ProductParam
        [HttpGet]
        public async Task<ActionResult<ProductParam>> GetProductParams()
        {
            var list = await context.ProductParams
                .Include(e => e.Product)
                .Include(e => e.Product.FileRecord)
                .ToListAsync();
            return Ok((mapper.Map<List<ProductParamDto>>(list)));
        }

        [HttpGet]
        public async Task<ActionResult<ProductParam>> GetProductParam(int id)
        {
            var productParam = await context.ProductParams.FindAsync(id);

            if (productParam == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductParam>(productParam));
        }

        [HttpPut]
        public async Task<IActionResult> PutProductParam(UpdateProductParamDto productParam)
        {
            context.Entry(productParam).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductParamExists(productParam.Id))
                {
                    return NotFound();
                }
            }

            return Ok(await GetProductParam(productParam.Id));
        }

        [HttpPost]
        public async Task<ActionResult<ProductParam>> PostProductParam(CreateProductParamDto productParam)
        {
            var product = await context.Products.FindAsync(productParam.ProductId);

            if (product == null)
            {
                return NotFound();
            }


            var item = context.ProductParams.Add(new ProductParam
            {
                ProductId = productParam.ProductId,
                Product = product,
            }).Entity;

            await context.SaveChangesAsync();

            return Ok(item);
        }

        // DELETE: api/ProductParam/5
        [HttpDelete]
        public async Task<IActionResult> DeleteProductParam(int id)
        {
            var productParam = await context.ProductParams.FindAsync(id);
            if (productParam == null)
            {
                return NotFound();
            }

            context.ProductParams.Remove(productParam);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductParamExists(int id)
        {
            return context.ProductParams.Any(e => e.Id == id);
        }
    }
}