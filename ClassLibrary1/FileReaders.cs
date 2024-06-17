using System;
using System.Globalization;
using System.Linq;

namespace AtonTP
{

    public interface IZooInfoReader
    {
        public Food readFoodFile(string path);
        public List<AnimalType> readAnimalTypesFile(string path);
        public List<Animal> readAnimalsFile(string path, List<AnimalType> animalTypes);
    }
    public class FilesReader : IZooInfoReader
    {
        public FilesReader() { }

        public Food readFoodFile(string path)
        {
            if (File.Exists(path))
            {
                Food food = new Food();
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        if (double.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                        {
                            if (key.Equals("meat", StringComparison.OrdinalIgnoreCase))
                            {
                                food.meatPrice = price;
                            }
                            else if (key.Equals("fruit", StringComparison.OrdinalIgnoreCase))
                            {
                                food.fruitPrice = price;
                            }
                        }
                    }
                }
                return food;
            }
            else
            {
                throw new FileNotFoundException("Указанный файл не найден.", path);
            }
        }

        public List<AnimalType> readAnimalTypesFile(string path)
        {
            try
            {
                List<AnimalType> animalTypes = new List<AnimalType>();
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 3)
                    {
                        var animalType = new AnimalType(parts[0], double.Parse(parts[1], CultureInfo.InvariantCulture), Enum.Parse<FoodTypes>(parts[2], true),
                            parts.Length == 4 ? int.Parse(parts[3].Replace("%", "").Trim()) : (int?)null);

                        animalTypes.Add(animalType);
                    }
                }
                return animalTypes;
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<Animal> readAnimalsFile(string path, List<AnimalType> animalTypes)
        {
            try
            {
                List<Animal> animals = new List<Animal>();
                var lines = File.ReadAllLines(path);
                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        var animal = new Animal(parts[0], animalTypes.Where(r => r.name.Equals(parts[1], StringComparison.OrdinalIgnoreCase)).FirstOrDefault(), double.Parse(parts[2]));
                        animals.Add(animal);
                    }
                }
                return animals;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
