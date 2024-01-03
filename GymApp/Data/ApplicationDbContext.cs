using GymApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<BodyPart> BodyParts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<BodyPart>()
            .HasMany(bp => bp.Exercises)
            .WithOne(e => e.BodyPart)
            .HasForeignKey(e => e.BodyPartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exercise>()
            .HasOne(e => e.RecordHolder)
            .WithMany()
            .HasForeignKey(e => e.RecordHolderId)

            
        base.OnModelCreating(modelBuilder);
    }

}

