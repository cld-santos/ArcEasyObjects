using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityManyToManyFieldAttribute : Attribute, IEntityField
    {
        public EntityManyToManyFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }
        public EntityManyToManyFieldAttribute(Type FieldModelType, string RelateFieldName)
            : this("", null)
        {
            _fieldModelType = FieldModelType;
            _relateFieldName = RelateFieldName;
        }

        public string RelateFieldName
        {
            get { return _relateFieldName; }
            set { _relateFieldName = value; }
        }

        public Type FieldModelType
        {
            get { return _fieldModelType; }
            set { _fieldModelType = value; }
        }

        private Type _fieldModelType;
        private string _relateFieldName;


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

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IRow Row, BaseModel AEOModel, ModelProperty Property)
        {
            throw new NotImplementedException();
        }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace _workspace, ESRI.ArcGIS.Geodatabase.IRow _row, BaseModel BaseModel, ModelProperty _property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            throw new NotImplementedException();
        }
    }
}
