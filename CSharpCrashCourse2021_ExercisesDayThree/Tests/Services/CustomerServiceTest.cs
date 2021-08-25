using System;
using System.IO;
using CrashCourse2021ExercisesDayThree.Models;
using CrashCourse2021ExercisesDayThree.Services;
using Xunit;

namespace CrashCourse2021ExercisesDayThree.Tests.Services
{
    public class CustomerServiceTest
    {
        #region Data And Initialization
        
        private CustomerService service;
        private string firstName = "John";
        private readonly string lastName = "Doe";
        private readonly DateTime birthDate = DateTime.Now.AddYears(-32);

        public CustomerServiceTest()
        {
            service = new CustomerService();
        }
        
        #endregion

        #region Create
        [Fact]
        public void Create_WithFirstNameWithLessThen2Characters_ReturnsExeption()
        {
            firstName = "d";
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                        this.service.Create(firstName, lastName, birthDate));
            Assert.Equal(Constants.FirstNameException, exception.Message);

        }

        [Fact]
        public void Create_WithValidData_ReturnsCustomerWithData()
        {
            var customer = this.service.Create(firstName, lastName, birthDate);
            Assert.False(customer.Id.HasValue);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(birthDate, customer.BirthDate);
        }
        
        [Fact]
        public void CreateAndAdd_WithValidData_ReturnsCustomerWithDataAndId()
        {
            Customer customer = this.service.CreateAndAdd(firstName, lastName, birthDate);
            Assert.Equal(1, customer.Id);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(birthDate, customer.BirthDate);

            Customer customer2 = this.service.CreateAndAdd(firstName, lastName, birthDate);
            Assert.Equal(2, customer2.Id);
        }

        #endregion

        #region Find

        [Fact]
        public void FindCustomer_WithInvalidId_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.FindCustomer(-1));
            Assert.Equal(Constants.CustomerIdMustBeAboveZero, exception.Message);
        }
                 
        [Fact]
        public void FindCustomer_WithValidId_ReturnsCorrectCustomer()
        {
            var customer = this.service.CreateAndAdd(firstName, lastName, birthDate);
            var customerFound = this.service.FindCustomer(customer.Id ?? -1);
            Assert.Equal(customer, customerFound);
        }

        #endregion

        #region Search

        [Fact]
        public void SearchCustomer_WithNullSearchField_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.SearchCustomer(null, ""));
            Assert.Equal(Constants.CustomerSearchFieldCannotBeNull, exception.Message);
        }
        
        [Fact]
        public void SearchCustomer_WithNullSearchValue_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.SearchCustomer("", null));
            Assert.Equal(Constants.CustomerSearchValueCannotBeNull, exception.Message);
        }
        
        [Fact]
        public void SearchCustomer_SearchFieldNotFound_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.SearchCustomer("firstnames", "bilbo"));
            Assert.Equal(Constants.CustomerSearchFieldNotFound, exception.Message);
        }
        
        [Fact]
        public void SearchCustomer_SearchFieldIdWithNANSearchValue_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.SearchCustomer("id", "not a number (NAN)"));
            Assert.Equal(Constants.CustomerSearchValueWithFieldTypeIdMustBeANumber, exception.Message);
        }
        
        [Fact]
        public void SearchCustomer_SearchFieldOfIdAndSearchValueIsANumberLessThenOne_ReturnsException()
        {
            var exception = Assert.Throws<InvalidDataException>(testCode: () => this.service.SearchCustomer("id", "0"));
            Assert.Equal(Constants.CustomerIdMustBeAboveZero, exception.Message);
        }
        
        [Theory]
        [InlineData("Id", "1", 1)]
        [InlineData("Id", "2", 1)]
        [InlineData("FirstName", "John", 3)]
        [InlineData("LastName", "Doe", 2)]
        public void SearchCustomer_CorrectSearchFieldAndValue_ReturnsCorrectCustomers(string searchField, string searchValue, int expectedCount)
        {
            this.service.CreateAndAdd("John", "Doe", birthDate);
            this.service.CreateAndAdd("Bill", "Doe", birthDate);
            this.service.CreateAndAdd("John", "Bib", birthDate);
            this.service.CreateAndAdd("John", "Bob", birthDate);
            var customersFound = this.service.SearchCustomer(searchField, searchValue);
            Assert.Equal(expectedCount, customersFound.Count);
            switch (searchField.ToLower())
            {
                case "id":
                {
                    if (int.TryParse(searchValue, out var id))
                    {
                        foreach (var customer in customersFound)
                        {
                            Assert.Equal(id, customer.Id);
                        }
                    }
                    break;
                }
                case "firstname":
                {
                    foreach (var customer in customersFound)
                    {
                        Assert.Equal(searchValue, customer.FirstName);
                    }
                    break;
                }
                case "lastname":
                {
                    foreach (var customer in customersFound)
                    {
                        Assert.Equal(searchValue, customer.LastName);
                    }
                    break;
                }
                default:
                {
                    Assert.True(false);
                    break;
                }
            }
        }
        #endregion
    }
}
