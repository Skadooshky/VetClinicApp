
using Assignment2.App.BusinessLayer.Interfaces;
using Assignment2.App.BusinessLayer.Models;
using System.Collections.Generic;

namespace Assignment2.App.BusinessLayer
{
    public class AnimalService
    {
        private readonly IAnimalRepository _repository;

        public AnimalService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public void AddAnimal(Animal animal)
        {
            _repository.Add(animal);
        }

        public void UpdateAnimal(Animal animal)
        {
            _repository.Update(animal);
        }

        public void DeleteAnimal(int id)
        {
            _repository.Delete(id);
        }

        public Animal? GetAnimal(int id)
        {
            return _repository.GetById(id);
        }

        public List<Animal> GetAllAnimals()
        {
            return _repository.GetAll();
        }
    }
}
