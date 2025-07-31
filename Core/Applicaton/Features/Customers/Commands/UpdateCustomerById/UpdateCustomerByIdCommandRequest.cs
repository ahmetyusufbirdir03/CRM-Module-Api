using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Applicaton.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerByIdCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }

    [Required]
    public int TypeId { get; set; }

    [Required]
    public int StateId { get; set; }
}
