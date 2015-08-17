using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityManyToManyFieldAttribute : Attribute, IEntityField
    {
        public EntityManyToManyFieldAttribute(Type TargetModelType, Type RelateModelType, string FromFieldName, string ToFieldName)
        {
            _TargetModelType = TargetModelType;
            _RelateModelType = RelateModelType;
            _FromFieldName = FromFieldName;
            _ToFieldName = ToFieldName;
        }

        public string FieldName
        {
            get { return _FromFieldName + "." + _ToFieldName; }
        }
        public Type FieldType
        {
            get { throw new NotImplementedException(); }
        }



        public Type TargetModelType
        {
            get { return _TargetModelType; }
        }

        public Type RelateModelType
        {
            get { return _RelateModelType; }
        }

        public string FromFieldName
        {
            get { return _FromFieldName; }
        }

        public string ToFieldName
        {
            get { return _ToFieldName; }
        }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IRow Row, BaseModel BaseModel, ModelProperty Property)
        {

            object[] _parametros = { (object)Workspace };
            EntityManyToManyFieldAttribute _attribute = (EntityManyToManyFieldAttribute)Property.Attribute;
            BaseModel _mtmField = (BaseModel)Activator.CreateInstance(_attribute.RelateModelType, _parametros);
            
            string _KeyObj = Row.get_Value(Row.Fields.FindField(BaseModel.KeyField)).ToString();
            Int32 _keyValue = !String.IsNullOrEmpty(_KeyObj) ? Convert.ToInt32(_KeyObj) : 0;
            if (_keyValue > 0)
            {
                var _source = _mtmField.Search(_attribute.FromFieldName + "=" + _keyValue, BaseModel.LoadMethod.Lazy);
                IList _target = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(_attribute.TargetModelType));
                foreach (BaseModel _item in _source)
                {
                    BaseModel _itemTarget = (BaseModel)Activator.CreateInstance(_attribute.TargetModelType, _parametros);
                    PropertyInfo _entityTarget = _item.ModelProperties.Where(x => {
                        return _attribute.ToFieldName.Split('.')[1].Equals(((System.Reflection.MemberInfo)x.Property).Name); 
                    }).Single().Property;
                    _itemTarget.Load((int)_entityTarget.GetValue(_item, null));
                    _target.Add(_itemTarget);
                }

                Property.Property.SetValue(BaseModel, _target, null);
            }

        }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IRow Row, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Row, BaseModel, Property);
        }


        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            object[] _parametros = { (object)Workspace };
            EntityManyToManyFieldAttribute _attribute = (EntityManyToManyFieldAttribute)Property.Attribute;
            BaseModel _mtmField = (BaseModel)Activator.CreateInstance(_attribute.RelateModelType, _parametros);

            string _KeyObj = Feature.get_Value(Feature.Fields.FindField(BaseModel.KeyField)).ToString();
            Int32 _keyValue = !String.IsNullOrEmpty(_KeyObj) ? Convert.ToInt32(_KeyObj) : 0;
            if (_keyValue > 0)
            {
                var _source = _mtmField.Search(_attribute.FromFieldName + "=" + _keyValue, BaseModel.LoadMethod.Lazy);

                IList _target = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(_attribute.TargetModelType));
                foreach (BaseModel _item in _source)
                {
                    BaseModel _itemTarget = (BaseModel)Activator.CreateInstance(_attribute.TargetModelType, _parametros);
                    PropertyInfo _entityTarget = _item.ModelProperties.Where(x =>
                    {
                        return _attribute.ToFieldName.Split('.')[1].Equals(((System.Reflection.MemberInfo)x.Property).Name);
                    }).Single().Property;
                    _itemTarget.Load((int)_entityTarget.GetValue(_item, null));
                    _target.Add(_itemTarget);
                }

                Property.Property.SetValue(BaseModel, _target, null);
            }
        }

        public void Load(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IFeature Feature, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Feature, BaseModel, Property);
        }


        public void Save(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            this.Save(Workspace, BaseModel, Property);
        }

        public void Save(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, ESRI.ArcGIS.Geodatabase.IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            this.Save(Workspace, BaseModel, Property);
        }


        public string Save(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, BaseModel BaseModel, ModelProperty Property)
        {

            object[] _parametros = { (object)Workspace };
            ModelProperty _mpChaveFromField = BaseModel.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First();
            int chave = (int)BaseModel.GetType().GetProperty(_mpChaveFromField.Property.Name).GetValue(BaseModel, null);
            EntityManyToManyFieldAttribute _attribute = (EntityManyToManyFieldAttribute)Property.Attribute;
            BaseModel _mtmField = (BaseModel)Activator.CreateInstance(_attribute.TargetModelType, _parametros);

            BaseModel _relateField = (BaseModel)Activator.CreateInstance(_attribute.RelateModelType, _parametros);
            _relateField.Delete(_attribute.FromFieldName + "=" + chave);


            foreach (ModelProperty _mp in BaseModel.ModelProperties.Where(x => x.Attribute.FieldName.Equals(Property.Attribute.FieldName)))
            {
                if (_mp.Property.GetValue(BaseModel, null) != null)
                {
                    IList _list = (IList)_mp.Property.GetValue(BaseModel, null);

                    foreach (var _item in _list)
                    {
                        ModelProperty _mpChaveToField = BaseModel.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First();
                        int _value = Convert.ToInt32(_item.GetType().GetProperty(_mpChaveToField.Property.Name).GetValue(_item, null).ToString());

                        var _objFinal = Activator.CreateInstance(_attribute.RelateModelType,_parametros);
                        _objFinal.GetType().GetProperty(_attribute.FromFieldName.Split('.')[1]).SetValue(_objFinal, chave, null);
                        _objFinal.GetType().GetProperty(_attribute.ToFieldName.Split('.')[1]).SetValue(_objFinal, _value, null);
                        ((BaseModel)_objFinal).Save();
                    }
                }
            }



            return "";
        }

        public void Delete(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace, BaseModel BaseModel, ModelProperty _property)
        {
            ModelProperty _mpChaveFromField = BaseModel.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First();
            int chave = (int)BaseModel.GetType().GetProperty(_mpChaveFromField.Property.Name).GetValue(BaseModel, null);
            object[] _parametros = { (object)Workspace };

            EntityManyToManyFieldAttribute _attribute = (EntityManyToManyFieldAttribute)_property.Attribute;
            BaseModel _mtmField = (BaseModel)Activator.CreateInstance(_attribute.TargetModelType, _parametros);

            BaseModel _relateField = (BaseModel)Activator.CreateInstance(_attribute.RelateModelType, _parametros);
            _relateField.Delete(_attribute.FromFieldName + "=" + chave);


        }

        private Type _TargetModelType;
        private Type _RelateModelType;
        private string _FromFieldName;
        private string _ToFieldName;

    }
}
