using Insttantt.FieldsManagement.Application.Common.Interfaces.Repository;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Services;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Utils;
using Insttantt.FieldsManagement.Domain.Build;
using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Application.Services
{
    public class FieldService : IFieldService
    {
        #region Global Variables
        private readonly IFieldRepository _fieldRepository;
        private readonly IUtility _utility;
        #endregion

        #region Constructor
        public FieldService(IFieldRepository fieldRepository, IUtility utility)
        {
            _fieldRepository = fieldRepository;
            _utility = utility;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<FieldResponse>> GetAllFieldsAsync()
        {
            var result = await _fieldRepository.GetAllFieldsAsync();
            return await _utility.MapToFieldResponse(result);
        }
        public async Task<FieldResponse> GetFieldByIdAsync(int id)
        {
            var result = await _fieldRepository.GetFieldByIdAsync(id);
            return await _utility.MapToFieldResponse(result);
        }
        public async Task<FieldResponse> AddFieldAsync(FieldRequest field)
        {
            var entity = await ToFieldBuild(field);
            var result = await _fieldRepository.AddFieldAsync(entity);
            return await _utility.MapToFieldResponse(result);
        }
        public async Task UpdateFieldAsync(int id, FieldRequest field)
        {
            var entity = await ToFieldBuild(id, field);
            await _fieldRepository.UpdateFieldAsync(entity);
        }
        public async Task DeleteFieldAsync(int id)
        {
            await _fieldRepository.DeleteFieldAsync(id);
        }
        #endregion

        #region Private Methods
        private async Task<Field> ToFieldBuild(FieldRequest field)
        {
            return await Task.FromResult(
                new FieldBuilder()
                .WithName(field.Name)
                .WithType(field.Type)
                .IsRequired(field.IsRequired)
                .WithValidation(field.Validation)
                .Build());

        }
        private async Task<Field> ToFieldBuild(int id, FieldRequest field)
        {
            return await Task.FromResult(
                new FieldBuilder()
                .WithId(id)
                .WithName(field.Name)
                .WithType(field.Type)
                .IsRequired(field.IsRequired)
                .WithValidation(field.Validation)
                .Build());

        }
        #endregion
    }
}
