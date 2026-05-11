using VetClinic.App.BusinessLayer.Interfaces;
using VetClinic.App.BusinessLayer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VetClinic.App.BusinessLayer
{
    public class CsvCustomerRepository : ICustomerRepository
    {
        private readonly string _filePath;

        public CsvCustomerRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Customer? GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Customer>();
            return File.ReadAllLines(_filePath)
                       .Skip(1)
                       .Select(Customer.FromCsv)
                       .Where(c => c != null)
                       .ToList()!;
        }

        public void Add(Customer customer)
        {
            var customers = GetAll();

            customer.Id = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;

            customers.Add(customer);
            WriteAll(customers);
        }


        public void Update(Customer customer)
        {
            var customers = GetAll();
            var index = customers.FindIndex(c => c.Id == customer.Id);
            if (index >= 0)
            {
                customers[index] = customer;
                WriteAll(customers);
            }
        }

        public void Delete(int id)
        {
            var customers = GetAll();
            customers = customers.Where(c => c.Id != id).ToList();
            WriteAll(customers);
        }

        private void WriteAll(List<Customer> customers)
        {
            using var writer = new StreamWriter(_filePath);
            writer.WriteLine("Id,FirstName,Surname,PhoneNumber,Address");
            foreach (var c in customers)
            {
                Customer.WriteCsv(writer, c);
            }
        }
    }
}
