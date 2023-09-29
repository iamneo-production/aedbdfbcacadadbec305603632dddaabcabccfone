using Microsoft.AspNetCore.Mvc;
using dotnetmicroservicetwo.Models;

namespace dotnetmicroservicetwo.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext customerDbContext;
    public CustomerController(CustomerDbContext _customerDbContext)
    {
        customerDbContext = _customerDbContext;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        //return customerDbContext.Customers;
        var customers = customerDbContext.Customers.ToList();

    if (customers == null || customers.Count == 0)
    {
        return NotFound(); // Return NotFound when no customers are found
    }

    return Ok(customers);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
    var customer = await customerDbContext.Customers.FindAsync(id);
    if (customer == null)
    {
        return NotFound(); // Return NotFound when the customer is not found
    }
    return Ok(customer);
    }
}