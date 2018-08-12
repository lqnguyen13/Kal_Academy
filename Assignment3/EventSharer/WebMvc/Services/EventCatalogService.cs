using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;

        public EventCatalogService(IOptionsSnapshot<AppSettings> settings, 
            IHttpClient httpClient)
        {
            _settings = settings;
            _apiClient = httpClient;
            _remoteServiceBaseUrl = $"{ _settings.Value.CatalogUrl }/api/eventcatalog/";
        }

        public async Task<EventCatalog> GetEvents(int page, int take, int? loction, int? type)
        {
            var allEventsUrl = ApiPaths.EventCatalog.GetAllEvents(_remoteServiceBaseUrl, page, take, loction, type);
            var dataString = await _apiClient.GetStringAsync(allEventsUrl);
            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);
            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetLocations()
        {
            var getLocationsUrl = ApiPaths.EventCatalog.GetAllLocations(_remoteServiceBaseUrl);
            var dataString = await _apiClient.GetStringAsync(getLocationsUrl);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };

            var locations = JArray.Parse(dataString);
            foreach (var location in locations.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = location.Value<string>("id"),
                    Text = location.Value<string>("region")
                });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            var getTypesUrl = ApiPaths.EventCatalog.GetAllTypes(_remoteServiceBaseUrl);
            var dataString = await _apiClient.GetStringAsync(getTypesUrl);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };

            var types = JArray.Parse(dataString);
            foreach (var eventType in types.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = eventType.Value<string>("id"),
                    Text = eventType.Value<string>("type")
                });
            }
            return items;
        }
    }
}
