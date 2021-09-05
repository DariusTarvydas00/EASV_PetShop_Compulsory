using System.Collections.Generic;
using EASV_PetShop.Core.Entity;

namespace EASV_PetShop.Core.DomainService
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        Customer ReadById(int id);
        IEnumerable<Customer> ReadAll();
        Customer Update(Customer customerUpdate);
        Customer Delete(int id);
    }
}