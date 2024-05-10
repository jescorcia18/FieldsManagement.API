using Insttantt.FieldsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Domain.Build
{
    public class FieldBuilder
    {
        private readonly Field _field;

        public FieldBuilder()
        {
            _field = new Field();
        }

        public FieldBuilder WithId(int id)
        {
            _field.FieldId = id;
            return this;
        }
        public FieldBuilder WithName(string name)
        {
            _field.FieldName = name;
            return this;
        }

        public FieldBuilder WithType(string type)
        {
            _field.FieldType = type;
            return this;
        }

        public FieldBuilder IsRequired(bool required)
        {
            _field.FieldRequired = required;
            return this;
        }

        public FieldBuilder WithValidation(string validation)
        {
            _field.FieldValidation = validation;
            return this;
        }

        public Field Build()
        {
            return _field;
        }
    }
}
