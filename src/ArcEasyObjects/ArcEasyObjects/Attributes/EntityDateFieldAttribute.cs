using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityDateFieldAttribute : EntityFieldAttribute
    {
        public EntityDateFieldAttribute(string FieldName) : base(FieldName, typeof(DateTime)) { }
    }
}