using Microsoft.AspNetCore.Mvc;
using B2S_REST_API.Domain;
using Microsoft.EntityFrameworkCore;
using B2S_REST_API.Models;

namespace DatabaseProductCatalogRESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  CountController : ControllerBase
    {
        #region Variables
        private readonly Buy2SellContext _context;
        #endregion
        #region Constructor
        public CountController(Buy2SellContext context)
        {
            _context = context;
        }
        #endregion
        #region Get
        // GET: api/Count/Brand/[string]
        [HttpGet("Brand/{brandName}")]
        public async Task<ActionResult<int>> GetCountViaBrand(string brandName)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }

            List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
            List<Brand> brands = TestItemsFilter.FilterBrands(await _context.Brands.ToListAsync());

            Brand brand = brands.First(b =>
            {
                return b.BrdName.ToLower().Equals(brandName.ToLower());
            });

            return products.Where(p =>
            {
                return p.BrdId == brand.BrdId;
            }).Count();
        }
        // GET: api/Count/ItemGroup/[string]
        [HttpGet("ItemGroup/{itemGroupName}")]
        public async Task<ActionResult<int>> GetCountViaItemGroup(string itemGroupName)
        {
            if (_context.ItemGroups == null)
            {
                return NotFound();
            }

            List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
            List<ItemGroup> itemGroups = TestItemsFilter.FilterItemGroups(await _context.ItemGroups.ToListAsync());

            ItemGroup itemGroup = itemGroups.First(ig =>
            {
                return ig.GrpName.ToLower().Equals(itemGroupName.ToLower());
            });

            return products.Where(p =>
            {
                return p.GrpId == itemGroup.GrpId;
            }).Count();
        }
        #endregion
    }
}

