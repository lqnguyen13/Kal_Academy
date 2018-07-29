using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }

        public int EventTypeId { get; set; }
        public int LocationId { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual Location Location { get; set; }
    }
}
