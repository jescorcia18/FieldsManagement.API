
using Insttantt.FieldsManagement.Application.Common.Interfaces.Services;
using Insttantt.FieldsManagement.Application.Middleware;
using Insttantt.FieldsManagement.Application.Services;
using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Insttantt.FieldsManagement.Api.Controllers
{
    /// <summary>
    /// Fields Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        #region Global Variables
        private readonly IFieldService _fieldService;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        public FieldsController(IFieldService fieldService, ILogger<ExceptionHandler> logger)
        {
            _fieldService = fieldService;
            _logger = logger;
        }

        /// <summary>
        /// Get Fields
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldResponse>>> GetFields()
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : FieldsController.GetFieldsAll");
                var fields = await _fieldService.GetAllFieldsAsync();
                _logger.LogInformation($"Finish Endpoint : FieldsController.GetFieldsAll");
                return Ok(fields);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error FieldsController.GetFieldsAll: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Field
        /// </summary>
        /// <param name="id">Id of Field</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldResponse>> GetField(int id)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : FieldsController.GetFieldsById");
                var field = await _fieldService.GetFieldByIdAsync(id);
                if (field == null)
                {
                    return NotFound("Field not found");
                }
                _logger.LogInformation($"Finish Endpoint : FieldsController.GetFieldsById");
                return Ok(field);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error FieldsController.GetFieldsById: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Field
        /// </summary>
        /// <param name="field">Field properties add </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FieldResponse>> AddField(FieldRequest field)
        {
            try
            {
                _logger.LogInformation($"Finish Start : FieldsController.AddField");
                var newField = await _fieldService.AddFieldAsync(field);
                _logger.LogInformation($"Finish Endpoint : FieldsController.AddField");
                return CreatedAtAction(nameof(GetField), new { id = newField.FieldId }, newField);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error FieldsController.AddField: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Field
        /// </summary>
        /// <param name="id">Update field id</param>
        /// <param name="field">Field properties update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateField(int id, FieldRequest field)
        {
            try
            {
                _logger.LogInformation($"Finish Start : FieldsController.UpdateField");
                var fieldExist = await _fieldService.GetFieldByIdAsync(id);
                if (id != fieldExist.FieldId)
                    return BadRequest();

                await _fieldService.UpdateFieldAsync(id, field);
                _logger.LogInformation($"Finish Endpoint : FieldsController.UpdateField");
                return Ok("Update Field Sucessull!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error FieldsController.UpdateField: { ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Field
        /// </summary>
        /// <param name="id">Update field id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            try
            {
                _logger.LogInformation($"Start Endpoint : FieldsController.DeleteField");
                await _fieldService.DeleteFieldAsync(id);
                _logger.LogInformation($"Finish Endpoint : FieldsController.DeleteField");
                return Ok("Delete Field Successull!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error FieldsController.DeleteField: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
