using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        public string EventName { get; set; }

        public int EventId { get; set; }

        public string PictureUrl  { get; set; }

        public decimal UnitPrice{ get; set; }() {}

        public int Units { get; set; }

        public Order Order { get; set; }

        public int OrderID { get; set; }

       protected OrderItem()
        {
        }

        protected OrderItem(int eventID, string eventName, decimal unitPrice, string pictureUrl, int units)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }

            EventId = eventID;

            EventName = eventName;

            UnitPrice = unitPrice;

            Units = units;

            PictureUrl = pictureUrl;

        }

        public void SetPictureUri(string pictureUri)
        {
            if (!String.IsNullOrWhiteSpace(pictureUri))
            {
                PictureUrl = pictureUri;
            }
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            Units += units;
        }

    }
}
