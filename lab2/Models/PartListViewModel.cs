using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace lab2.Models
{
    public class PartListViewModel
    {
        public IEnumerable<Part> Parts { get; set; }
        public SelectList Shops { get; set; }
        public SelectList Kind { get; set; }
    }
}