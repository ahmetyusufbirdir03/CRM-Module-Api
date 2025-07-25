using Shared.Bases;

namespace Domain.Entities;

public class Meeting : BaseEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TypeId { get; set; }
    public MeetingType MeetingType { get; set; }
    public int FormatId { get; set; }
    public MeetingFormat MeetingFormat { get; set; }
    public int StateId { get; set; }
    public MeetingState MeetingState { get; set; }
    public int ContentId { get; set; }
    public MeetingContent MeetingContent { get; set; }
    public ICollection<MeetingParticipation> MeetingParticipations { get; set; }


}
