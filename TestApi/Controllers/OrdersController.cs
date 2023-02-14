using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public OrdersController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("top-orders")]
        public IActionResult GetTopOrdersForToday()
        {
            var today = DateTime.Today;

            var orders = _context.Order
                .Where(o => o.Date.Date == today)
                .GroupBy(o => o.ItemID)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => new
                {
                    ItemName = _context.Item
                        .Where(i => i.itemID == g.Key)
                        .Select(i => i.name)
                        .FirstOrDefault(),
                    Quantity = g.Sum(o => o.Qty)
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("orders-per-day")]
        public IActionResult GetOrdersPerDayForLast7Days()
        {
            var end = DateTime.Today.AddDays(1);
            var start = end.AddDays(-7);

            var orders = _context.Order
                .Where(o => o.Date >= start && o.Date < end)
                .GroupBy(o => o.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("orders-by-item")]
        public IActionResult GetOrdersByItemForToday()
        {
            var today = DateTime.Today;

            var orders = _context.Order
                .Where(o => o.Date.Date == today)
                .GroupBy(o => o.ItemID)
                .Select(g => new
                {
                    ItemID = g.Key,
                    TotalOrders = g.Count()
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("highest-order-salesrep")]
        public IActionResult GetSalesRepWithHighestOrder()
        {
            var salesRep = _context.SalesReps
                .Join(_context.Order, s => s.SalesRepID, o => o.SalesRepID, (s, o) => new { SalesRep = s, Order = o })
                .GroupBy(x => x.SalesRep)
                .Select(g => new
                {
                    g.Key.SalesRepID,
                    TotalOrders = g.Sum(x => x.Order.Qty),
                    SalesRep = g.Key
                })
                .OrderByDescending(x => x.TotalOrders)
                .FirstOrDefault();

            if (salesRep == null)
            {
                return NotFound();
            }

            return Ok(salesRep.SalesRep);
        }

        [HttpGet("delivery-counts")]
        public IActionResult GetDeliveryCountsForToday()
        {
            var today = DateTime.Today;

            var deliveredCount = _context.Order
                .Count(o => o.IsDeleverd && o.Date.Date == today);

            var notDeliveredCount = _context.Order
                .Count(o => !o.IsDeleverd && o.Date.Date == today);

            var result = new
            {
                DeliveredCount = deliveredCount,
                NotDeliveredCount = notDeliveredCount
            };

            return Ok(result);
        }

        [HttpPost("{id}")]        
        public async Task<IActionResult> ChangeDeliveryStatus(int orderId)
        {
            using ( _context)
            {
                var order = await _context.Order.SingleOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    return NotFound();
                }

                order.IsDeleverd = !order.IsDeleverd;
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
