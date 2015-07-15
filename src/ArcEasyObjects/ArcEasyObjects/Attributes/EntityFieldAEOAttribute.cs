using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityFieldAEOAttribute : Attribute
    {
        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }

        public EntityFieldAEOAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }

        private string _fieldName;
        private Type _fieldType;

    }
}
