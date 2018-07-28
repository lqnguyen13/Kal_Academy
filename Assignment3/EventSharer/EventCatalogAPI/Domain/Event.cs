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
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }

        public int EventTypeId { get; set; }
        public int LoactionId { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual Location Location { get; set; }
    }
}
