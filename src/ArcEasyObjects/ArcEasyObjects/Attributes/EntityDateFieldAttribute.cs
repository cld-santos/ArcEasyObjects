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
    }
}