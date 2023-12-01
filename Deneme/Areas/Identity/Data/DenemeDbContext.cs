using Deneme.Areas.Identity.Data;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Deneme.Data;

public class DenemeDbContext : IdentityDbContext<ApplicationUser>
{
  
    public DenemeDbContext(DbContextOptions<DenemeDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Doctor> doctors { get; set; }
    public DbSet<Department> departments { get; set; }
    public DbSet<RandevuForm> randevusforms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<RandevuForm>()
            .HasOne(r => r.doctor)
            .WithMany(d => d.randevular)
            .HasForeignKey(r => r.doctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
