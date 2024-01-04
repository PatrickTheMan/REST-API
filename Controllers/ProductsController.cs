using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B2S_REST_API.Domain;
using Newtonsoft.Json;
using B2S_REST_API.Models;
using Algorithms;
using Algorithms.Algorithm;

namespace DatabaseProductCatalogRESTApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		#region Variables
		private readonly Buy2SellContext _context;
		#endregion
		#region Constructor
		public ProductsController(Buy2SellContext context)
		{
			_context = context;
		}
		#endregion
		#region Get
		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			return TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
		}
		// GET: api/Products/[int]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = TestItemsFilter.FilterProducts(await _context.Products.FindAsync(id));

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}
		// GET: api/Products/Brand/[string]
		[HttpGet("Brand/{brandName}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductBasedBrand(string brandName)
		{
			if (_context.Brands == null || _context.Products == null)
			{
				return NotFound();
			}

			// Get list of brands and find the right brand
			List<Brand> brands = TestItemsFilter.FilterBrands(await _context.Brands.ToListAsync());
			var brand = brands.First(b =>
			{
				return b.BrdName != null && b.BrdName.ToLower().Equals(brandName.ToLower());
			}
			);

			// Get list of products and sort to only get the ones with the right brand
			List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
			return products.Where(p =>
			{
				return p.BrdId.Equals(brand.BrdId);
			}
			).ToList();
		}
		// GET: api/Products/ItemGroup/[string]
		[HttpGet("ItemGroup/{groupName}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductBasedItemGroup(string groupName)
		{
			if (_context.ItemGroups == null || _context.Products == null)
			{
				return NotFound();
			}

			// Get list of groups and find the right group
			List<ItemGroup> groups = TestItemsFilter.FilterItemGroups(await _context.ItemGroups.ToListAsync());
			var group = groups.First(g =>
			{
				return g.GrpName != null && g.GrpName.ToLower().Equals(groupName.ToLower());
			}
			);

			// Get list of products and sort to only get the ones with the right group
			List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
			return products.Where(p =>
			{
				return p.GrpId != null && p.GrpId == group.GrpId;
			}
			).ToList();
		}
		// GET: api/Products/EAN/[int]
		[HttpGet("EAN/{EANNumber}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductBasedEANNumber(string EANNumber)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}

			// Get list of products and find the first one that equals the EAN
			List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
			return products.Where(p =>
			{
				return p.PrdEanGlr == EANNumber;
			}
			).ToList();
		}
		// GET: api/Products/ProductNumber/[string]
		[HttpGet("ProductNumber/{productNumber}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductBasedProductNumber(string productNumber)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}

			// Get list of products and sort a list to get only products containing the productNumber
			List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());
			return products.Where(p =>
			{
				return p.PrdProductNumber.ToLower().Contains(productNumber.ToLower());
			}
			).ToList();
		}
		#endregion
		#region Put
		// PUT: api/Products/[int]
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.PrdId)
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
				if (!ProductExists(id))
				{
					product.PrdId = 0;
					_context.Products.Add(product);
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
		// POST: api/Products
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'Buy2SellContext.Products'  is null.");
			}
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.PrdId }, product);
		}
		#endregion
		#region Delete
		// DELETE: api/Products/[int]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		#endregion
		#region Exits
		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.PrdId == id)).GetValueOrDefault();
		}
		#endregion
		#region Get Product with Filter
		// GET: api/Products/Request/[string]
		[HttpGet("Request/{requestJson}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductViaFilter(string requestJson)
		{
			ProductRequest? productRequest = JsonConvert.DeserializeObject<ProductRequest>(requestJson);

			if (productRequest == null)
			{
				return BadRequest();
			}

			if (_context.Products == null)
			{
				return NotFound();
			}

			// Get list
			List<BrandAlias> alias = TestItemsFilter.FilterBrandAlias(await _context.BrandAliases.ToListAsync());
			List<Product> products = TestItemsFilter.FilterProducts(await _context.Products.ToListAsync());

			// Try for EAN, return specific product
			if (productRequest.EAN != null)
			{
				Product product = products.First(p => { return p.PrdEanGlr != null && p.PrdEanGlr.Equals(productRequest.EAN); });
				if (product != null)
				{
					return new List<Product>() { product };
				}
			}

			// Try for UPC, return specific product
			if (productRequest.UPC != null)
			{
				Product product = products.First(p => { return p.PrdUpc != null && p.PrdUpc.Equals(productRequest.UPC); });
				if (product != null)
				{
					return new List<Product>() { product };
				}
			}

			// Try for Brand
			if (productRequest.Brand != null)
			{
				int? brandId = alias.First(a => { return a.AliAlias.ToLower().Equals(productRequest.Brand.ToLower()); })?.BrdId;
				if (brandId != null)
				{
					products = products.Where(p => { return p.BrdId == brandId; }).ToList();
				}
			}

			// Try for ItemGroup
			if (productRequest.ItemGroup != null)
			{
				int? itemGroupId = products.First(p => { return p.Grp?.GrpName == productRequest.ItemGroup; })?.GrpId;
				if (itemGroupId != null)
				{
					products = products.Where(p => { return p.GrpId == itemGroupId; }).ToList();
				}
			}

			if (productRequest.ProductNumber == null)
			{
				return NotFound();
			}

			// Try for Productnumber and TypeNumber
			products = products
				.Where(p => {
					if (p.PrdTypeNumber != null && productRequest.TypeNumber != null && Filter.SimpleFilter(p.PrdTypeNumber)
					.Contains(Filter.SimpleFilter(productRequest.TypeNumber))
					)
						return true;
					return Filter.SimpleFilter(p.PrdProductNumber).Contains(Filter.SimpleFilter(productRequest.ProductNumber));
				})
				.ToList();

			// Cut the list
			if (productRequest.Amount == 0) // Return all
			{
				return products;
			}
			else if (productRequest.Index + productRequest.Amount > products.Count())
			{
				products = products.GetRange(productRequest.Index, products.Count() - productRequest.Index);
			}
			else
			{
				products = products.GetRange(productRequest.Index, productRequest.Amount);
			}

			// Return the products
			return products;
		}
		#endregion
		#region OCR
		// GET: api/Products/OCR/[string]
		[HttpGet("OCR/{ocrString}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductOCR(string ocrString)
		{
			ProductFinder finder = new(await _context.BrandAliases.ToListAsync());

			List<string> ocrWords = new();
			foreach (string str in ocrString.Split('¤'))
			{
				ocrWords.Add(HTTPSafeStringDecode(str));
			}

			// Filter - Using "ocrWords" list
			Dictionary<Product, int> productNumAmountPair = new();
			foreach (ProductRequest req in finder.GetProductRequests(ocrWords))
			{
				ActionResult<IEnumerable<Product>> tempList = await GetProductViaFilter(JsonConvert.SerializeObject(req));

				foreach (Product prod in tempList.Value)
				{
					try
					{
						productNumAmountPair[productNumAmountPair.First(
							pair => pair.Key.PrdProductNumber == prod.PrdProductNumber
							).Key]++;
					}
					catch
					{
						productNumAmountPair.Add(prod, 1);
					}
				}
			}

			// Create orderd list
			List<Product> products = new();
			foreach (KeyValuePair<Product, int> pair in productNumAmountPair.OrderBy(pair => pair.Value))
			{
				products.Add(pair.Key);
			}

			return products;
		}
		#endregion
		#region Private
		/// <summary>
		/// An encoder to make a string HTTP safe
		/// </summary>
		/// <param name="str">String to make safe</param>
		/// <returns>The changed string</returns>
		private static string HTTPSafeStringEncode(string str)
		{
			str = str.Replace("+", "&#43;");
			str = str.Replace("/", "%2F");
			str = str.Replace("-", "%2D");

			return str;
		}
		/// <summary>
		/// A decoder to make a HTTP string into a normal one
		/// </summary>
		/// <param name="str">HTTP safe string to make normal</param>
		/// <returns>The changed string</returns>
		private static string HTTPSafeStringDecode(string str)
		{
			str = str.Replace("&#43;", "+");
			str = str.Replace("%2F", "/");
			str = str.Replace("%2D", "-");

			return str;
		}
		#endregion
	}
}
