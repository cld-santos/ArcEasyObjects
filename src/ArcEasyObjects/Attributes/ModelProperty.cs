﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcEasyObjects.Attributes
{
    public class ModelProperty
    {
        public PropertyInfo Property
        {
            get { return _property; }
        }

        public FeatureClassFieldsAEOAttribute Attribute
        {
            get { return _attribute; }
        }


        public ModelProperty(PropertyInfo PropertyInfo, FeatureClassFieldsAEOAttribute Attribute)
        {
            _property  = PropertyInfo;
            _attribute = Attribute;
        }

        private PropertyInfo _property;
        private FeatureClassFieldsAEOAttribute _attribute;


    }
}