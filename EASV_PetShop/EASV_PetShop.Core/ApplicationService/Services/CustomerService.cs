using System.Collections.Generic;
using System.Linq;
using EASV_PetShop.Core.DomainService;
using EASV_PetShop.Core.Entity;

namespace EASV_PetShop.Core.ApplicationService.Services
{
    public class CustomerService: ICustomerService
    {
        readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer NewCustomer(string firsName, string lastName, string address, string phoneNumber, string email)
        {
            var customer = new Customer()
            {
                FirstName = firsName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
            };

            return customer;
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepository.Create(customer);
        }

        public Customer FindCustomerById(int id)
        {
            return _customerRepository.ReadById(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.ReadAll().ToList();
        }

        public List<Customer> GetAllByFirstName(string name)
        {
            var list = _customerRepository.ReadAll();
            var queryContinued = list.Where(customer => customer.FirstName.Equals(name));
            queryContinued = queryContinued.OrderBy(customer => customer.FirstName);
            return queryContinued.ToList();
        }

        public Customer UpdateCustomer(Customer customerUpdate)
        {
            var customer = FindCustomerById(customerUpdate.Id);
            customer.FirstName = customerUpdate.FirstName;
            customer.LastName = customerUpdate.LastName;
            customer.Address = customerUpdate.Address;
            customer.PhoneNumber = customerUpdate.PhoneNumber;
            customer.Email = customerUpdate.Email;
            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepository.Delete(id);
        }
    }
}