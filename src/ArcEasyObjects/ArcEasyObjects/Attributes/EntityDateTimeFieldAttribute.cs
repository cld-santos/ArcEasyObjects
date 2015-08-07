using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityDateTimeFieldAttribute : EntityFieldAttribute
    {
        public EntityDateTimeFieldAttribute(string FieldName) : base(FieldName, typeof(DateTime)) { }
    }
}