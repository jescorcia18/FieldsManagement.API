
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Insttantt.FieldsManagement.Domain.Entities
{
    [Table (name: "Fields")]
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FieldId { get; set; } // Identificador único del campo

        [Column(name: "FieldName")]
        public string FieldName { get; set; } = string.Empty; // Nombre del campo

        [Column(name: "FieldType")]
        public string FieldType { get; set; } = string.Empty;// Tipo de datos del campo (texto, número, fecha, etc.)

        [Column(name: "FieldRequired")]
        public bool FieldRequired { get; set; } // Requerido o no requerido

        [Column(name: "FieldValidation")]
        public string FieldValidation { get; set; } = string.Empty;// Expresión regular de validación del campo
    }
}
