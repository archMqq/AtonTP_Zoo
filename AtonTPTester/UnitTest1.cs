using AtonTP;
namespace AtonTPTester
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void readFoodPriceWhithTrue()
        {
            // arrange
            var reader = new FilesReader();
            var foodFilePath = "test_food.txt";
            File.WriteAllLines(foodFilePath, new[] { "meat = 10.5", "fruit = 5.3" });

            // act
            var food = reader.readFoodFile(foodFilePath);

            // assert
            Assert.That(food.meatPrice, Is.EqualTo(10.5).Within(0.0001));
            Assert.That(food.fruitPrice, Is.EqualTo(5.3).Within(0.0001));

            //cleaning file
            File.Delete(foodFilePath);
        }

        [Test]
        public void calculateZooCostWithMeatAnimalsTrue()
        {
            // arrange
            var food = new Food { meatPrice = 10.0, fruitPrice = 5.0 };
            var animalType = new AnimalType("Lion", 1.2, FoodTypes.meat, null);
            var animals = new List<Animal>
            {
                new Animal("Lion1", animalType, 100.0),
                new Animal("Lion2", animalType, 150.0)
            };
            var zoo = new Zoo(food, animals);
            var calculator = new Calculator();

            // act
            calculator.calculateCost(zoo);

            // assert
            Assert.That(zoo.costOfAnimalsKeeping, Is.EqualTo(3000.0).Within(0.0001));
        }

        [Test]
        public void calculateZooCostWithFruitAnimalsTrue()
        {
            // arrange
            var food = new Food { meatPrice = 10.0, fruitPrice = 5.0 };
            var animalType = new AnimalType("Monkey", 1.1, FoodTypes.fruit, null);
            var animals = new List<Animal>
            {
                new Animal("Monkey1", animalType, 50.0),
                new Animal("Monkey2", animalType, 60.0)
            };
            var zoo = new Zoo(food, animals);
            var calculator = new Calculator();

            // act
            calculator.calculateCost(zoo);

            // assert
            Assert.That(zoo.costOfAnimalsKeeping, Is.EqualTo(605.0).Within(0.0001));
        }
    }
}