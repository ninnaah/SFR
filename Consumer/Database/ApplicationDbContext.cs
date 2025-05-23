﻿using Consumer.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<ClothingAd> ClothingAds { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClothingAd>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}