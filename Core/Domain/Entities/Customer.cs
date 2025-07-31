using CrmModuleApi.Shared.Bases;

namespace Domain.Entities;

public class Customer : EntityBase
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public DateTime LastContactDate { get; set; }
    public int TypeId { get; set; }
    public CustomerType CustomerType { get; set; }
    public int StateId { get; set; }
    public CustomerState CustomerState { get; set; }


}
