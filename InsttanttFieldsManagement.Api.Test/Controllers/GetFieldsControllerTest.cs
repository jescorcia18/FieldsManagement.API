using Insttantt.FieldsManagement.Api.Controllers;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Services;
using Insttantt.FieldsManagement.Application.Middleware;
using Insttantt.FieldsManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsttanttFieldsManagement.Api.Test.Controllers
{
    public class GetFieldsControllerTest
    {
        private readonly  Mock<ILogger<ExceptionHandler>> _mocklogger;
        private readonly Mock<IFieldService> _mockFieldService;

        public GetFieldsControllerTest()
        {
            _mocklogger = new Mock<ILogger<ExceptionHandler>>();
            _mockFieldService = new Mock<IFieldService>();
        }
        [Fact]
        public async Task GetFields_ReturnsOkResult_WithListOfFields()
        {
            // Arrange
            _mockFieldService.Setup(service => service.GetAllFieldsAsync())
                            .ReturnsAsync(new List<FieldResponse> { new FieldResponse {} });

            var controller = new FieldsController(_mockFieldService.Object, _mocklogger.Object);

            // Act
            var result = await controller.GetFields();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var fields = Assert.IsAssignableFrom<IEnumerable<FieldResponse>>(okResult.Value);
        }

        [Fact]
        public async Task GetFields_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            _mockFieldService.Setup(service => service.GetAllFieldsAsync())
                            .ThrowsAsync(new Exception("Test exception"));

            var controller = new FieldsController(_mockFieldService.Object, _mocklogger.Object);

            // Act
            var result = await controller.GetFields();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("Test exception", errorMessage);
        }
    }
}
