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

            return ((EntityAEOAttribute)attrs.Where(x => x is EntityAEOAttribute).Single()).EntityName;
        }

        public EntityAEOAttribute getFeatureClassConfig()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_modelo.GetType());

            return (EntityAEOAttribute)attrs.Where(x => x is EntityAEOAttribute).Single();
        }

        private void loadModelConfig()
        {
            if (_modelProperties != null && _modelAttributes != null) return;

            PropertyInfo[] _properties = _modelo.GetType().GetProperties();
            _modelProperties = new HashSet<ModelProperty>();
            _modelAttributes = new Dictionary<string, string>();
            System.Console.WriteLine("carregou o modelo");
            foreach (PropertyInfo _property in _properties)
            {
                EntityFieldAEOAttribute _featureAttribute;
                var _attributes = _property.GetCustomAttributes(true);
                try
                {
                    var _attribute = _attributes.Where(x =>
                        x.GetType().IsSubclassOf(typeof(EntityFieldAEOAttribute)) ||
                        x is EntityFieldAEOAttribute
                        ).First();

                    if (_attribute is EntityKeyFieldAEOAttribute)
                    {
                        _featureAttribute = (EntityKeyFieldAEOAttribute)_attribute;
                        _modelProperties.Add(new ModelProperty(_property, (EntityKeyFieldAEOAttribute)_featureAttribute));
                        _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _featureAttribute.FieldName;
                        _KeyField = ((EntityFieldAEOAttribute)_attribute).FieldName;
                    }
                    else
                    {
                        _featureAttribute = (EntityFieldAEOAttribute)_attribute;
                        _modelProperties.Add(new ModelProperty(_property, _featureAttribute));
                        _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _featureAttribute.FieldName;

                    }
                }
                catch (System.InvalidOperationException)
                {
                    continue;
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }


        private BaseModel _modelo;
        private HashSet<ModelProperty> _modelProperties;
        private Dictionary<string, string> _modelAttributes;
        private string _KeyField;


    }
}
