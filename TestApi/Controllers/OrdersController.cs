using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using TestApi.Data;
using TestApi.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrder()
        {
            var orders = await _context.Order
                .Include(o => o.OrderItems)
                .Include(o => o.Store)
                .ToListAsync();

            var result = orders.Select(o => new OrderViewModel
            {
                OrderID = o.OrderID,
                StoreName = o.Store.Name,
                SalesRepID = o.SalesRepID,
                Date = o.Date,
                IsDelivered = o.IsDelivered,
                StoreAddress = o.Store.Address
            });

            return Ok(result);
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

        [HttpGet("most-ordered-item")]
        public async Task<IActionResult> GetMostOrderedItem()
        {
            var today = DateTime.Today;

            var item = await _context.OrderItems
                .Where(oi => oi.Order.Date == today)
                .GroupBy(oi => oi.Item.name)
                .OrderByDescending(g => g.Sum(oi => oi.Qty))
                .Select(g => new {
                    ItemName = g.Key,
                    Quantity = g.Sum(oi => oi.Qty)
                })
                .FirstOrDefaultAsync();

            return Ok(item);
           
        }


        [HttpGet]
        [Route("orders/count")]
        public async Task<ActionResult<int>> GetOrdersCountToday()
        {
            DateTime today = DateTime.Today;
            int count = await _context.Order
                .Where(o => o.Date == today)
                .CountAsync();

            return Ok(count);
        }

        [HttpGet]
        [Route("api/salesreps-top3")]
        public async Task<IActionResult> GetTop3SalesReps()
        {
            var topSalesReps = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .GroupBy(o => o.SalesRepID) // group orders by sales rep
                .Select(grp => new
                {
                    SalesRep = grp.Key,
                    OrderCount = grp.Count(),
                    TotalSales = grp.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSales)
                .Take(3)
                .ToListAsync();

            return Ok(topSalesReps);
        }




        [HttpGet("delivery-counts")]
        public IActionResult GetDeliveryCountsForToday()
        {
            var today = DateTime.Today;

            var deliveredCount = _context.Order
                .Count(o => o.IsDelivered && o.Date.Date == today);

            var notDeliveredCount = _context.Order
                .Count(o => !o.IsDelivered && o.Date.Date == today);

            var result = new
            {
                DeliveredCount = deliveredCount,
                NotDeliveredCount = notDeliveredCount
            };

            return Ok(result);
        }

        [HttpPost]        
        public async Task<IActionResult> ChangeDeliveryStatus(int orderId)
        {
            using ( _context)
            {
                var order = await _context.Order.SingleOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    return NotFound();
                }

                order.IsDelivered = !order.IsDelivered;
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
        /*        [HttpPost]
                public async Task<ActionResult<Order>> PostOrder(Order order)
                {
                    _context.Order.Add(order);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
                }*/

        /* [HttpPost]
         public IActionResult PlaceOrder(string storeName, string itemName, int qty, int salesRepID)
         {
             // Look up the store by name
             Store store = _context.Store.FirstOrDefault(s => s.name == storeName);

             if (store == null)
             {
                 return NotFound("Store not found");
             }

             // Look up the item by name
             Item item = _context.Item.FirstOrDefault(i => i.name == itemName);

             if (item == null)
             {
                 return NotFound("Item not found");
             }

             // Create the order
             Order order = new Order
             {
                 StoreID = store.storeID,
                 SalesRepID = salesRepID,
                 ItemID = item.itemID,
                 Qty = qty,
                 Date = DateTime.Now
             };

             // Calculate the total amount
             order.TotalAmount = item.price * qty;

             // Add the order to the context and save changes
             _context.Order.Add(order);
             _context.SaveChanges();

             return Ok("Order placed successfully");
         }
 */
        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderModel model)
        {
            // First, retrieve the store ID using the store name
            var store = await _context.Store.FirstOrDefaultAsync(s => s.Name == model.StoreName);
            if (store == null)
            {
                return BadRequest($"Store with name {model.StoreName} not found.");
            }

            // Then, create the order
            var order = new Order
            {
                StoreID = store.StoreID,
                SalesRepID = model.SalesRepId,
                TotalAmount = 0,
                Date = DateTime.Now,
                IsDelivered = false,
                OrderItems = new List<OrderItem>()
            };

            // Add each item to the order and calculate the total amount
            foreach (var itemQty in model.ItemQuantities)
            {
                var item = await _context.Item.FirstOrDefaultAsync(it => it.name == itemQty.ItemName);
                if (item == null)
                {
                    return BadRequest($"Item with name {itemQty.ItemName} not found.");
                }
                Console.WriteLine(item.name);
                var orderItem = new OrderItem
                {
                    
                    ItemID = item.itemID,
                    Qty = itemQty.Qty
                };

                order.OrderItems.Add(orderItem);
                order.TotalAmount += item.price * itemQty.Qty;
            }

            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok($"Order with ID {order.OrderID} placed successfully.");
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
