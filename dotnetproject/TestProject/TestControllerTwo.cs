using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroservicetwo.Controllers;
using dotnetmicroservicetwo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroservicetwo.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _CustomerController;
        private CustomerDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new CustomerDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Customers.AddRange(new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName="Alex",MobileNumber="9876543210",Email="alex@gmail.com" },
                new Customer { CustomerId = 2, CustomerName="Manju",MobileNumber="9876543217",Email="manju@gmail.com" },
                new Customer { CustomerId = 3, CustomerName="Govid",MobileNumber="9876543218",Email="govid@gmail.com" },
            });
            _context.SaveChanges();

            _CustomerController = new CustomerController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }
        [Test]
        public void CustomerClassExists()
        {
            // Arrange
            Type CustomerType = typeof(Customer);

            // Act & Assert
            Assert.IsNotNull(CustomerType, "Customer class not found.");
        }
        [Test]
        public void Customer_Properties_CustomerId_ReturnExpectedDataTypes()
        {
            // Arrange
            Customer customer = new Customer();
            PropertyInfo propertyInfo = customer.GetType().GetProperty("CustomerId");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "CustomerId property not found.");
            Assert.AreEqual(typeof(int), propertyInfo.PropertyType, "CustomerId property type is not string.");
        }
[Test]
        public void Customer_Properties_CustomerName_ReturnExpectedDataTypes()
        {
            // Arrange
            Customer customer = new Customer();
            PropertyInfo propertyInfo = customer.GetType().GetProperty("CustomerName");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "CustomerName property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "CustomerName property type is not string.");
        }
        [Test]
        public void Customer_Properties_MobileNumber_ReturnExpectedDataTypes()
        {
            // Arrange
            Customer customer = new Customer();
            PropertyInfo propertyInfo = customer.GetType().GetProperty("MobileNumber");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "MobileNumber property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "MobileNumber property type is not string.");
        }
  [Test]
        public void Customer_Properties_Email_ReturnExpectedDataTypes()
        {
            // Arrange
            Customer customer = new Customer();
            PropertyInfo propertyInfo = customer.GetType().GetProperty("Email");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Email property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Email property type is not string.");
        }

         [Test]
        public async Task GetCustomerById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _CustomerController.GetCustomerById(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetCustomerById_ExistingId_ReturnsCustomer()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _CustomerController.GetCustomerById(existingId);

            // Assert
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            var Customer = okResult.Value as Customer;
            Assert.IsNotNull(Customer);
            Assert.AreEqual(existingId, Customer.CustomerId);
        }

        [Test]
        public async Task GetCustomerById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 99; // Assuming this ID does not exist in the seeded data

            // Act
            var result = await _CustomerController.GetCustomerById(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
       [Test]
public void GetAllCustomers_ReturnsAllCustomers()
{
    // Act
    var result = _CustomerController.GetCustomers();

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result.Result);
    var okResult = result.Result as OkObjectResult;

    Assert.IsInstanceOf<IEnumerable<Customer>>(okResult.Value);
    var customers = okResult.Value as IEnumerable<Customer>;

    var customerCount = customers.Count();
    Assert.AreEqual(3, customerCount); // Assuming you have 3 Customers in the seeded data
}

[Test]
public void GetAllCustomers_ReturnsOkResult()
{
    // Act
    var result = _CustomerController.GetCustomers();

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result.Result);
}
    }
}
