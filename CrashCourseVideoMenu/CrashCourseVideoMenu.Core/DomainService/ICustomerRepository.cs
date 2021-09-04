using System.Collections.Generic;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Core.DomainService
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        Customer ReadById(int id);
        List<Customer> ReadAll();
        Customer Update(Customer customerUpdate);
        Customer Delete(int id);
    }
}