using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityDateTimeFieldAttribute : Attribute,IEntityField
    {
        public EntityDateTimeFieldAttribute(string FieldName) : this(FieldName, typeof(DateTime)) { }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }

        public EntityDateTimeFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = typeof(DateTime);
        }

        public virtual void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {

            if (!String.IsNullOrEmpty(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString()))
            {
                Property.Property.SetValue(BaseModel,
                                                    Convert.ChangeType(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)),
                                                                    Property.Attribute.FieldType), null);
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
            if (Property.Property.GetValue(BaseModel, null) != null)
            {
                Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType));
            }
            else
            {
                Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), DBNull.Value);
            }
        }

        public void Save(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            if (Property.Property.GetValue(BaseModel, null) != null)
            {
                Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName),
                   Convert.ChangeType(Property.Property.GetValue(BaseModel, null),
                                      Property.Attribute.FieldType));
            }
            else
            {
                Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName), DBNull.Value);
            }
        }

        public string Save(IWorkspace Workspace, BaseModel BaseModel, ModelProperty Property)
        {
            string _resultado = "''";
            if (Property.Property.GetValue(BaseModel, null) != null)
            {
                DateTime _date = Convert.ToDateTime(Property.Property.GetValue(BaseModel, null));
                string _DateValue = "";
                if (_date.Year > 1)
                {
                    _DateValue = _date.ToShortDateString() + " " + _date.ToLongTimeString();
                }
                _resultado = "to_date('" + FieldFormatHelper.FormatField(_DateValue, Property.Attribute.FieldType) + "','DD/MM/YYYY HH24:MI:SS')";
            }
            return _resultado;

        }

        private string _fieldName;
        private Type _fieldType;
    }
}