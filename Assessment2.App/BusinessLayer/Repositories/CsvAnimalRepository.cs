using Assignment2.App.BusinessLayer.Models;
using Assignment2.App.BusinessLayer.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment2.App.BusinessLayer
{
    public class CsvAnimalRepository : IAnimalRepository
    {
        private readonly string _filePath;

        public CsvAnimalRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Animal? GetById(int id)
        {
            return GetAll().FirstOrDefault(a => a.Id == id);
        }

        public List<Animal> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Animal>();
            return File.ReadAllLines(_filePath)
                       .Skip(1)
                       .Select(Animal.FromCsv)
                       .Where(a => a != null)
                       .ToList()!;
        }

        public void Add(Animal animal)
        {
            var animals = GetAll();

            animal.Id = animals.Any() ? animals.Max(a => a.Id) + 1 : 1;

            animals.Add(animal);
            WriteAll(animals);
        }


        public void Update(Animal animal)
        {
            var animals = GetAll();
            var index = animals.FindIndex(a => a.Id == animal.Id);
            if (index >= 0)
            {
                animals[index] = animal;
                WriteAll(animals);
            }
        }

        public void Delete(int id)
        {
            var animals = GetAll();
            animals = animals.Where(a => a.Id != id).ToList();
            WriteAll(animals);
        }

        private void WriteAll(List<Animal> animals)
        {
            using var writer = new StreamWriter(_filePath);
            writer.WriteLine("Id,Name,Type,Breed,Sex,OwnerId");
            foreach (var a in animals)
            {
                Animal.WriteCsv(writer, a);
            }
        }
    }
}
