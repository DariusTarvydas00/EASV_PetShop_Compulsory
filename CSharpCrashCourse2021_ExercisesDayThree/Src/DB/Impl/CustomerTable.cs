using System.Collections.Generic;
using CrashCourse2021ExercisesDayThree.Models;

namespace CrashCourse2021ExercisesDayThree.DB.Impl
{
    public class CustomerTable
    {
        int _id = 0;
        readonly List<Customer> _customers = new List<Customer>();

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer AddCustomer(Customer customer)
        {
            _id = ++_id;
            customer.Id = _id;
            _customers.Add(customer);
            return customer;
        }
    }
}
