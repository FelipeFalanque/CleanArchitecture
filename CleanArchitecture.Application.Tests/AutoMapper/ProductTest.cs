using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.AutoMapper
{
    public class ProductTest : BaseTest
    {
        // You can Map The Models
        // É Possível Mapear os Modelos
        [Fact(DisplayName = "You can Map The Models")]
        public void You_Can_Map_The_Models()
        {
            Product entity = new Product(1, "Product 1", "Product 1", 19.59m, 55, string.Empty, new Domain.Entities.Category(1, "Category 1"));

            //Entity To Dto
            var userDto = Mapper.Map<ProductDTO>(entity);
            Assert.Equal(userDto.Id, entity.Id);
            Assert.Equal(userDto.Name, entity.Name);
            Assert.Equal(userDto.Description, entity.Description);
            Assert.Equal(userDto.Stock, entity.Stock);
            Assert.Equal(userDto.Image, entity.Image);

        }
    }
}
