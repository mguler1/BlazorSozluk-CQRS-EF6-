﻿using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.Context
{
    public class BlazorSozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public BlazorSozlukContext()
        {

        }
        public BlazorSozlukContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryVote> EntryVotes { get; set; }

        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryComment> EntryComments { get; set; }

        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }

        public DbSet<EMailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var constr = "Data Source=MEHDI\\GULER;Initial Catalog=BlazorSozluk;Persist Security Info=True;user=sa;password=123456;";
                optionsBuilder.UseSqlServer(constr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSave()
        {
            var addedEntites = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => (BaseEntity)x.Entity);
            PrepareAddedEntities(addedEntites);
        }
        private void PrepareAddedEntities(IEnumerable<BaseEntity>entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreateDate==DateTime.MinValue)
                {
                    entity.CreateDate = DateTime.Now;
                }
            }
              
        }
    }
}
