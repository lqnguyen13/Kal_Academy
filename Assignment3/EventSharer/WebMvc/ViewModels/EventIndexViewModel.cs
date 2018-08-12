﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventIndexViewModel
    {
        public IEnumerable<Event> EventItems { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public int? LocationFilterApplied { get; set; }
        public int? EventTypeFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}

