namespace Domain.Entities;

public class MeetingContent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ForwardPlanId { get; set; }
    public ForwardPlan ForwardPlan { get; set; }
}
