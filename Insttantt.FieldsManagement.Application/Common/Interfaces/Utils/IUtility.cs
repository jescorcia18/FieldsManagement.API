using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Application.Common.Interfaces.Utils
{
    public interface IUtility
    {
        Task<string> Decrypt(string key, string cipherText);
        Task<string> Encrypt(string key, string data);
        Task<IEnumerable<FieldResponse>> MapToFieldResponse(IEnumerable<Field> fields);
        Task<FieldResponse> MapToFieldResponse(Field fields);
    }
}
