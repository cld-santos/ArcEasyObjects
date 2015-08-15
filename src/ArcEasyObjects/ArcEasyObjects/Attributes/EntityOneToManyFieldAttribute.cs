using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityOneToManyFieldAttribute : Attribute, IEntityField
    {
        public EntityOneToManyFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }
        public EntityOneToManyFieldAttribute(Type FieldModelType, string FieldName, Type FieldType)
            : this(FieldName, FieldType)
        {
            _fieldModelType = FieldModelType;

        }

        public Type FieldModelType
        {
            get { return _fieldModelType; }
            set { _fieldModelType = value; }
        }

        private Type _fieldModelType;


        public void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            object[] _parametros = { (object)Workspace };
            EntityOneToManyFieldAttribute _attribute = (EntityOneToManyFieldAttribute)Property.Attribute;
            BaseModel otmField = (BaseModel)Activator.CreateInstance((_attribute).FieldModelType, _parametros);
            string _KeyObj = Row.get_Value(Row.Fields.FindField(_attribute.FieldName)).ToString();
            Int32 _keyValue = !String.IsNullOrEmpty(_KeyObj) ? Convert.ToInt32(_KeyObj) : 0;
            if (_keyValue > 0)
            {
                var _source = otmField.Search(_attribute.FieldName + "=" + _keyValue, BaseModel.LoadMethod.Lazy);
                IList _target = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(_attribute.FieldModelType));
                foreach (var _item in _source)
                {
                    _target.Add(_item);
                }

                Property.Property.SetValue(BaseModel, _target, null);
            }
        }

        public void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            if (ChooseLoadMethod == BaseModel.LoadMethod.Lazy) return;
            this.Load(Workspace, Row, BaseModel, Property);
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
