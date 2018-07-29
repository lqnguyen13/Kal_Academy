using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventCatalogSeed
    {
        public static async Task SeedAsync(EventCatalogContext context)
        {
            // check if database is up to date; if not, then migrate
            context.Database.Migrate();

            // if there are no records in the database, call seed methods
            if (!context.EventTypes.Any())
            {
                context.EventTypes.AddRange(GetPreconfiguredEventTypes());
                context.SaveChanges(); // commit data
            }
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(GetPreconfiguredLocations());
                context.SaveChanges(); // commit data
            }
            if (!context.Events.Any())
            {
                context.Events.AddRange(GetPreconfiguredEvents());
                context.SaveChanges(); // commit data
            }
        }

        static IEnumerable<EventType> GetPreconfiguredEventTypes()
        {
            return new List<EventType>()
            {
                new EventType() {Type = "Music"},
                new EventType() {Type = "Classes"},
                new EventType() {Type = "Networking"}
            };
        }

        static IEnumerable<Location> GetPreconfiguredLocations()
        {
            return new List<Location>()
            {
                new Location() {Region = "Seattle, WA"},
                new Location() {Region = "Portland, OR"},
                new Location() {Region = "Bellevue, WA"}
            };
        }

        static IEnumerable<Event> GetPreconfiguredEvents()
        {
            return new List<Event>()
            {
                new Event() { LocationId=1, EventTypeId=3, Description="Meet face-to-face with hiring decision-makers from some of the areas top employers.", Name="Seattle Career Fair", EventStart=DateTime.Parse("08/23/2018 11:00:00"), EventEnd=DateTime.Parse("08/23/2018 14:00:00"), Price = 9.99M, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/1"},
                new Event() { LocationId=2, EventTypeId=3, Description="Come and enjoy beverages, hors d'oeuvres, and great conversation amongst women professionals.", Name="Seattle Summer Networking Event", EventStart=DateTime.Parse("08/21/2018 17:00:00"), EventEnd=DateTime.Parse("08/21/2018 19:30:00"), Price = 0, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/2"},
                new Event() { LocationId=3, EventTypeId=3, Description="Fusion of speed networking and roundtable discussions.", Name="Pro Speed Networking", EventStart=DateTime.Parse("08/08/2018 18:00:00"), EventEnd=DateTime.Parse("08/08/2018 20:30:00"), Price = 30, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/3"},
                new Event() { LocationId=2, EventTypeId=2, Description="Fun and fast paced session that will teach you everything you need to know about public speaking", Name="Learn Public Speaking", EventStart=DateTime.Parse("08/07/2018 19:00:00"), EventEnd=DateTime.Parse("08/07/2018 21:00:00"), Price = 5, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/4"},
                new Event() { LocationId=3, EventTypeId=2, Description="Introductory workshop on how to code.", Name="Code 101: Software Development & Careers", EventStart=DateTime.Parse("08/11/2018 09:00:00"), EventEnd=DateTime.Parse("08/12/2018 20:00:00"), Price = 199, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/5"},
                new Event() { LocationId=1, EventTypeId=2, Description="Discover how Seattle is taking on the homelessness crisis and what you can do to help.", Name="Civic Boot Camp: Housing the Homeless", EventStart=DateTime.Parse("09/14/2018 09:00:00"), EventEnd=DateTime.Parse("09/14/2018 15:30:00"), Price = 250, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/6"},
                new Event() { LocationId=1, EventTypeId=1, Description="Hall and Oats & Train at KeyArena at Seattle Center", Name="Hall and Oats & Train", EventStart=DateTime.Parse("08/11/2018 19:00:00"), EventEnd=DateTime.Parse("08/23/2018 23:00:00"), Price = 60, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/7"},
                new Event() { LocationId=2, EventTypeId=1, Description="Justin Timberlake at the Moda Center", Name="The Man of the Woods Tour", EventStart=DateTime.Parse("11/16/2018 19:30:00"), EventEnd=DateTime.Parse("11/16/2018 23:00:00"), Price = 90, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/8"},
                new Event() { LocationId=3, EventTypeId=1, Description="Florence and the Machine", Name="Florence and the Machine", EventStart=DateTime.Parse("09/11/2018 19:00:00"), EventEnd=DateTime.Parse("09/11/2018 23:00:00"), Price = 60, PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/9"},

            };
        }
    }
}
