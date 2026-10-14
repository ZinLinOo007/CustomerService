using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Repositories;
using Moq;
using Xunit;

namespace CustomerService.Tests
{
    public class CustomerServiceTests
    {
        public List<Customer> SimpleCustomer()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com",Phone= "09245678123" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Phone= "09234567891" },
                new Customer { Id = 3, Name = "Bob Johnson", Email = "bob.johnson@example.com" , Phone = "90455678123" }
            };
            return customers;
        }

        [Fact]
        public void GetCustomers_ValidCall()
        {
            //Arrange
            var expected = SimpleCustomer();

            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomers())
                .Returns(expected);
            var service = new CustomerService(mockRepository.Object);

            //Act
            var result = service.GetCustomers();

            //Assert
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.Equal(expected[0].Name, result[0].Name);
            Assert.Equal(expected[0].Email, result[0].Email);
            Assert.Equal(expected[0].Phone, result[0].Phone);

            mockRepository.Verify(
                          repo => repo.GetCustomers(),
                          Times.Once);
        }

        [Fact]
        public void GetCustomerById_ValidCall()
        {
            //Arrange
            var expected = SimpleCustomer()[2];
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomerById(expected.Id))
                .Returns(expected);
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.GetCustomerById(expected.Id);
            //Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Email, result.Email);
            Assert.Equal(expected.Phone, result.Phone);
            mockRepository.Verify(
                          repo => repo.GetCustomerById(expected.Id),
                          Times.Once);

        }

        [Fact]
        public void GetCustomerById_CustomerNotFound_ReturnsNull()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomerById(999))
                .Returns((Customer)null);
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.GetCustomerById(999);
            //Assert
            Assert.Null(result);
            mockRepository.Verify(
                          repo => repo.GetCustomerById(999),
                          Times.Exactly(1));
        }

        [Fact]
        public void AddCustomer_ValidCall()
        {
            //Arrange
            var customer = SimpleCustomer();
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.AddCustomer(customer[0]))
                .Returns(true);
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.AddCustomer(customer[0]);
            //Assert
            Assert.True(result);
            mockRepository.Verify(
                          repo => repo.AddCustomer(customer[0]),
                          Times.Exactly(1));
        }

        [Theory]
        [InlineData(null, "example@gmail.com", "09456123789")]
        [InlineData("John Doe", null, "09456123789")]
        [InlineData("John Doe", "example@gmail.com", null)]
        [InlineData("", "example@gmail.com", "09456123789")]
        [InlineData("John Doe", "", "09456123789")]
        [InlineData("John Doe", "example@gmail.com", "")]
        public void AddCustomer_InvalidInput_ReturnsFalse(string name, string email, string phone)
        {
            //Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone
            };

            var mockRepository = new Mock<ICustomerRepository>();
            var service = new CustomerService(mockRepository.Object);

            //Act
            var result = service.AddCustomer(customer);

            //Assert
            Assert.False(result);
            mockRepository.Verify(
                          repo => repo.AddCustomer(It.IsAny<Customer>()),
                          Times.Never);


        }

        [Fact]
        public void AddCustomer_RepositoryReturnsFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Aung Aung",
                Email = "aung@example.com",
                Phone = "09123456789"

            };
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.AddCustomer(customer))
                .Returns(false);
            var service = new CustomerService(mockRepository.Object);

            //Act
            var result = service.AddCustomer(customer);

            //Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.AddCustomer(customer), Times.Exactly(1));


        }

        [Fact]
        public void UpdateCustomer_ValidCall()
        {
            //Arrange
            var customer = SimpleCustomer();
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.UpdateCustomer(customer[0]))
                .Returns(true);
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.UpdateCustomer(customer[0]);
            //Assert
            Assert.True(result);
            mockRepository.Verify(
                          repo => repo.UpdateCustomer(customer[0]),
                          Times.Exactly(1));
        }

        [Fact]
        public void UpdateCustomer_RepositoryReturnsFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Aung Aung",
                Email = "aung@example.com",
                Phone = "09123456789"
            };
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.UpdateCustomer(customer))
                .Returns(false);
            var service = new CustomerService(mockRepository.Object);

            //Act
            var result = service.UpdateCustomer(customer);

            //Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.UpdateCustomer(customer), Times.Exactly(1));
        }

        [Theory]
        [InlineData(null, "example@gmail.com", "09456123789")]
        [InlineData("John Doe", null, "09456123789")]
        [InlineData("John Doe", "example@gmail.com", null)]
        [InlineData("", "example@gmail.com", "09456123789")]
        [InlineData("John Doe", "", "09456123789")]
        [InlineData("John Doe", "example@gmail.com", "")]

        public void UpdateCustomer_InvalidInput_ReturnsFalse(string name, string email, string phone)
        {
            //Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone
            };
            var mockRepository = new Mock<ICustomerRepository>();
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.UpdateCustomer(customer);
            //Assert
            Assert.False(result);
            mockRepository.Verify(
                          repo => repo.UpdateCustomer(It.IsAny<Customer>()),
                          Times.Never);
        }

        [Fact]
        public void DeleteCustomer_ValidCall()
        {
            //Arrange
            var customer = SimpleCustomer()[0];
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.DeleteCustomer(customer.Id))
                .Returns(true);
            var service = new CustomerService(mockRepository.Object);


            //Act
            var result = service.DeleteCustomer(customer.Id);
            //Assert
            Assert.True(result);

            mockRepository.Verify(
                repo => repo.DeleteCustomer(customer.Id),
                Times.Exactly(1));

        }

        [Fact]
        public void DeleteCustomer_RepositoryReturnsFalse()
        {
            //Arrange
            var customer = SimpleCustomer()[0];
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.DeleteCustomer(customer.Id))
                .Returns(false);
            var service = new CustomerService(mockRepository.Object);
            //Act
            var result = service.DeleteCustomer(customer.Id);
            //Assert
            Assert.False(result);
            mockRepository.Verify(
                repo => repo.DeleteCustomer(customer.Id),
                Times.Exactly(1));
        }
    }
}


