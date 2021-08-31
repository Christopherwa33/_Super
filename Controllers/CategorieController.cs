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
    public class CategorieController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CategorieController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Categorie //get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategories()
        {
            return await _context.Categorie.ToListAsync();
        }

        // GET: api/Categorie/5 //get cate specified by an id
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategory(String id)
        {
            try {
                int Id = int.Parse(id);
                var cate = await _context.Categorie.FindAsync(Id);

                if (cate == null)
                {
                    return NotFound();
                }

                return cate;
            }
            catch ( FormatException i)
            {
                return BadRequest();
            }  
        }

        // PUT: api/Categorie/5 //modify cate specified by an id
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(String id, Categoria cate)
        {
            try
            {
                int Id = int.Parse(id);
                if (Id != cate.Id || cate.Nome == null || cate.Descrizione == null )
                {
                    return BadRequest();
                }

                _context.Entry(cate).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(Id))
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

        // POST: api/Categorie //create a new cate
        
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategory(Categoria cate)
        {
            if (cate.Nome == null || cate.Descrizione == null)
            {
                return BadRequest();
            }
            _context.Categorie.Add(cate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = cate.Id }, cate);
        }

        // DELETE: api/Categorie/5 //delete a cate specified by an id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(String id)
        {
            try
            {
              int Id = int.Parse(id);
              var cate = await _context.Categorie.FindAsync(Id);
              if (cate == null)
              {
                 return NotFound();
              } 

              _context.Categorie.Remove(cate);
              await _context.SaveChangesAsync();

             return NoContent();
            }
            catch (FormatException i)
            {
                return BadRequest();
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categorie.Any(i => i.Id == id);
        }
    }

}