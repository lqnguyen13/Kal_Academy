using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class EventCatalog
        {
            public static string GetAllEvents(string baseUri, int page, int take, int? location, int? type)
            {
                var filterQs = string.Empty;

                if (location.HasValue || type.HasValue)
                {
                    var locationQs = (location.HasValue) ? location.Value.ToString() : "null";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/location/{locationQs}";
                }
                
                return $"{baseUri}events{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetEvent(string baseUri, int id)
            {
                return $"{baseUri}events/{id}";
            }

            public static string GetAllLocations(string baseUri)
            {
                return $"{baseUri}locations";
            }

            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}eventTypes";
            }
        }
    }
}
