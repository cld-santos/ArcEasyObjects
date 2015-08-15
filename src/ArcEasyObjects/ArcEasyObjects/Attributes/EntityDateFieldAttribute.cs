using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityDateFieldAttribute : Attribute, IEntityField
    {
        public EntityDateFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }
        public EntityDateFieldAttribute(string FieldName) : this(FieldName, typeof(DateTime)) { }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IRow Row, BaseModel AEOModel, ModelProperty Property)
        {
            if (!String.IsNullOrEmpty(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString()))
            {
                Property.Property.SetValue(AEOModel,
                                            Convert.ChangeType(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)),
                                                               Property.Attribute.FieldType), null);
            }

        }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace _workspace, ESRI.ArcGIS.Geodatabase.IRow _row, BaseModel BaseModel, ModelProperty _property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(_workspace, _row, BaseModel, _property);

        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }



        private string _fieldName;
        private Type _fieldType;


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
            string _DateValue = Convert.ToDateTime(Property.Property.GetValue(BaseModel, null)).ToShortDateString();
            return "'" + FieldFormatHelper.FormatField(_DateValue, Property.Attribute.FieldType) + "'";
        }
    }
}