﻿using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region DbSets / Properties

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<RefreshTokens> RefreshTokens { get; set; } = null!;
        public DbSet<Quotes> Quotes { get; set; } = null!;
        public DbSet<FavoriteQuote> FavoriteQuotes { get; set; } = null!;
        public DbSet<DailyQuote> DailyQuotes { get; set; } = null!;

        #endregion DbSets / Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        #endregion Protected Methods
    }
}
