using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    DbSet<Meeting> Meetings { get; set; }
    DbSet<MeetingContent> MeetingContents { get; set; }
    DbSet<MeetingState> MeetingStates { get; set; }
    DbSet<MeetingType> MeetingTypes { get; set; }
    DbSet<MeetingFormat> MeetingFormat { get; set; }

    DbSet<MeetingParticipation> MeetingParticipation { get; set; }

    DbSet<Customer> Customers { get; set; }
    DbSet<CustomerType> CustomerTypes { get; set; }
    DbSet<CustomerState> CustomerState { get; set; }
    DbSet<ForwardPlan> ForwardPlan { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<MeetingType>().HasData(
            new MeetingType {Id = 1, Type = "Sales Meeting" },
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
}
