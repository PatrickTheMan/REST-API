using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B2S_REST_API.Domain;
using B2S_REST_API.Models;

namespace DatabaseProductCatalogRESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        #region Variables
        private readonly Buy2SellContext _context;
        #endregion
        #region Constructor
        public BrandsController(Buy2SellContext context)
        {
            _context = context;
        }
        #endregion
        #region Get
        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
          if (_context.Brands == null)
          {
              return NotFound();
          }
          return TestItemsFilter.FilterBrands(await _context.Brands.ToListAsync());
        }
        // GET: api/Brands/Alias/[string]
        [HttpGet("Alias/{aliasName}")]
        public async Task<ActionResult<Brand>> GetBrandBasedAlias(string aliasName)
        {
            aliasName = aliasName.ToLower();

            if (_context.Brands == null && aliasName.Length == 0)
            {
                return NotFound();
            }

            List<BrandAlias> brandAliases = TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.ToListAsync());
            List<Brand> brands = TestItemsFilter.FilterBrands(await _context.Brands.ToListAsync());

            BrandAlias brandAlias = brandAliases.First(ba => { return ba.AliAlias.ToLower().Equals(aliasName); });
            return brands.First(b => { return b.BrdId == brandAlias.BrdId; });
        }
        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
          if (_context.Brands == null)
          {
              return NotFound();
          }
          var brand = TestItemsFilter.FilterBrands(await _context.Brands.FindAsync(id));

          if (brand == null)
          {
              return NotFound();
          }

          return brand;
        }
        #endregion
        #region Put
        // PUT: api/Brands/[int]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.BrdId)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    brand.BrdId = 0;
                    _context.Brands.Add(brand);
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
        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
          if (_context.Brands == null)
          {
              return Problem("Entity set 'Buy2SellContext.Brands'  is null.");
          }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            _context.BrandAliases.Add(new() { BrdId = brand.BrdId, AliAlias = brand.BrdName});
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.BrdId }, brand);
        }
        #endregion
        #region Delete
        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);
            var brandAlias = (await _context.BrandAliases.ToListAsync()).Where(ba => ba.BrdId == brand.BrdId);

            if (brand == null)
            {
                return NotFound();
            }

            foreach (var alias in brandAlias)
            {
                _context.BrandAliases.Remove(alias);
            }
            await _context.SaveChangesAsync();
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region Exists
        private bool BrandExists(int id)
        {
            return (_context.Brands?.Any(e => e.BrdId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
