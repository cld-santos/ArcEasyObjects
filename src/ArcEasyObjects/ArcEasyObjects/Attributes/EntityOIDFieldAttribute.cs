using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityOIDFieldAttribute : Attribute, IEntityField
    {
        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }

        public EntityOIDFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }

        private string _fieldName;
        private Type _fieldType;

        public virtual void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {

            if (Property.Attribute.FieldType == typeof(bool) || Property.Attribute.FieldType == typeof(bool))
            {
                Property.Property.SetValue(BaseModel, "1".Equals(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString()), null);
            }
            else
            {
                if (!String.IsNullOrEmpty(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString()))
                {
                    Property.Property.SetValue(BaseModel,
                                                        Convert.ChangeType(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)),
                                                                        Property.Attribute.FieldType), null);
                }
            }

        }


        public virtual void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Row, BaseModel, Property);
        }


        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            if (!String.IsNullOrEmpty(Feature.get_Value(Feature.Fields.FindField(Property.Attribute.FieldName)).ToString()))
            {
                Property.Property.SetValue(BaseModel,
                                             Convert.ChangeType(Feature.get_Value(Feature.Fields.FindField(Property.Attribute.FieldName)),
                                                                Property.Attribute.FieldType),
                                             null);
            }
        }

        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Feature, BaseModel, Property);
        }


        public void Save(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType));
        }

        public void Save(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName),
               Convert.ChangeType(Property.Property.GetValue(BaseModel, null),
                                  Property.Attribute.FieldType));
        }

        public string Save(IWorkspace Workspace, BaseModel BaseModel, ModelProperty Property)
        {
            return FieldFormatHelper.FormatField(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType);
        }
    }
}
