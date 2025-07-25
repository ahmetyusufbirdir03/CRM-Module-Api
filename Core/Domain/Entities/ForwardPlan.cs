using Shared.Bases;

namespace Domain.Entities;

public class ForwardPlan : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
