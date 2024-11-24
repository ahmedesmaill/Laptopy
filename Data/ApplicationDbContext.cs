using Laptopy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Laptopy.Data
{
    public class ApplicationDbContext : IdentityDbContext  <ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }


    }
}
