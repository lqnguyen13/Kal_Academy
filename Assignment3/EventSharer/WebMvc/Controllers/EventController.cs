using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;


namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private IEventCatalogService _catalogSvc;

        public EventController(IEventCatalogService catalogSvc) =>
            _catalogSvc = catalogSvc;

        public async Task<IActionResult> Index(
            int? LocationFilterApplied,
            int? EventTypeFilterApplied, int? page)
        {

            int itemsPage = 10;
            var catalog = await
                _catalogSvc.GetEvents
                (page ?? 0, itemsPage, LocationFilterApplied,
                EventTypeFilterApplied);
            var vm = new EventIndexViewModel()
            {
                EventItems = catalog.Data,
                Location = await _catalogSvc.GetLocations(),
                EventType = await _catalogSvc.GetTypes(),
                LocationFilterApplied = LocationFilterApplied ?? 0,
                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage, //catalog.Data.Count,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }
    }
}