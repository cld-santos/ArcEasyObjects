using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityFieldAttribute : Attribute, IEntityField
    {
        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }

        public EntityFieldAttribute(string FieldName, Type FieldType)
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
            if (ChooseLoadMethod == BaseModel.LoadMethod.Lazy) return;
            this.Load(Workspace, Row, BaseModel, Property);
        }
    }
}
