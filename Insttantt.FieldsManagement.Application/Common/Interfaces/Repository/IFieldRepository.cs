
using Insttantt.FieldsManagement.Domain.Entities;

namespace Insttantt.FieldsManagement.Application.Common.Interfaces.Repository
{
    public interface IFieldRepository
    {
        Task<IEnumerable<Field>> GetAllFieldsAsync();
        Task<Field> GetFieldByIdAsync(int id);
        Task<Field> AddFieldAsync(Field field);
        Task UpdateFieldAsync(Field field);
        Task DeleteFieldAsync(int id);
    }
}
