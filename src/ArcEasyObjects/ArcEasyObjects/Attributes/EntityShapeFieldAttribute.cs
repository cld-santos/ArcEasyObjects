using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityShapeFieldAttribute : EntityFieldAttribute
    {
        public EntityShapeFieldAttribute(Type Type):base("",Type) { }
    }
}
