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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly FileService _fileService;

        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, FileService fileService, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            AppProvider.Instance.BaseUrl = $"{Request.Scheme}://{Request.Host}";
            var list = await _context.Products.Include(e => e.FileRecord).ToListAsync();
            var response = _mapper.Map<List<ProductDto>>(list,
                opts =>
                {
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";

                    opts.Items["baseUrl"] = baseUrl;
                });
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            AppProvider.Instance.BaseUrl = $"{Request.Scheme}://{Request.Host}";
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


            return Ok(new
            {
                Naame = result.Name,
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