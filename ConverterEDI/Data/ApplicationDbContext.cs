using ConverterEDI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConverterEDI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TranslationRow> TranslationRows { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TranslationRow>()
                .HasIndex(t => new { t.SupplierItemCode, t.SupplierId })
                .IsUnique();
            builder.Entity<TranslationRow>()
                .HasIndex(t => new { t.SupplierItemCode, t.BuyerItemCode })
                .IsUnique();
        }

    }
}
