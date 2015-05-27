using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcEasyObjects.Attributes
{
    public class FeatureClassKeyFieldAEOAttribute : FeatureClassFieldAEOAttribute
    {
        public FeatureClassKeyFieldAEOAttribute(string FieldName, Type FieldType) : base(FieldName, FieldType) { }
    }
}


