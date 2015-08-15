using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityKeyFieldAttribute : EntityFieldAttribute
    {
        public EntityKeyFieldAttribute(string FieldName, Type FieldType) : base(FieldName, FieldType) { }
        public EntityKeyFieldAttribute(string FieldName, Type FieldType, String Sequence)
            : base(FieldName, FieldType)
        {
            _Sequence = Sequence;
        }


        public string Sequence
        {
            get { return _Sequence; }
            set { _Sequence = value; }
        }

        private string _Sequence;

    }
}