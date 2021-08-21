using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Validation;
using System;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            var exception = Record.Exception(() => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image"));
            Assert.Null(exception);
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image"));
            Assert.Equal("Invalid Id value.", exception.Message);
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image"));
            Assert.Equal("Invalid name, too short, minimum 3 characters", exception.Message);
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image toooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooogggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg"));
            Assert.Equal("Invalid image name, too long, maximum 250 characters", exception.Message);
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            var exception = Record.Exception(() => new Product(1, "Product Name", "Product Description", 9.99m, 99, null));
            Assert.Null(exception);
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            var exception = Record.Exception(() => new Product(1, "Product Name", "Product Description", 9.99m, 99, null));
            Assert.Null(exception);
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            var exception = Record.Exception(() => new Product(1, "Product Name", "Product Description", 9.99m, 99, ""));
            Assert.Null(exception);
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Product(1, "Product Name", "Product Description", -9.99m, 99, ""));
            Assert.Equal("Invalid price value", exception.Message);
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Product(1, "Pro", "Product Description", 9.99m, value, "product image"));
            Assert.Equal("Invalid stock value", exception.Message);
        }
    }
}
