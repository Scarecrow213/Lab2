using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string NameShop { get; set; }
        public ICollection<Part> Parts { get; set; }
        public Shop()
        {
            Parts = new List<Part>();
        }
    }
}