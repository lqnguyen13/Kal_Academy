using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCatalogAPI.Data
{
    public class EventCatalogContext : DbContext
    {
        public EventCatalogContext(DbContextOptions options) : 
            base(options)
        {
        }

        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EventType>(ConfigureEventType);
            builder.Entity<Location>(ConfigureLocation);
            builder.Entity<Event>(ConfigureEvent);

        }


        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_hilo")
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Description)
                .HasMaxLength(1000);
            builder.Property(c => c.Price)
                .IsRequired();
            builder.Property(c => c.PictureUrl)
                .HasMaxLength(150);
            builder.Property(c => c.EventStart)
                .IsRequired();
            builder.Property(c => c.EventEnd)
                .IsRequired();

            builder.HasOne(c => c.EventType)
                .WithMany()
                .HasForeignKey(c => c.EventTypeId);

            builder.HasOne(c => c.Location)
                .WithMany()
                .HasForeignKey(c => c.LocationId);
        }

        private void ConfigureLocation(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("location_hilo")
                .IsRequired();
            builder.Property(c => c.Region)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureEventType(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("EventType");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_type_hilo")
                .IsRequired();
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

    }
}