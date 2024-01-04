using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B2S_REST_API.Domain;
using B2S_REST_API.Models;

namespace DatabaseProductCatalogRESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandAliasController : ControllerBase
    {
        #region Variables
        private readonly Buy2SellContext _context;
        #endregion
        #region Constructor
        public BrandAliasController(Buy2SellContext context)
        {
            _context = context;
        }
        #endregion
        #region Get
        // GET: api/BrandAlias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandAlias>>> GetBrandAliases()
        {
          if (_context.BrandAliases == null)
          {
              return NotFound();
          }
            return TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.ToListAsync());
        }
        // GET: api/BrandAlias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandAlias>> GetBrandAlias(int id)
        {
          if (_context.BrandAliases == null)
          {
              return NotFound();
          }
            var brandAlias = TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.FindAsync(id));

            if (brandAlias == null)
            {
                return NotFound();
            }

            return brandAlias;
        }
        // GET: api/BrandAlias/Alias/[string]
        [HttpGet("Alias/{alias}")]
        public async Task<ActionResult<BrandAlias>> GetBrandAliasViaAlias(string alias)
        {
            if (_context.BrandAliases == null)
            {
                return NotFound();
            }

            List<BrandAlias> brandAliasList = TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.ToListAsync());
            BrandAlias brandAlias = brandAliasList.First(ba => { return ba.AliAlias.ToLower().Equals(alias.ToLower()); });

            if (brandAlias == null)
            {
                return NotFound();
            }

            return brandAlias;
        }
        // GET: api/BrandAlias/Brand/[string]
        [HttpGet("Brand/{brandName}")]
		public async Task<ActionResult<IEnumerable<BrandAlias>>> GetBrandAliasBasedBrand(string brandName)
		{
			brandName = brandName.ToLower();

			if (_context.BrandAliases == null && brandName.Length == 0)
			{
				return NotFound();
			}

			List<Brand> brands = TestItemsFilter.FilterBrands(await _context.Brands.ToListAsync());
			List<BrandAlias> brandAliases = TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.ToListAsync());

			Brand brand = brands.First(b => { return b.BrdName.ToLower().Equals(brandName); });
			return brandAliases.Where(ba => { return ba.BrdId == brand.BrdId; }).ToList();
		}
		#endregion
		#region Put
		// PUT: api/BrandAlias/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutBrandAlias(int id, BrandAlias brandAlias)
        {
            if (id != brandAlias.AliId)
            {
                return BadRequest();
            }

            _context.Entry(brandAlias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAliasExists(id))
                {
                    brandAlias.AliId = 0;
                    _context.BrandAliases.Add(brandAlias);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion
        #region Post
        // POST: api/BrandAlias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandAlias>> PostBrandAlias(BrandAlias brandAlias)
        {
          if (_context.BrandAliases == null)
          {
              return Problem("Entity set 'Buy2SellContext.BrandAliases'  is null.");
          }
            _context.BrandAliases.Add(brandAlias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrandAlias", new { id = brandAlias.AliId }, brandAlias);
        }
        #endregion
        #region Delete
        // DELETE: api/BrandAlias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandAlias(int id)
        {
            if (_context.BrandAliases == null)
            {
                return NotFound();
            }
            var brandAlias = await _context.BrandAliases.FindAsync(id);
            if (brandAlias == null)
            {
                return NotFound();
            }

            _context.BrandAliases.Remove(brandAlias);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region Exists
        private bool BrandAliasExists(int id)
        {
            return (_context.BrandAliases?.Any(e => e.AliId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
