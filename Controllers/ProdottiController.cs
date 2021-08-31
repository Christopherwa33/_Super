using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Super.Models;

namespace Super.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly AppDataContext _context;

        public ProdottiController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Prodotti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProducts()
        {
            return await _context.Prodotti.ToListAsync();
        }

        // GET: api/Prodotti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotto>> GetProduct(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var product = await _context.Prodotti.FindAsync(Id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
            }
            catch (FormatException i)
            {
                return BadRequest();
            }
        }

        // PUT: api/Prodotti/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(String id, Prodotto product)
        {
            try
            {
                int Id = int.Parse(id);
                if (Id != product.Id || product.Nome == null)
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
                if (!ProductExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            }
            catch (FormatException i)
            {
                return BadRequest();
            }
        }

        // POST: api/Prodotti
        [HttpPost]
        public async Task<ActionResult<Prodotto>> PostProduct(Prodotto product)
        {
            _context.Prodotti.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Prodotti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(String id)
        {
            try
            {
             int Id = int.Parse(id);
             var product = await _context.Prodotti.FindAsync(Id);
             if (product == null)
             {
                return NotFound();
             }

             _context.Prodotti.Remove(product);
             await _context.SaveChangesAsync();

             return NoContent();
            }
            catch (FormatException i)
            {
                return BadRequest();
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Prodotti.Any(i => i.Id == id);
        }
    }
}
