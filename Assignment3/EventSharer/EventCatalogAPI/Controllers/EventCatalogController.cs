using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EventCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EventCatalog")]
    public class EventCatalogController : Controller
    {
        private readonly EventCatalogContext _catalogContext;
        private readonly IOptionsSnapshot<EventCatalogSettings> _settings;

        public EventCatalogController(EventCatalogContext catalogContext, IOptionsSnapshot<EventCatalogSettings> settings)
        {
            _catalogContext = catalogContext;
            _settings = settings;
        }

        //TODO Add API Get, Post, Put, and Delete methods
    }
}