using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string img { get; set; }

        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}