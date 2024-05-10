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
    public class AddFieldsControllerTest
    {
        private readonly Mock<ILogger<ExceptionHandler>> _mocklogger;
        private readonly Mock<IFieldService> _mockFieldService;

        public AddFieldsControllerTest()
        {
            _mocklogger = new Mock<ILogger<ExceptionHandler>>();
            _mockFieldService = new Mock<IFieldService>();
        }

        [Fact]
        public async Task AddField_ReturnsCreatedAtAction_WithNewField()
        {
            // Arrange
            var fieldRequestMock = new FieldRequest
            {
                Name = "FieldName",
                Type = "String",
                IsRequired = true,
                Validation = "SomeValidationRule"
            };

            var fieldResponseMock = new FieldResponse
            {
                FieldId = 1,
                FieldName = "FieldName",
                FieldType = "String",
                FieldRequired = true,
                FieldValidation = "SomeValidationRule"
            };
            var controller = new FieldsController(_mockFieldService.Object, _mocklogger.Object);
            _mockFieldService.Setup(service => service.AddFieldAsync(fieldRequestMock))
                            .ReturnsAsync(fieldResponseMock);

            // Act
            var result = await controller.AddField(fieldRequestMock);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(FieldsController.GetField), createdAtActionResult.ActionName);
            Assert.Equal(fieldResponseMock.FieldId, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(fieldResponseMock, createdAtActionResult.Value);
        }

        [Fact]
        public async Task AddField_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            _mockFieldService.Setup(service => service.AddFieldAsync(It.IsAny<FieldRequest>()))
                            .ThrowsAsync(new Exception("Test exception"));

            var controller = new FieldsController(_mockFieldService.Object, _mocklogger.Object);
            var fieldRequest = new FieldRequest { };

            // Act
            var result = await controller.AddField(fieldRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("Test exception", errorMessage);
        }
    }
}
