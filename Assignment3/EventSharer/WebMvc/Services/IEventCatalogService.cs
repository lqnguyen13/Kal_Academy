using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Services
{
    public interface IEventCatalogService
    {
        Task<Event> GetEvents(int page, int take, int? loction, int? type);
        Task<IEnumerable<SelectListItem>> GetLocations();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
