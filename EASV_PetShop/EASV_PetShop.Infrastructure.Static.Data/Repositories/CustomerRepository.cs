using System.Collections.Generic;
using EASV_PetShop.Core.DomainService;
using EASV_PetShop.Core.Entity;

namespace EASV_PetShop.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {

        private static int _id = 1;
        private static readonly List<Customer> Customers = new List<Customer>();

        public Customer Create(Customer customer)
        {
            customer.Id = _id++;
            Customers.Add(customer);
            return customer;
        }

        public Customer ReadById(int id)
        {
            foreach (var customer in Customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }

            return null;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return Customers;
        }

        public Customer Update(Customer customerUpdate)
        {
            var customerFromDb = this.ReadById(customerUpdate.Id);
            if (customerFromDb != null)
            {
                customerFromDb.FirstName = customerUpdate.FirstName;
                customerFromDb.LastName = customerUpdate.LastName;
                customerFromDb.Address = customerUpdate.Address;
                customerFromDb.Email = customerUpdate.Email;
                customerFromDb.PhoneNumber = customerUpdate.PhoneNumber;
                return customerFromDb;
            }

            return null;
        }

        public Customer Delete(int id)
        {
            var customerFound = this.ReadById(id);

            if (customerFound != null)
            {
                Customers.Remove(customerFound);
                return customerFound;
            }

            return null;
        }
    }
}