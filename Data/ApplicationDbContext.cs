using Microsoft.EntityFrameworkCore;
using NationalPark2._0.Models;

namespace NationalPark2._0.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trailers { get; set; }
    }
}
