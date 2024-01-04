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
    public class ItemGroupsController : ControllerBase
    {
        #region Variables
        private readonly Buy2SellContext _context;
        #endregion
        #region Constructor
        public ItemGroupsController(Buy2SellContext context)
        {
            _context = context;
        }
        #endregion
        #region Get
        // GET: api/ItemGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemGroup>>> GetItemGroups()
        {
          if (_context.ItemGroups == null)
          {
              return NotFound();
          }
            return TestItemsFilter.FilterItemGroups(await _context.ItemGroups.ToListAsync());
        }
        // GET: api/ItemGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemGroup>> GetItemGroup(int id)
        {
          if (_context.ItemGroups == null)
          {
              return NotFound();
          }
            var itemGroup = TestItemsFilter.FilterItemGroups(await _context.ItemGroups.FindAsync(id));

            if (itemGroup == null)
            {
                return NotFound();
            }

            return itemGroup;
        }
        // GET: api/ItemGroups/ItemGroup/[string]
        [HttpGet("ItemGroup/{groupName}")]
        public async Task<ActionResult<ItemGroup>> GetItemGroupViaItemGroup(string groupName)
        {
            if (_context.ItemGroups == null)
            {
                return NotFound();
            }

            List<ItemGroup> itemGroups = TestItemsFilter.FilterItemGroups(await _context.ItemGroups.ToListAsync());
            ItemGroup itemGroup = itemGroups.First(ig => { return ig.GrpName != null && ig.GrpName.ToLower().Equals(groupName.ToLower()); });

            if (itemGroup == null)
            {
                return NotFound();
            }

            return itemGroup;
        }
        #endregion
        #region Put
        // PUT: api/ItemGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemGroup(int id, ItemGroup itemGroup)
        {
            if (id != itemGroup.GrpId)
            {
                return BadRequest();
            }

            _context.Entry(itemGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupExists(id))
                {
                    itemGroup.GrpId = 0;
                    _context.ItemGroups.Add(itemGroup);
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
        // POST: api/ItemGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemGroup>> PostItemGroup(ItemGroup itemGroup)
        {
          if (_context.ItemGroups == null)
          {
              return Problem("Entity set 'Buy2SellContext.ItemGroups'  is null.");
          }
            _context.ItemGroups.Add(itemGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemGroup", new { id = itemGroup.GrpId }, itemGroup);
        }
        #endregion
        #region Delete
        // DELETE: api/ItemGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemGroup(int id)
        {
            if (_context.ItemGroups == null)
            {
                return NotFound();
            }
            var itemGroup = await _context.ItemGroups.FindAsync(id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            _context.ItemGroups.Remove(itemGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region Exists
        private bool ItemGroupExists(int id)
        {
            return (_context.ItemGroups?.Any(e => e.GrpId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
