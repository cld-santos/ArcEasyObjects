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
        public EntityAEO(BaseModel Modelo)
        {
            this._modelo = Modelo;

            loadModels.Add(typeof(EntityKeyFieldAEOAttribute), loadEntityKeyField);
            loadModels.Add(typeof(EntityFieldAEOAttribute), loadEntityField);
            loadModels.Add(typeof(EntityShapeFieldAEOAttribute), loadEntityShapeField);
            loadModels.Add(typeof(EntityOneToOneFieldAEOAttribute), loadEntityOneToOneField);

            loadModelConfig();
        }





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

            foreach (PropertyInfo _property in _properties)
            {
                var _attributes = _property.GetCustomAttributes(true);
                foreach (var _attribute in _attributes.Where(x => x.GetType().IsSubclassOf(typeof(EntityFieldAEOAttribute)) || x is EntityFieldAEOAttribute))
                {
                    loadModels[_attribute.GetType()]((EntityFieldAEOAttribute)_attribute, _property);
                }
            }
        }

        private void loadEntityKeyField(EntityFieldAEOAttribute _attribute, PropertyInfo _property)
        {
            EntityKeyFieldAEOAttribute _featureAttribute = (EntityKeyFieldAEOAttribute)_attribute;
            _modelProperties.Add(new ModelProperty(_property, (EntityKeyFieldAEOAttribute)_featureAttribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _featureAttribute.FieldName;
            _KeyField = ((EntityFieldAEOAttribute)_attribute).FieldName;
        }

        private void loadEntityField(EntityFieldAEOAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, _attribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _attribute.FieldName;

        }

        private void loadEntityShapeField(EntityFieldAEOAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, _attribute));
            _modelAttributes[_modelo.GetType().Name + ".Shape"] = "Geometry";

        }

        private void loadEntityOneToOneField(EntityFieldAEOAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, (EntityOneToOneFieldAEOAttribute)_attribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _attribute.FieldName;
        }
        private Dictionary<Type, Action<EntityFieldAEOAttribute, PropertyInfo>> loadModels = new Dictionary<Type, Action<EntityFieldAEOAttribute, PropertyInfo>>();

        private BaseModel _modelo;
        private HashSet<ModelProperty> _modelProperties;
        private Dictionary<string, string> _modelAttributes;
        private string _KeyField;


    }
}
