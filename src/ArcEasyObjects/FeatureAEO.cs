﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects;
using ArcEasyObjects.Attributes;
using System.Reflection;

namespace ArcEasyObjects
{
    public class FeatureAEO
    {
        private Model _modelo;

        public FeatureAEO(Model Modelo)
        {
            this._modelo = Modelo;
        }

        public string obterNomeFeatureClass()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_modelo.GetType());

            foreach (System.Attribute attr in attrs)
            {
                if (attr is FeatureClassAEOAttribute)
                {
                    FeatureClassAEOAttribute a = (FeatureClassAEOAttribute)attr;
                    return a.NomeFeatureClass;
                }
            }
            return "";
        }

        public HashSet<ModelProperty> obterAtributosFeatureClass()
        {
            PropertyInfo[] _properties = _modelo.GetType().GetProperties();
            HashSet<ModelProperty> _modelProperty = new HashSet<ModelProperty>();

            foreach (PropertyInfo _property in _properties)
            {
                object[] attributes = _property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is FeatureClassFieldsAEOAttribute)
                    {
                        _modelProperty.Add(new ModelProperty(_property, (FeatureClassFieldsAEOAttribute)attribute));
                    }

                }
            }
            return _modelProperty;

        }
    }
}
