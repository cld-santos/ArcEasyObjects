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
                
        public void Save(BaseModel BaseModel)
        {
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).CreateRow();

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityOIDFieldAttribute) && !(x.Attribute is EntityShapeFieldAttribute) && !(x.Attribute is EntityManyToManyFieldAttribute)))
            {
                _property.Attribute.Save(_workspace, _row, BaseModel, _property);
            }

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityManyToManyFieldAttribute)))
            {
                _property.Attribute.Save(_workspace, BaseModel, _property);
            }

            ((GISModel)BaseModel).ObjectId = _row.OID;

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

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityOIDFieldAttribute) && !(x.Attribute is EntityShapeFieldAttribute)))
            {
                _property.Attribute.Save(_workspace, _row, BaseModel, _property);
            }

            _row.Store();

        }

        public void Load(BaseModel BaseModel, int KeyFieldValue, BaseModel.LoadMethod ChooseLoadMethod)
        {
            if (String.IsNullOrEmpty(BaseModel.KeyField)) throw new KeyFieldNotFoundException();
            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = BaseModel.KeyField + "=" + KeyFieldValue;

            ICursor _rows = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName).Search(_queryParamns, true);
            IRow _row = _rows.NextRow();
            if (_row != null)
            {
                foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Attribute.Load(_workspace, _row, BaseModel, _property);
                }
            }
        }

        public void Load(BaseModel BaseModel, int KeyFieldValue)
        {
            this.Load(BaseModel, KeyFieldValue, BaseModel.LoadMethod.Lazy);
        }

        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause, BaseModel.LoadMethod ChooseMethod)
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
                        _property.Attribute.Load(_workspace, _row, (BaseModel)_model, _property, ChooseMethod);
                }

                _ModelsReturn.Add((BaseModel)_model);
            }

            return _ModelsReturn;

        }

        private IWorkspace _workspace;



        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause)
        {
            return this.Search(AEOModel, AOWhereClause, BaseModel.LoadMethod.Lazy);

        }


        public void Delete(BaseModel AEOModel, string AOWhereClause)
        {
            throw new NotImplementedException();
        }
    }
}
