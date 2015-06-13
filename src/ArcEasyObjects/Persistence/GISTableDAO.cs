using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AEOModel.KeyField + "=" + KeyFieldValue;

            ICursor _rows = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.EntityName).Search(_queryParamns, true);
            IRow _row = _rows.NextRow();
            if (_row != null)
            {
                foreach (ModelProperty _property in AEOModel.ModelProperties)
                {
                    _property.Property.SetValue(AEOModel,
                                                Convert.ChangeType(_row.get_Value(_row.Fields.FindField(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType));
                }
            }

        }

        public void Save(BaseModel AEOModel)
        {
            
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.EntityName).CreateRow();

            foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName)))
            {
                _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), 
                                Convert.ChangeType(_property.Property.GetValue(AEOModel), 
                                                   _property.Attribute.FieldType));
            }

            _row.Store();

        }

        public void Delete(BaseModel BaseModel)
        {
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).GetRow(((GISModel)BaseModel).ObjectId);

            _row.Delete();

            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _property.Property.SetValue(BaseModel, null);
            }
        }

        public void Update(BaseModel BaseModel)
        {

            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).GetRow(((GISModel)BaseModel).ObjectId);

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName)))
            {
                _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName),
                                Convert.ChangeType(_property.Property.GetValue(BaseModel),
                                                   _property.Attribute.FieldType));
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

                foreach (ModelProperty _property in AEOModel.ModelProperties)
                {
                    _property.Property.SetValue(_model,
                                                Convert.ChangeType(_row.get_Value(_row.Fields.FindField(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType));
                }

                _ModelsReturn.Add((BaseModel)_model);
            }

            return _ModelsReturn;

        }

        private IWorkspace _workspace;

    }
}
