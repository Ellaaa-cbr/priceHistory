using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 建一个序列
            modelBuilder.HasSequence<int>("ProductNoSeq", schema: "dbo")
                .StartsAt(6)
                .IncrementsBy(1);

            // 让 ProductNo 默认取序列的下一个值
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductNo)
                .HasDefaultValueSql("NEXT VALUE FOR [dbo].[ProductNoSeq]");

            base.OnModelCreating(modelBuilder);
        }        
    }
}