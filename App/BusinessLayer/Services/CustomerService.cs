
using VetClinic.App.BusinessLayer.Interfaces;
using VetClinic.App.BusinessLayer.Models;
using System.Collections.Generic;

namespace VetClinic.App.BusinessLayer
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void AddCustomer(Customer customer)
        {
            _repository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _repository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repository.Delete(id);
        }

        public Customer? GetCustomer(int id)
        {
            return _repository.GetById(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }
    }
}
