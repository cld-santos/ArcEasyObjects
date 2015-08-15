using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityOneToOneFieldAttribute : Attribute, IEntityField
    {
        public EntityOneToOneFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }

        public EntityOneToOneFieldAttribute(Type FieldModelType, string FieldName, Type FieldType)
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
        private string _fieldName;
        private Type _fieldType;


        public void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            object[] _parametros = { (object)Workspace };

            BaseModel otoField = (BaseModel)Activator.CreateInstance(((EntityOneToOneFieldAttribute)Property.Attribute).FieldModelType, _parametros);
            string _KeyObj = Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString();
            Int32 _keyValue = !String.IsNullOrEmpty(_KeyObj) ? Convert.ToInt32(_KeyObj) : 0;
            if (_keyValue > 0)
            {
                otoField.Load(_keyValue);
                Property.Property.SetValue(BaseModel, otoField, null);
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



        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            object[] _parametros = { (object)Workspace };

            BaseModel otoField = (BaseModel)Activator.CreateInstance(((EntityOneToOneFieldAttribute)Property.Attribute).FieldModelType, _parametros);
            string _KeyObj = Feature.get_Value(Feature.Fields.FindField(Property.Attribute.FieldName)).ToString();
            Int32 _keyValue = !String.IsNullOrEmpty(_KeyObj) ? Convert.ToInt32(_KeyObj) : 0;
            if (_keyValue > 0)
            {
                otoField.Load(_keyValue);
                Property.Property.SetValue(BaseModel, otoField, null);
            }
        }

        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            if (ChooseLoadMethod == BaseModel.LoadMethod.Lazy) return;
            this.Load(Workspace, Feature, BaseModel, Property);
        }


        public void Save(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            BaseModel _bm = (BaseModel)Property.Property.GetValue(BaseModel, null);
            if (_bm != null)
            {
                ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), _keyValue);
            }

        }

        public void Save(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            BaseModel _bm = (BaseModel)Property.Property.GetValue(BaseModel, null);
            if (_bm != null)
            {
                ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName), _keyValue);
            }
        }


        public string Save(IWorkspace Workspace, BaseModel BaseModel, ModelProperty Property)
        {
            BaseModel _bm = (BaseModel)Property.Property.GetValue(BaseModel, null);
            if (_bm != null)
            {
                ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                return FieldFormatHelper.FormatField(_keyValue, Property.Attribute.FieldType);
            }
            return FieldFormatHelper.FormatField("", Property.Attribute.FieldType); 
        }




    }
}
