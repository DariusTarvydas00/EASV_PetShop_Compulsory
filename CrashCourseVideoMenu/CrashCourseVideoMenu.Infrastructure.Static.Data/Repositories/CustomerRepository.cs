using System.Collections.Generic;
using System.Linq;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private static int _id = 0;
        private readonly List<Customer> _customers = new List<Customer>();
        
        public Customer Create(Customer customer)
        {
            customer.Id = _id++;
            _customers.Add(customer);
            return customer;
        }

        public Customer ReadById(int id)
        {
            return _customers.FirstOrDefault(customer => customer.Id == id);
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _customers;
        }

        public Customer Update(Customer customerUpdate)
        {
            var customerFromDb = this.ReadById(customerUpdate.Id);
            if (customerFromDb == null) return null;
            customerFromDb.Name = customerUpdate.Name;
            customerFromDb.Surname = customerUpdate.Surname;
            customerFromDb.DateOfBirth = customerUpdate.DateOfBirth;
            return customerFromDb;

        }

        public Customer Delete(int id)
        {
            var customerFound = this.ReadById(id);
            if (customerFound == null) return null;
            _customers.Remove(customerFound);
            return customerFound;

        }
    }
}