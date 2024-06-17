using System;


namespace AtonTP
{
    public enum FoodTypes
    {
        meat,
        fruit,
        both
    }

    public interface IAnimal
    {
        public AnimalType type { get; }
        public double weight { get; }
    }

    public class Food
    {
        public double fruitPrice;
        public double meatPrice;
    }

    public class AnimalType
    {
        public string name { get; private set; }
        public double ratio { get; private set; }
        public FoodTypes foodType { get; private set; }
        public double? percent { get; private set; }

        public AnimalType() { }
        public AnimalType(string name, double ratio, FoodTypes foodType, double? percent)
        {
            this.name = name;
            this.ratio = ratio;
            this.foodType = foodType;
            this.percent = percent;
        }
    }

    public class Animal : IAnimal
    {
        public string name { get; private set; }
        public AnimalType type { get; private set; }
        public double weight { get; private set; }

        public Animal() { }
        public Animal(string name, AnimalType type, double weight)
        {
            this.name = name;
            this.type = type;
            this.weight = weight;
        }
    }

    public interface IZoo
    {
        public List<Animal> animals { get; }
        public Food food { get; }
        public double? costOfAnimalsKeeping { get; }
        public void setCostOfAnimalsKeeping(double cost);
    }

    public class Zoo : IZoo
    {
        public List<Animal> animals { get; private set; }
        public List<AnimalType> animalTypes { get; private set; }
        public Food food { get; private set; }
        public double? costOfAnimalsKeeping { get; private set; }

        public Zoo() { }
        public Zoo(Food food, List<Animal> animals)
        {
            this.food = food;
            this.animals = animals;
        }
        public Zoo(IZooInfoReader reader, string foodPath, string animalTypesPath, string animalsPath)
        {
            food = reader.readFoodFile(foodPath);
            animalTypes = reader.readAnimalTypesFile(animalTypesPath);
            animals = reader.readAnimalsFile(animalsPath, animalTypes);
        }

        public void setCostOfAnimalsKeeping(double cost)
        {
            costOfAnimalsKeeping = cost;
        }
    }

    public interface IZooCostCalc
    {
        public void calculateCost(IZoo zoo);
    }

    public class Calculator : IZooCostCalc
    {
        public void calculateCost(IZoo zoo) 
        {
            double cost = 0;
            foreach (Animal animal in zoo.animals) 
            { 
                switch (getFoodType(animal))
                {
                    case FoodTypes.meat:
                        cost += animal.weight * animal.type.ratio * zoo.food.meatPrice;
                        break;

                    case FoodTypes.fruit:
                        cost += animal.weight * animal.type.ratio * zoo.food.fruitPrice;
                        break;

                    case FoodTypes.both:
                        cost += animal.weight * animal.type.ratio * zoo.food.meatPrice * (animal.type.percent ?? 0) +
                            (zoo.food.fruitPrice < zoo.food.meatPrice ?
                            animal.weight * animal.type.ratio * zoo.food.fruitPrice * (1 - (animal.type.percent ?? 0)) :
                            animal.weight * animal.type.ratio * zoo.food.meatPrice * (1 - (animal.type.percent ?? 0)));
                        break;
                }
            }
            zoo.setCostOfAnimalsKeeping(cost);
        }

        public FoodTypes getFoodType(Animal animal)
        {
            return animal.type.foodType;
        }
    }
}
