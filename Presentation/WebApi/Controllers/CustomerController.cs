using Applicaton.Features.Customers.Commands.CreateCustomer;
using Applicaton.Features.Customers.Commands.DeleteCustomerById;
using Applicaton.Features.Customers.Queries.GetAllCustomers;
using Applicaton.Features.Customers.Queries.GetCustomerById;
using Applicaton.Features.Customers.Queries.GetCustomerByPhoneNumber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await mediator.Send(new GetAllCustomersQueryRequest());

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await mediator.Send(new GetCustomerByIdQueryRequest { Id = id});

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerByPhoneNumber(GetCustomerByPhoneNumberQueryRequest request)
        {
            var response = await mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommandRequest request)
        {
            var response = await mediator.Send(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerById( int id)
        {
            var response = await mediator.Send( new DeleteCustomerByIdCommandRequest { Id = id});

            return StatusCode(response.StatusCode, response);
        }
    }
}
