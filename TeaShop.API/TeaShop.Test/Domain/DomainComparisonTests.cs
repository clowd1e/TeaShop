using FluentAssertions;
using TeaShop.Application.Comparison;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Exceptions;

namespace TeaShop.Test.Domain
{
    public sealed class DomainComparisonTests
    {
        public DomainComparisonTests()
        {
            #region Set comparison delegates
            Tea.ComparisonDelegate = ComparisonExtensions.TeaDefaultComparison;
            TeaType.ComparisonDelegate = ComparisonExtensions.TeaTypeDefaultComparison;
            Customer.ComparisonDelegate = ComparisonExtensions.CustomerDefaultComparison;
            Order.ComparisonDelegate = ComparisonExtensions.OrderDefaultComparison;
            #endregion
        }

        [Theory]
        [InlineData("John", "Doe", "John", "Doe", "0987654321", "09325654321")]
        [InlineData("Jane", "Doe", "Jane", "Doe", "0987657821", "03454671")]
        [InlineData("Doe", "John", "Doe", "John", "0987657821", "03454671")]
        [InlineData("", "", "", "", "", "")]
        public void Customers_ShouldReturnTrue_WhenEqual(string firstName1, string lastName1, string firstName2, string lastName2, string phone1, string phone2)
        {
            // Arrange
            var Customer1 = new Customer() { FirstName = firstName1, LastName = lastName1, Phone = phone1 };
            var Customer2 = new Customer() { FirstName = firstName2, LastName = lastName2, Phone = phone2 };

            // Act
            bool result = Customer1 == Customer2;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("John", "Doe", "Jane", "Doe", "0987654321", "09325654321")]
        [InlineData("John", "Doe", "John", "Smith", "0987657821", "03454671")]
        [InlineData("Jane", "Doe", "John", "Doe", "0987657821", "03454671")]
        [InlineData("Jane", "Doe", "Jan", "Doe", "0987657821", "03454671")]
        public void Customers_ShouldReturnFalse_WhenNotEqual(string firstName1, string lastName1, string firstName2, string lastName2, string phone1, string phone2)
        {
            // Arrange
            var Customer1 = new Customer() { FirstName = firstName1, LastName = lastName1, Phone = phone1 };
            var Customer2 = new Customer() { FirstName = firstName2, LastName = lastName2, Phone = phone2 };

            // Act
            bool result = Customer1 == Customer2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Customers_ShouldReturnFalse_WhenNull()
        {
            // Arrange
            Customer Customer1 = null;
            Customer Customer2 = null;

            // Act
            bool result = Customer1 == Customer2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Customers_ShouldThrowException_WhenComparisonDelegateNotSet()
        {
            // Arrange
            Customer.ComparisonDelegate = null;
            var Customer1 = new Customer() { FirstName = "John", LastName = "Doe", Phone = "0987654321" };
            var Customer2 = new Customer() { FirstName = "John", LastName = "Doe", Phone = "0987654321" };

            // Act
            Action act = () => { bool result = Customer1 == Customer2; };

            // Assert
            act.Should().Throw<ComparisonException>();
        }

        [Theory]
        [InlineData("GrEen Tea", "Green Tea", "", "")]
        [InlineData("Black Tea", "Black tea", "", "")]
        [InlineData("Green Tea", "Green Tea",
            "Green tea is a type of tea that.",
            "Green tea is a type.")]
        [InlineData("BLACK TEA", "black tea", "", "")]
        public void TeaTypes_ShouldReturnTrue_WhenEqual(string name1, string name2, string desc1, string desc2)
        {
            // Arrange
            var TeaType1 = new TeaType() { Name = name1, Description = desc1 };
            var TeaType2 = new TeaType() { Name = name2, Description = desc2 };

            // Act
            bool result = TeaType1 == TeaType2;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Green Tea", "Black Tea", "", "")]
        [InlineData("Gren Tea", "Green Tea", "", "Green tea is a type.")]
        [InlineData("Black Tea", "Tea", "Black tea is a type.", "")]
        [InlineData("White", "White Tea", "Basically tea.", "Basically tea.")]

        public void TeaTypes_ShouldReturnFalse_WhenNotEqual(string name1, string name2, string desc1, string desc2)
        {
            // Arrange
            var TeaType1 = new TeaType() { Name = name1, Description = desc1 };
            var TeaType2 = new TeaType() { Name = name2, Description = desc2 };

            // Act
            bool result = TeaType1 == TeaType2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TeaTypes_ShouldReturnFalse_WhenNull()
        {
            // Arrange
            TeaType TeaType1 = null;
            TeaType TeaType2 = null;

            // Act
            bool result = TeaType1 == TeaType2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TeaTypes_ShouldThrowException_WhenComparisonDelegateNotSet()
        {
            // Arrange
            TeaType.ComparisonDelegate = null;
            var TeaType1 = new TeaType() { Name = "Green Tea", Description = "Green tea is a type." };
            var TeaType2 = new TeaType() { Name = "Green Tea", Description = "Green tea is a type." };

            // Act
            Action act = () => { bool result = TeaType1 == TeaType2; };

            // Assert
            act.Should().Throw<ComparisonException>();
        }

        [Theory]
        [InlineData(" fiRst Tea", "first teA", "desc1", "", "black Tea", "Black tea")]
        [InlineData("   Second Tea", "Second Tea", "desc1", "desc2", "Green Tea", "Green Tea")]
        [InlineData("ThIrd tea", "ThiRd tea   ", "", "desc2", "white Tea", "WhITE TeA")]
        [InlineData("Fourth Tea", "Fourth Tea", "desc1", "desc2", "Oolong Tea", "Oolong Tea")]
        public void Tea_ShouldReturnTrue_WhenEqual(string teaName1, string teaName2, string desc1, string desc2, string teaTypeName1, string teaTypeName2)
        {
            // Arrange
            var TeaType1 = new TeaType() { Name = teaTypeName1 };
            var TeaType2 = new TeaType() { Name = teaTypeName2 };
            var Tea1 = new Tea() { Name = teaName1, Description = desc1, Type = TeaType1 };
            var Tea2 = new Tea() { Name = teaName2, Description = desc2, Type = TeaType2 };

            // Act
            bool result = Tea1 == Tea2;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("First Tea", "Second Tea", "desc1", "", "black Tea", "Black tea")]
        [InlineData("First Tea", "First Tea", "desc1", "desc2", "black Tea", "Green Tea")]
        [InlineData("  Third Tea   ", "Third Tea   ", "", "desc2", "  whiteeee Tea", "WhITE TeA")]
        [InlineData("  FourtH Tea", "Fourth Tea", "+", "", "Oolong Tea", "Oolong333 Tea")]
        public void Tea_ShouldReturnFalse_WhenNotEqual(string teaName1, string teaName2, string desc1, string desc2, string teaTypeName1, string teaTypeName2)
        {
            // Arrange
            var TeaType1 = new TeaType() { Name = teaTypeName1 };
            var TeaType2 = new TeaType() { Name = teaTypeName2 };
            var Tea1 = new Tea() { Name = teaName1, Description = desc1, Type = TeaType1 };
            var Tea2 = new Tea() { Name = teaName2, Description = desc2, Type = TeaType2 };

            // Act
            bool result = Tea1 == Tea2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Tea_ShouldReturnFalse_WhenNull()
        {
            // Arrange
            Tea Tea1 = null;
            Tea Tea2 = null;

            // Act
            bool result = Tea1 == Tea2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Tea_ShouldThrowException_WhenComparisonDelegateNotSet()
        {
            // Arrange
            Tea.ComparisonDelegate = null;
            var TeaType1 = new TeaType() { Name = "Green Tea" };
            var TeaType2 = new TeaType() { Name = "Green Tea" };
            var Tea1 = new Tea() { Name = "Green Tea", Description = "Green tea is a type.", Type = TeaType1 };
            var Tea2 = new Tea() { Name = "Green Tea", Description = "Green tea is a type.", Type = TeaType2 };

            // Act
            Action act = () => { bool result = Tea1 == Tea2; };

            // Assert
            act.Should().Throw<ComparisonException>();
        }
    }
}
