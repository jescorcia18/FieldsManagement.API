using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Domain.Models
{
    public class FieldRequest
    {
        public string Name { get; set; } = string.Empty; 
        public string Type { get; set; } = string.Empty;
        public bool IsRequired { get; set; } 
        public string Validation { get; set; } = string.Empty;
    }
}
