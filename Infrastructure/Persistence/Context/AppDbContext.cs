using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.Reflection;
using System.Security.Claims;

namespace Persistence.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly string userEmail;
    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "System";
    }
    
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingState> MeetingStates { get; set; }
    public DbSet<MeetingType> MeetingTypes { get; set; }
    public DbSet<MeetingFormat> MeetingFormat { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
    public DbSet<CustomerState> CustomerState { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Customer>()
        .HasIndex(c => c.Email)
        .IsUnique()
        .HasDatabaseName("UX_Customers_Email")
        .HasFilter("[DeletedBy] IS NULL");

        modelBuilder.Entity<Customer>()
        .HasIndex(c => c.PhoneNumber)
        .IsUnique()
        .HasDatabaseName("UX_Customers_PhoneNumber")
        .HasFilter("[DeletedBy] IS NULL");

        modelBuilder.Entity<MeetingType>().HasData(
            new MeetingType { Id = 1, Type = "Sales Meeting" },
            new MeetingType { Id = 2, Type = "Proposal" },
            new MeetingType { Id = 3, Type = "Customer Support" },
            new MeetingType { Id = 4, Type = "Contract Signing" },
            new MeetingType { Id = 5, Type = "Introduction Meeting" },
            new MeetingType { Id = 6, Type = "Payment Discussion" },
            new MeetingType { Id = 7, Type = "Feedback Gathring" }
            );

        modelBuilder.Entity<MeetingState>().HasData(
            new MeetingState { Id = 1, State = "Scheduled" },
            new MeetingState { Id = 2, State = "Rescheduled" },
            new MeetingState { Id = 3, State = "Completed" },
            new MeetingState { Id = 4, State = "Pending" },
            new MeetingState { Id = 5, State = "Cancelled" },
            new MeetingState { Id = 6, State = "Postponed" }
            );

        modelBuilder.Entity<MeetingFormat>().HasData(
            new MeetingFormat { Id = 1, Format = "Face to Face" },
            new MeetingFormat { Id = 2, Format = "Phone Call" },
            new MeetingFormat { Id = 3, Format = "Video Conference" },
            new MeetingFormat { Id = 4, Format = "Email" },
            new MeetingFormat { Id = 5, Format = "Instant Messaging" }
            );

        modelBuilder.Entity<CustomerType>().HasData(
            new CustomerType { Id = 1, Type = "Buyer" },
            new CustomerType { Id = 2, Type = "Seller" },
            new CustomerType { Id = 3, Type = "Buyer-Seller" },
            new CustomerType { Id = 4, Type = "Investor" }
            );

        modelBuilder.Entity<CustomerState>().HasData(
            new CustomerState { Id = 1, State = "New" },
            new CustomerState { Id = 2, State = "Former" },
            new CustomerState { Id = 3, State = "Partner" },
            new CustomerState { Id = 4, State = "Prospect" }
            );
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                var createdDateProp = entry.Entity.GetType().GetProperty("CreatedDate");
                createdDateProp?.SetValue(entry.Entity, DateTime.UtcNow);
                    
                var createdByProp = entry.Entity.GetType().GetProperty("CreatedBy");
                createdByProp?.SetValue(entry.Entity, userEmail);
            }

            if (entry.State == EntityState.Modified)
            {
                var updatedDateProp = entry.Entity.GetType().GetProperty("UpdatedDate");
                updatedDateProp?.SetValue(entry.Entity, DateTime.UtcNow);

                var updatedByProp = entry.Entity.GetType().GetProperty("UpdatedBy");
                updatedByProp?.SetValue(entry.Entity, userEmail);
            }

            if (entry.State == EntityState.Deleted)
            {
                var deletedDateProp = entry.Entity.GetType().GetProperty("DeletedDate");
                deletedDateProp?.SetValue(entry.Entity, DateTime.UtcNow);

                var deletedByProp = entry.Entity.GetType().GetProperty("DeletedBy");
                deletedByProp?.SetValue(entry.Entity, userEmail);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


}

