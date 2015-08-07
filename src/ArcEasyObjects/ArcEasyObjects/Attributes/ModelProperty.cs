using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class ModelProperty
    {
        public PropertyInfo Property
        {
            get { return _property; }
        }

        public EntityFieldAttribute Attribute
        {   
            get { return _attribute; }
        }


        public ModelProperty(PropertyInfo PropertyInfo, EntityFieldAttribute Attribute)
        {
            _property  = PropertyInfo;
            _attribute = Attribute;
        }

        private PropertyInfo _property;
        private EntityFieldAttribute _attribute;


    }
}
