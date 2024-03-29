﻿using GymApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<BodyPart> BodyParts { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingExercises> TrainingExercises { get; set; }
    public DbSet<TrainingSession> TraningSessions { get; set; }
    public DbSet<TrainingSessionDetails> TrainingSessionDetails { get; set; } //do usuniecia
    public DbSet<WeightRecord> WeightRecords { get; set; }

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
            .IsRequired(false);

        modelBuilder.Entity<Training>()
            .HasMany(t => t.TrainingExercises)
            .WithOne(ct => ct.Training)
            .HasForeignKey(ct => ct.TrainingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingExercises>()
            .HasOne(ct => ct.Exercise)
            .WithMany()
            .HasForeignKey(ct => ct.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TrainingSessionDetails>() //do usuniecia
       .HasKey(tsd => tsd.Id);

        modelBuilder.Entity<TrainingSessionDetails>() //do usuniecia
            .HasOne(tsd => tsd.TrainingSession)
            .WithMany()
            .HasForeignKey(tsd => tsd.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeightRecord>()
              .HasOne(wr => wr.TrainingSession)
              .WithMany()
              .HasForeignKey(wr => wr.TrainingSessionId)
              .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeightRecord>()
            .HasOne(wr => wr.Exercise)
            .WithMany()
            .HasForeignKey(wr => wr.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}

