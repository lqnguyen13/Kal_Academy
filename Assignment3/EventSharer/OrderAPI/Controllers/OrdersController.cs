using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderAPI.Data;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/v1/Orders")]
    public class OrdersController : Controller
    {
        private readonly OrdersContext _ordersContext;
        private readonly IOptionsSnapshot<OrdersSettings> _settings;

        private readonly ILogger<OrdersContext> _logger;

        public OrdersController(OrdersContext ordersContext, ILogger<OrdersContext> logger,
            IOptionsSnapshot<OrderSettings> settings)
        {
            _settings = settings;
            // _ordersContext = ordersContext

            _ordersContext = ordersContext ?? throw new ArgumentNullException(nameof(ordersContext));

            ((DBContext)ordersContext).ChangeTracker.QuertyTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _logger = logger;
        }

        // POST api/Order
        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {

            order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;

            _ordersContext.Orders.Add(order);
            _ordersContext.OrderItems.AddRange(order.OrderItems);

            await _ordersContext.SaveChangesAsync();
            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        //  [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrder(int id)
        {

            var item = await _ordersContext.Orders
                .Include(x => x.OrderItems)
                .SingleOrDefaultAsync(ci => ci.OrderId == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();

        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [EventrResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersContext.Orders.ToListAsync();


            return Ok(orders);
        }



    }
}