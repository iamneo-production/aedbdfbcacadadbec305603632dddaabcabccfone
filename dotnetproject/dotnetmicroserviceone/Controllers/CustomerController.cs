using Microsoft.AspNetCore.Mvc;
using dotnetmicroserviceone.Models;

namespace dotnetmicroserviceone.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext customerDbContext;
    public CustomerController(CustomerDbContext _customerDbContext)
    {
        customerDbContext = _customerDbContext;
    }

    [HttpPost]
    public async Task<ActionResult> AddCustomer(Customer customer)
    {
        if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Return detailed validation errors
    }
        await customerDbContext.Customers.AddAsync(customer);
        await customerDbContext.SaveChangesAsync();
        return Ok();
    }

     [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid customer id");

            var customer = await customerDbContext.Customers.FindAsync(id);
              customerDbContext.Customers.Remove(customer);
                await customerDbContext.SaveChangesAsync();
            return NoContent();
        }
}