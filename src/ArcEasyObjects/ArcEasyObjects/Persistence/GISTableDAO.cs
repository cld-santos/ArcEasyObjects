using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArcEasyObjects.ExceptionAEO;

namespace ArcEasyObjects.Persistence
{
    public class GISTableDAO:IPersistence
    {

        public GISTableDAO(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }

        public void Load(BaseModel AEOModel, int KeyFieldValue) 
        {
            if (String.IsNullOrEmpty(AEOModel.KeyField)) throw new KeyFieldNotFoundException();
            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AEOModel.KeyField + "=" + KeyFieldValue;

            ICursor _rows = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.EntityName).Search(_queryParamns, true);
            IRow _row = _rows.NextRow();
            if (_row != null)
            {
                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Attribute.Load(_workspace, _row, AEOModel, _property);
                }
            }
        }


        
        public void Save(BaseModel AEOModel)
        {
            
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.EntityName).CreateRow();

            foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAttribute)))
            {
                if (_property.Attribute is EntityKeyFieldAttribute)
                {

                    EntityKeyFieldAttribute _keyField = (EntityKeyFieldAttribute)_property.Attribute;
                    if (String.IsNullOrEmpty(_keyField.Sequence))
                    {
                        _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), Convert.ChangeType(_property.Property.GetValue(AEOModel, null), _property.Attribute.FieldType));

                    }
                    else
                    {
                        ICursor cursor = Helper.GDBCursor.obterCursor((IFeatureWorkspace)_workspace, "SYS.DUAL", _keyField.Sequence + ".NEXTVAL", "");
                        IRow row = cursor.NextRow();
                        _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), Convert.ChangeType(row.get_Value(0).ToString(), _property.Attribute.FieldType));
                    }
                }
                else if (_property.Attribute is EntityOneToOneFieldAttribute)
                {
                    BaseModel _bm = (BaseModel)_property.Property.GetValue(AEOModel, null);
                    if (_bm != null)
                    {
                        ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                        Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                        _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), _keyValue);
                    }
                }
                else
                {
                    _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), Convert.ChangeType(_property.Property.GetValue(AEOModel, null), _property.Attribute.FieldType));
                }

            }

            _row.Store();

        }

        public void Delete(BaseModel BaseModel)
        {
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).GetRow(((GISModel)BaseModel).ObjectId);

            _row.Delete();

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
            {
                _property.Property.SetValue(BaseModel, null,null);
            }
        }

        public void Update(BaseModel BaseModel)
        {

            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).GetRow(((GISModel)BaseModel).ObjectId);

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAttribute)))
            {
                //TODO: Apply Observer pattern
                if (_property.Attribute is EntityOneToOneFieldAttribute)
                {
                    BaseModel _bm = (BaseModel)_property.Property.GetValue(BaseModel, null);
                    if (_bm != null)
                    {
                        ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                        Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                        _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), _keyValue);
                    }
                }
                else
                {
                    _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName),
                                    Convert.ChangeType(_property.Property.GetValue(BaseModel, null),
                                                       _property.Attribute.FieldType));
                }
            }

            _row.Store();

        }

        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();

            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AOWhereClause;

            ICursor _rows = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.EntityName).Search(_queryParamns, true);
            IRow _row;

            while ((_row = _rows.NextRow()) != null)
            {
                object[] _parameters = { _workspace };
                object _model = Activator.CreateInstance(AEOModel.GetType(), _parameters);

                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                        _property.Attribute.Load(_workspace, _row, (BaseModel)_model, _property);
                }

                _ModelsReturn.Add((BaseModel)_model);
            }

            return _ModelsReturn;

        }

        private IWorkspace _workspace;



        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause, BaseModel.LoadMethod ChooseMethod)
        {
            throw new NotImplementedException();
        }
    }
}
