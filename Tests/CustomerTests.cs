using VetClinic.App.BusinessLayer.Models;

namespace Tests
{
    public class CustomerTests
    {
        [Theory]
        [InlineData("John", "Doe", "123-4567", true)]
        [InlineData(null, "Doe", "123-4567", false)]
        [InlineData("John", null, "123-4567", false)]
        [InlineData("John", "Doe", null, false)]
        public void CheckIfValidChecksProperties(string? firstName, string? surname, string? phoneNumber, bool isValid)
        {
            var customer = new Customer { FirstName = firstName, Surname = surname, PhoneNumber = phoneNumber };
            var result = customer.CheckIfValid();
            Assert.Equal(isValid, result);
        }

        [Fact]
        public void ToStringContainsCorrectDetails()
        {
            var customer = new Customer
            {
                FirstName = "John",
                Surname = "Doe",
            };

            var textValue = customer.ToString();
            Assert.Equal("Doe, John", textValue);
        }

        [Fact]
        // This test checks that a valid CSV line produces a correctly populated Customer object.
        // It is important because it confirms that all fields are read and mapped accurately.
        public void FromCsv_ValidInput_ParsesCorrectly()
        {
            string csv = "10,Jane,Doe,0211234567,\"123 Main Street\\nWellington\"";
            var customer = Customer.FromCsv(csv);

            Assert.Equal(10, customer.Id);
            Assert.Equal("Jane", customer.FirstName);
            Assert.Equal("Doe", customer.Surname);
            Assert.Equal("0211234567", customer.PhoneNumber);
            Assert.Contains("123 Main Street", customer.Address);
        }

        [Fact]
        // This test checks that the method throws an exception if the input CSV line is incomplete.
        // It is important to prevent accepting corrupted or partial customer records.
        public void FromCsv_MissingFields_ThrowsException()
        {
            string csv = "11,John";
            Assert.Throws<IndexOutOfRangeException>(() => Customer.FromCsv(csv));
        }

        [Fact]
        // This test verifies that optional fields like PhoneNumber can be missing or empty.
        // It is important because real-world data often lacks complete info and should still be handled.
        public void FromCsv_EmptyOptionalValues_ParsesCorrectly()
        {
            string csv = "12,Mary,Smith,,\"456 Another Rd\\nAuckland\"";
            var customer = Customer.FromCsv(csv);

            Assert.Equal("Mary", customer.FirstName);
            Assert.Equal("Smith", customer.Surname);
            Assert.True(string.IsNullOrEmpty(customer.PhoneNumber)); // Depending on implementation
        }
    }
}
