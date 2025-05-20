using Assignment2.App.BusinessLayer.Models;

namespace Assignment2.Tests
{
    public class AnimalTests
    {
        [Theory]
        [InlineData("Bob", "Dog", "Male", 1, true)]
        [InlineData(null, "Dog", "Male", 1, false)]
        [InlineData("Bob", null, "Male", 1, false)]
        [InlineData("Bob", "Dog", null, 1, false)]
        [InlineData("Bob", "Dog", "Male", 0, false)]
        public void CheckIfValidChecksProperties(string? name, string? type, string? sex, int owner, bool isValid)
        {
            var animal = new Animal { Name = name, Type = type, Sex = sex, OwnerId = owner };
            var result = animal.CheckIfValid();
            Assert.Equal(isValid, result);
        }

        [Fact]
        public void ToStringContainsCorrectDetails()
        {
            var animal = new Animal { Name = "Bobby", Type = "Cat" };
            var textValue = animal.ToString();
            Assert.Equal("Bobby [Cat]", textValue);
        }

        [Fact]
        // This test checks that a properly formatted CSV line creates a valid Animal object.
        // It is important because it verifies that CSV deserialization maps all fields correctly.
        public void FromCsv_ValidInput_ParsesCorrectly()
        {
            string csv = "1,Buddy,Dog,Labrador,Male,100";
            var animal = Animal.FromCsv(csv);

            Assert.Equal(1, animal.Id);
            Assert.Equal("Buddy", animal.Name);
            Assert.Equal("Dog", animal.Type);
            Assert.Equal("Labrador", animal.Breed);
            Assert.Equal("Male", animal.Sex);
            Assert.Equal(100, animal.OwnerId);
        }

        [Fact]
        // This test checks that the method throws an exception if the input CSV line is incomplete.
        // This is important to catch data corruption early and prevent silent failures.
        public void FromCsv_MissingFields_ThrowsException()
        {
            string badCsv = "2,Buddy,Dog";
            Assert.Throws<IndexOutOfRangeException>(() => Animal.FromCsv(badCsv));
        }

        [Fact]
        // This test verifies that empty optional fields (like Breed) do not cause an error.
        // This is important because real-world data might not always include optional values.
        public void FromCsv_EmptyFields_ParsesPartially()
        {
            string csv = "3,Buddy,Dog,,Male,101";
            var animal = Animal.FromCsv(csv);

            Assert.Equal("", animal.Breed);
            Assert.Equal("Buddy", animal.Name);
            Assert.Equal("Male", animal.Sex);
        }
    }
}
