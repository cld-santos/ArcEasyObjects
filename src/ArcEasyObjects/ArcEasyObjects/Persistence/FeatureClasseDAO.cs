using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects.Persistence
{
    public class FeatureClassDAO:IPersistence
    {

        public FeatureClassDAO(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }

        public void Load(BaseModel AEOModel, int KeyFieldValue)
        {

            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AEOModel.KeyField + "=" + KeyFieldValue;

            IFeatureCursor _rows = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.EntityName).Search(_queryParamns, true);
            IFeature _feature = _rows.NextFeature();
            if (_feature != null)
            {
                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Attribute.Load(_workspace, _feature, AEOModel, _property);
                }

                ((GISModel)AEOModel).Geometry = _feature.ShapeCopy;
            }            
        }
        
        public void Save(BaseModel AEOModel)
        {
            IFeature feat = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.EntityName).CreateFeature();

            foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAttribute)))
            {
                _property.Attribute.Save(_workspace, feat, AEOModel, _property);
            }

            feat.Shape = ((GISModel)AEOModel).Geometry;
            feat.Store();

        }
        
        public void Delete(BaseModel BaseModel)
        {
            IFeature _feature = ((IFeatureWorkspace)_workspace).OpenFeatureClass(BaseModel.EntityName).GetFeature(((GISModel)BaseModel).ObjectId);

            _feature.Delete();

            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _property.Property.SetValue(BaseModel, null, null);
            }
        }

        public void Update(BaseModel BaseModel)
        {
            IFeature feat = ((IFeatureWorkspace)_workspace).OpenFeatureClass(BaseModel.EntityName).GetFeature(((GISModel)BaseModel).ObjectId);


            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAttribute)))    
            {
                _property.Attribute.Save(_workspace, feat, BaseModel, _property);
            }

            feat.Shape = ((GISModel)BaseModel).Geometry;
            feat.Store();
        }

        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause, BaseModel.LoadMethod ChooseMethod)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();

            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AOWhereClause;

            IFeatureCursor _rows = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.EntityName).Search(_queryParamns, true);
            IFeature _feature = _rows.NextFeature();

            while (_feature != null)
            {
                object[] _parameters = { _workspace };
                var _model = Activator.CreateInstance(AEOModel.GetType(), _parameters);

                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Attribute.Load(_workspace, _feature, (BaseModel)_model, _property,ChooseMethod);
                }
                ((GISModel)_model).Geometry = _feature.ShapeCopy;
                _ModelsReturn.Add((BaseModel)_model);
                _feature = _rows.NextFeature();

            }

            return _ModelsReturn;

        }
        
        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause)
        {
            return this.Search(AEOModel, AOWhereClause, BaseModel.LoadMethod.Lazy);
        }

        private IWorkspace _workspace;
        
    }
}
