using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Validation;
using System;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            var exception = Record.Exception(() => new Category(1, "Category Name "));
            Assert.Null(exception);
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(-1, "Category Name "));
            Assert.Equal("Invalid Id value.", exception.Message);
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(1, "Ca"));
            Assert.Equal("Invalid name, too short, minimum 3 characters", exception.Message);
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(1, ""));
            Assert.Equal("Invalid name.Name is required", exception.Message);
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Assert.Throws<DomainExceptionValidation>(() => new Category(1, null));
        }
    }
}
