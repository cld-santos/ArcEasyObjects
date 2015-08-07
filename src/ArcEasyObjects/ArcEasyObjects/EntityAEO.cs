﻿using System;
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

            loadModels.Add(typeof(EntityKeyFieldAttribute), loadEntityKeyField);
            loadModels.Add(typeof(EntityFieldAttribute), loadEntityField);
            loadModels.Add(typeof(EntityDateFieldAttribute), loadEntityField);
            loadModels.Add(typeof(EntityDateTimeFieldAttribute), loadEntityField);
            loadModels.Add(typeof(EntityShapeFieldAttribute), loadEntityShapeField);
            loadModels.Add(typeof(EntityOneToOneFieldAttribute), loadEntityOneToOneField);

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
                foreach (var _attribute in _attributes.Where(x => x.GetType().IsSubclassOf(typeof(EntityFieldAttribute)) || x is EntityFieldAttribute))
                {
                    loadModels[_attribute.GetType()]((EntityFieldAttribute)_attribute, _property);
                }
            }
        }

        private void loadEntityKeyField(EntityFieldAttribute _attribute, PropertyInfo _property)
        {
            EntityKeyFieldAttribute _featureAttribute = (EntityKeyFieldAttribute)_attribute;
            _modelProperties.Add(new ModelProperty(_property, (EntityKeyFieldAttribute)_featureAttribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _featureAttribute.FieldName;
            _KeyField = ((EntityFieldAttribute)_attribute).FieldName;
        }

        private void loadEntityField(EntityFieldAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, _attribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _attribute.FieldName;

        }

        private void loadEntityShapeField(EntityFieldAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, _attribute));
            _modelAttributes[_modelo.GetType().Name + ".Shape"] = "Geometry";

        }

        private void loadEntityOneToOneField(EntityFieldAttribute _attribute, PropertyInfo _property)
        {
            _modelProperties.Add(new ModelProperty(_property, (EntityOneToOneFieldAttribute)_attribute));
            _modelAttributes[_modelo.GetType().Name + "." + _property.Name] = _attribute.FieldName;
        }
        private Dictionary<Type, Action<EntityFieldAttribute, PropertyInfo>> loadModels = new Dictionary<Type, Action<EntityFieldAttribute, PropertyInfo>>();

        private BaseModel _modelo;
        private HashSet<ModelProperty> _modelProperties;
        private Dictionary<string, string> _modelAttributes;
        private string _KeyField;


    }
}
