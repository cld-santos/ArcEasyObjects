using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityOneToOneFieldAttribute : EntityFieldAttribute
    {
        public EntityOneToOneFieldAttribute(string FieldName, Type FieldType) : base(FieldName, FieldType) { }

        public EntityOneToOneFieldAttribute(Type FieldModelType, string FieldName, Type FieldType)
            : base(FieldName, FieldType)
        {
            _fieldModelType = FieldModelType;

        }

        public Type FieldModelType
        {
            get { return _fieldModelType; }
            set { _fieldModelType = value; }
        }

        private Type _fieldModelType;

    }
}
