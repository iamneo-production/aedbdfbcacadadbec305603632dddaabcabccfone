using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroserviceone.Controllers;
using dotnetmicroserviceone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroserviceone.Tests
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
        public async Task AddCustomer_ValidData_ReturnsOkResult()
        {
            // Arrange
            var newCustomer = new Customer
            {
CustomerName="Abdul",MobileNumber="9876543210",Email="abdul@gmail.com"
            };

            // Act
            var result = await _CustomerController.AddCustomer(newCustomer);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task DeleteCustomer_ValidId_ReturnsNoContent()
        {
            // Arrange
              // var controller = new CustomersController(context);

                // Act
                var result = await _CustomerController.DeleteCustomer(1) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteCustomer_InvalidId_ReturnsBadRequest()
        {
                   // Act
                var result = await _CustomerController.DeleteCustomer(0) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Not a valid customer id", result.Value);
        }
    }
}
