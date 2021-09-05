using System.Collections.Generic;
using EASV_PetShop.Core.Entity;

namespace EASV_PetShop.Core.ApplicationService
{
    public interface ICustomerService
    {
        Customer NewCustomer(string firsName, string lastName, string address, string phoneNumber, string email);
        Customer CreateCustomer(Customer customer);
        Customer FindCustomerById(int id);
        List<Customer> GetAllCustomers();
        public List<Customer> GetAllByFirstName(string name);
        Customer UpdateCustomer(Customer customerUpdate);
        Customer DeleteCustomer(int id);
    }
}