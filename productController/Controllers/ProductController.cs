using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using productController.Services;

namespace productController.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly FileService _fileService;

        public ProductController(AppDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _context.Products.Include(e => e.FileRecord).ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(e => e.FileRecord)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPut()]
        public async Task<IActionResult> PutProduct(Product product)
        {
            if (product.Id == 0)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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
            var result = (await _context.Products.AddAsync(new Product
                {
                    Name = createProduct.Name,
                    FileRecordId = createProduct.FileId,
                }
            )).Entity;

            await _context.SaveChangesAsync();

            var file = await _context.FileRecords.FindAsync(result.FileRecordId);

            if (result == null)
            {
                return NotFound(); // Handle the case where the product is not found
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var images = await _fileService.GetImages(result.Id, baseUrl);

            return Ok(new
            {
                Naame = result.Name,
                Image = images,
            });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}