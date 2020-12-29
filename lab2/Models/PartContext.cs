using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace lab2.Models
{
    public class PartContext : DbContext
    {
        public DbSet<Part> Parts { get; set; }
        public DbSet<Shop> Shops { get; set; }
    }
}