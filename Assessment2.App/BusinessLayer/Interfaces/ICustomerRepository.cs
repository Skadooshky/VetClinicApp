using Assignment2.App.BusinessLayer.Models;
using System.Collections.Generic;

namespace Assignment2.App.BusinessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? GetById(int id);
        List<Customer> GetAll();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
