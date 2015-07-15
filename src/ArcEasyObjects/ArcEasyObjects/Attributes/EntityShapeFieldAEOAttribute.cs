using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityShapeFieldAEOAttribute : EntityFieldAEOAttribute
    {
        public EntityShapeFieldAEOAttribute(Type Type):base("",Type)
        {

        }
    }
}
