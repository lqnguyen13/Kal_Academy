using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IEventCatalogService _eventCatalogService;
        private readonly IIdentityService<ApplicationUser> _identityService;

        public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, IEventCatalogService eventCatalogService)
        {
            _identityService = identityService;
            _cartService = cartService;
            _eventCatalogService = eventCatalogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            if (action == "[ Checkout ]")
            {
                return RedirectToAction("Create", "Order");
            }

            try
            {
                var user = _identityService.Get(HttpContext.User);
                var basket = await _cartService.SetQuantities(user, quantities);
                var vm = await _cartService.UpdateCart(basket);
            }
            catch (Exception)//TODO: BrokenCircuitException
            {

                HandleBrokenCircuitException();
            }
            return View();
        }

        public async Task<IActionResult> AddToCart(Event eventDetails)
        {
            try
            {
                if (eventDetails.Id != null)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var theEvent = new CartItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Quantity = 1,
                        EventName = eventDetails.Name,
                        PictureUrl = eventDetails.PictureUrl,
                        EventPrice = eventDetails.Price,
                        EventId = eventDetails.Id
                    };
                    await _cartService.AddItemToCart(user, theEvent);
                }
                return RedirectToAction("Index", "EventCatalog");
            }
            catch (Exception)//TODO:BrokenCircuitException
            {
                HandleBrokenCircuitException();
            }
            return RedirectToAction("Index", "EventCatalog");
        }

        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMsg"] = "cart service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }
    }
}