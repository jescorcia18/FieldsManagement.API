using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Application.Common.Interfaces.Services
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldResponse>> GetAllFieldsAsync();
        Task<FieldResponse> GetFieldByIdAsync(int id);
        Task<FieldResponse> AddFieldAsync(FieldRequest field);
        Task UpdateFieldAsync(int id, FieldRequest field);
        Task DeleteFieldAsync(int id);
    }
}
