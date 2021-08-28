using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Category
{
    public class CategoryTest : BaseTest
    {
        private ICategoryService _service;
        private Mock<ICategoryService> _serviceMock;

        // É Possivel Executar o Método GetCategories.
        [Fact(DisplayName = "It Is Possible To Execute The GetCategories Method")]
        public async Task It_Is_Possible_To_Execute_The_GetCategories_Method()
        {
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.GetCategories()).ReturnsAsync(new List<CategoryDTO>() { new CategoryDTO() { Id = 1, Name = "Name 1"}, new CategoryDTO() { Id = 2, Name = "Name 2" } });
            _service = _serviceMock.Object;

            var result = await _service.GetCategories();
            Assert.NotNull(result);
            Assert.True(result.Count() == 2);

            var _listResult = new List<CategoryDTO>();
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.GetCategories()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetCategories();
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);

        }
    }
}
