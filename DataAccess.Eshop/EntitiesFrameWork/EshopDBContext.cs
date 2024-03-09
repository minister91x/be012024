using DataAccess.Eshop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.EntitiesFrameWork
{
    public class EshopDBContext : DbContext
    {
        public EshopDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Product> Product { get; set; }
    }
}
