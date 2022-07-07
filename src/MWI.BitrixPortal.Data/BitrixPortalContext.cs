using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MWI.BitrixPortal.Domain.Entities;
using MWI.Core.Communication.Mediator;
using NetDevPack.Data;
using NetDevPack.Domain;
using NetDevPack.Messaging;

namespace MWI.BitrixPortal.Data
{
    public class BitrixPortalContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BitrixPortalContext(DbContextOptions<BitrixPortalContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Portal>? Portals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BitrixPortalContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateAdded") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateAdded").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateAdded").IsModified = false;
                }
            }

            var sucess = await base.SaveChangesAsync() > 0;
            if (sucess) await _mediatorHandler.PublishEvents(this);

            return sucess;
        }
    }


    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var entities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = entities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            entities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
