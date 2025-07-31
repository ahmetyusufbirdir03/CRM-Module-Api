namespace Applicaton.DTOs.Meeting;

public class MeetingResponseDto
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TypeId { get; set; }
    public int FormatId { get; set; }
    public int StateId { get; set; }
    public string Description { get; set; }
}
