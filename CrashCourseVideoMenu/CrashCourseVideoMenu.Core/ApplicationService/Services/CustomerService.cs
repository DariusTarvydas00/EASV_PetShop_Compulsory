using System.Collections.Generic;
using System.Linq;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Core.ApplicationService.Services
{
    public class CustomerService: ICustomerService
    {
        readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepo = customerRepository;
        }

        public Customer NewCustomer(string firstName, string lastName, string dateOfBirth)
        {
            var cust = new Customer()
            {
                Name = firstName,
                Surname = lastName,
                DateOfBirth = dateOfBirth,
            };
            return cust;
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepo.Create(customer);
        }

        public Customer FindCustomerById(int id)
        {
            return _customerRepo.ReadById(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.ReadAll().ToList();
        }

        public List<Customer> GetAllByFirstName(string name)
        {
            var list = _customerRepo.ReadAll();
            var queryContinued = list.Where(cust => cust.Name.Equals(name));
            queryContinued.OrderBy(customer => customer.Name);
            return queryContinued.ToList();
        }

        public Customer UpdateCustomer(Customer customerUpdate)
        {
            var customer = FindCustomerById(customerUpdate.Id);
            customer.Name = customerUpdate.Name;
            customer.Surname = customer.Surname;
            customer.DateOfBirth = customer.DateOfBirth;
            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepo.Delete(id);
        }
    }
}