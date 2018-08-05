using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        
        // get event types from database
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventTypes()
        {
            var items = await _catalogContext.EventTypes.ToListAsync();
            return Ok(items);
        }

        // get event locations from database
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Locations()
        {
            var items = await _catalogContext.Locations.ToListAsync();
            return Ok(items);
        }

        // get event by event id
        [HttpGet]
        [Route("events/{id:int}")]
        public async Task<IActionResult> GetEventbyId (int id)
        {
            // check if id value entered is valid, if not then return bad request
            if (id <= 0)
            {
                return BadRequest();
            }
            // get event based on matching id
            var item = await _catalogContext.Events.SingleOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                // if event is found, replace picture URL and return
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _settings.Value.ExternalCatalogBaseUrl);
                return Ok(item);
            }
            // otherwise return that event was not found
            return NotFound();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events(
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalEvents = await _catalogContext.Events.
                LongCountAsync();
            var eventsOnPage = await _catalogContext.Events
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            eventsOnPage = ChangeUrlPlaceHolder(eventsOnPage);

            var model = new PaginatedItemsViewModel<Event>(pageIndex, pageSize, totalEvents, eventsOnPage);
            return Ok(model);
        }

        // GET api/events/type/1/location/null[?pageSize=4&pageIndex=0]
        [HttpGet]
        [Route("[action]/type/{eventTypeId}/location/{locationId}")]
        public async Task<IActionResult> Events(
            int? eventTypeId,
            int? locationId,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<Event>) _catalogContext.Events;

            if (eventTypeId.HasValue)
            {
                root = root.Where(e => e.EventTypeId == eventTypeId);
            }
            if (locationId.HasValue)
            {
                root = root.Where(e => e.LocationId == locationId);
            }

            var totalEvents = await root
                    .LongCountAsync();
            var eventsOnPage = await root
                    .OrderBy(c => c.Name)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync();
            eventsOnPage = ChangeUrlPlaceHolder(eventsOnPage);

            var model = new 
                PaginatedItemsViewModel<Event>(pageIndex, pageSize, totalEvents, eventsOnPage);
            return Ok(model);
        }

        // GET api/Catalog/Items/type/1/brand/null[?pageSize=4&pageIndex=0]
        [HttpGet]
        [Route("[action]/with/{name:minlength(1)}")]
        public async Task<IActionResult> Events(string name,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalEvents = await _catalogContext.Events
                .Where(e => e.Name.StartsWith(name))
                .LongCountAsync();
            var eventsOnPage = await _catalogContext.Events
                .Where(e => e.Name.StartsWith(name))
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            eventsOnPage = ChangeUrlPlaceHolder(eventsOnPage);

            var model = new PaginatedItemsViewModel<Event>(pageIndex, pageSize, totalEvents, eventsOnPage);
            return Ok(model);
        }

        private List<Event> ChangeUrlPlaceHolder(List<Event> items)
        {
        
            items.ForEach(
                x => x.PictureUrl =
                    x.PictureUrl
                        .Replace("http://externalcatalogbaseurltobereplaced",
                            _settings.Value.ExternalCatalogBaseUrl));
            return items;
        }


        [HttpPost]
        [Route("events")]
        public async Task<IActionResult> CreateEvent(
            [FromBody] Event eventToCreate)
        {
            var newEvent = new Event
            {
                LocationId = eventToCreate.LocationId,
                EventTypeId = eventToCreate.EventTypeId,
                Description = eventToCreate.Description,
                Name = eventToCreate.Name,
                PictureUrl = eventToCreate.PictureUrl,
                Price = eventToCreate.Price,
                EventStart = eventToCreate.EventStart,
                EventEnd = eventToCreate.EventEnd
            };
            _catalogContext.Events.Add(newEvent);
            await _catalogContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventbyId), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut]
        [Route("events")]
        public async Task<IActionResult> UpdateEvent(
            [FromBody] Event eventToUpdate)
        {
            var eventUpdate = await _catalogContext.Events.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == eventToUpdate.Id);
            if (eventUpdate == null)
            {
                return NotFound(new { Message = $"Event with id {eventToUpdate.Id} not found." });
            }
            eventUpdate = eventToUpdate;
            _catalogContext.Events.Update(eventUpdate);
            await _catalogContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventbyId), new { id = eventToUpdate.Id }, eventToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _catalogContext.Events
                .SingleOrDefaultAsync(e => e.Id == id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            _catalogContext.Events.Remove(eventToDelete);
            await _catalogContext.SaveChangesAsync();
            return NoContent();
        }
    }
}