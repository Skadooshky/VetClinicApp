using Assignment2.App.BusinessLayer.Models;
using System.Collections.Generic;

namespace Assignment2.App.BusinessLayer.Interfaces
{
    public interface IAnimalRepository
    {
        Animal? GetById(int id);
        List<Animal> GetAll();
        void Add(Animal animal);
        void Update(Animal animal);
        void Delete(int id);
    }
}
