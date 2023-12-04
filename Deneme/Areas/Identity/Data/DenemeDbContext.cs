using Deneme.Areas.Identity.Data;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Deneme.Data;

public class DenemeDbContext : IdentityDbContext<ApplicationUser>
{
  
    public DenemeDbContext(DbContextOptions<DenemeDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Department> Departments { get; set; }
   

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Doctor>()
        .HasOne(d => d.Department)
        .WithMany(d=>d.Doctors)
        .HasForeignKey(d => d.DepartmentId) 
        .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Doctor>()
            .HasMany(d => d.Appointments) 
            .WithOne(a => a.Doctor) 
            .HasForeignKey(a => a.DoctorId) 
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Department>()
            .HasMany(d =>d.Appointments) 
            .WithOne(a => a.Department)
            .HasForeignKey(a => a.DepartmentId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
    }
}


