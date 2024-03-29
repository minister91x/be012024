﻿using DataAccess.Eshop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.EntitiesFrameWork
{
    public class EshopDBContext : IdentityDbContext<IdentityUser>
    {
        public EshopDBContext(DbContextOptions<EshopDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> user { get; set; }

        public virtual DbSet<Function> function { get; set; }
        public virtual DbSet<UserFunction> userfunction { get; set; }
    }
}
