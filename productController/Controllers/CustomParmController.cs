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
    public class CustomParmController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CustomParmDto>>> GetAll()
        {
            var result = await context.CustomParms.ToListAsync();

            return Ok(mapper.Map<List<CustomParmDto>>(result));
        }

        [HttpPut]
        public async Task<IActionResult> PutCustomParm(UpdateCustomParmDto customParm)
        {
            context.Entry(customParm).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomParmExists(customParm.Id))
                {
                    return NotFound();
                }
            }

            return Ok(customParm);
        }


        [HttpPost]
        public async Task<ActionResult<CustomParm>> PostCustomParm(CreateCustomParmDto customParm)
        {
            context.CustomParms.Add(new CustomParm
            {
                Key = customParm.Key,
                Value = customParm.Value,
                ProductId = customParm.ProductId
            });

            await context.SaveChangesAsync();

            return Ok(customParm);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCustomParm(int id)
        {
            var customParm = await context.CustomParms.FindAsync(id);
            if (customParm == null)
            {
                return NotFound();
            }

            context.CustomParms.Remove(customParm);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomParmExists(int id)
        {
            return context.CustomParms.Any(e => e.Id == id);
        }
    }
}