using CrmModuleApi.Shared.Bases;

namespace Domain.Entities;

public class MeetingParticipation : EntityBase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser AppUser { get; set; }
    public int MeetingId { get; set; }
    public Meeting Meeting { get; set; }
}
