using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects;
using ArcEasyObjects.Attributes;
using System.Reflection;

namespace ArcEasyObjects
{
    public class EntityAEO
    {

        public Dictionary<string, string> getFeatureClassAttributes()
        {

            return _modelAttributes;

        }

        public HashSet<ModelProperty> getFeatureClassFields()
        {

            return _modelProperties;

        }

        public String getFeatureClassKeyField()
        {

            return _KeyField;

        }

        public  EntityAEO(BaseModel Modelo)
        {
            this._modelo = Modelo;
            loadModelConfig();
        }



        public string getFeatureClassName()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_modelo.GetType());

            foreach (System.Attribute attr in attrs)
            {
                if (attr is EntityAEOAttribute)
                {
                    EntityAEOAttribute a = (EntityAEOAttribute)attr;
                    return a.EntityName;
                }
            }
            return "";
        }

        public EntityAEOAttribute getFeatureClassConfig()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_modelo.GetType());

            foreach (System.Attribute attr in attrs)
            {
                if (attr is EntityAEOAttribute)
                {
                    return (EntityAEOAttribute)attr;
                }
            }
            return null;
        }

        private void loadModelConfig()
        {
            PropertyInfo[] _properties = _modelo.GetType().GetProperties();
            _modelProperties = new HashSet<ModelProperty>();
            _modelAttributes = new Dictionary<string, string>();

            foreach (PropertyInfo _property in _properties)
            {
                EntityFieldAEOAttribute _featureAttribute;
                object[] _attributes = _property.GetCustomAttributes(true);
                object _attribute;

                if (_attributes.Count() > 0)
                {
                    _attribute = _attributes.Single();

                    _featureAttribute = (EntityFieldAEOAttribute)_attribute;
                    _modelProperties.Add(new ModelProperty(_property, _featureAttribute));
                    _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _featureAttribute.FieldName;

                    if (_attribute is EntityKeyFieldAEOAttribute)
                    {
                        _KeyField = ((EntityFieldAEOAttribute)_attribute).FieldName;
                    }
                }
            }
        }


        private BaseModel _modelo;
        private HashSet<ModelProperty> _modelProperties;
        private Dictionary<string, string> _modelAttributes;
        private string _KeyField;


    }
}
