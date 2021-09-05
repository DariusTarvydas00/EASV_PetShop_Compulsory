using System.Collections.Generic;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Core.ApplicationService
{
    public interface ICustomerService
    {
        //New Customer
        Customer NewCustomer(string firstName, string lastName, string dateOfBirth);
        //Create
        Customer CreateCustomer(Customer customer);
        //Read
        Customer FindCustomerById(int id);
        List<Customer> GetAllCustomers();
        List<Customer> GetAllByFirstName(string name);
        //Update
        Customer UpdateCustomer(Customer customerUpdate);
        //Delete
        Customer DeleteCustomer(int id);
        
    }
}