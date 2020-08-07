using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class PrsDbContext : DbContext {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }

        public PrsDbContext(DbContextOptions<PrsDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Username)
                    .IsUnique(true);
            });

            builder.Entity<Vendor>(entity => {
                entity.HasIndex(e => e.Code)
                    .IsUnique(true);
            });

            builder.Entity<Product>(entity => {
                entity.HasIndex(e => e.PartNbr)
                    .IsUnique(true);

            });

            builder.Entity<Request>(entity => {

            });

            builder.Entity<RequestLine>(entity => {

            });
        }
    }
}
