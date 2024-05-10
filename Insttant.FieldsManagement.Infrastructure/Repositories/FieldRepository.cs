using Insttantt.FieldsManagement.Application.Common.Interfaces.Repository;
using Insttantt.FieldsManagement.Application.Middleware;
using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Infrastructure.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        #region Global Variables
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExceptionHandler> _logger;
        #endregion

        #region Constructor
        public FieldRepository(ApplicationDbContext context, ILogger<ExceptionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Field>> GetAllFieldsAsync()
        {
            try
            {
                _logger.LogInformation($"Start method in GetAllFieldsAsync repository");
                return await _context.Fields.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Field Repository (GetAll) error: {ex.Message}");
                throw;
            }

        }

        public async Task<Field> GetFieldByIdAsync(int id)
        {
            try
            {
                var result = await _context.Fields.FindAsync(id);
                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Field Repository (GetById) error: {ex.Message}");
                throw;
            }

        }

        public async Task<Field> AddFieldAsync(Field field)
        {
            try
            {
                _context.Fields.Add(field);
                await _context.SaveChangesAsync();
                return field;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Field Repository (Add) error: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateFieldAsync(Field field)
        {
            try
            {
                var existingEntity = await _context.Fields.FindAsync(field.FieldId);

                if (existingEntity != null)
                {
                    existingEntity.FieldName = field.FieldName;
                    existingEntity.FieldType = field.FieldType;
                    existingEntity.FieldRequired = field.FieldRequired;
                    existingEntity.FieldValidation = field.FieldValidation;
                    _context.Entry(existingEntity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Field Repository (Update) error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteFieldAsync(int id)
        {
            try
            {
                var fieldToDelete = await _context.Fields.FindAsync(id);
                _context.Fields.Remove(fieldToDelete!);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Field Repository (Delete) error: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
