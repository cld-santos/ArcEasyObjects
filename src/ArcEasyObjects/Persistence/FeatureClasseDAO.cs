﻿using ArcEasyObjects.Attributes;
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
                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAEOAttribute)))
                {
                    _property.Property.SetValue(AEOModel,
                                                Convert.ChangeType(_feature.get_Value(_feature.Fields.FindField(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType),null);
                }

                ((GISModel)AEOModel).Geometry = _feature.ShapeCopy;
            }

            //TODO: Carregar os relacionamentos;

        }

        public void Save(BaseModel AEOModel)
        {
            IFeature feat = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.EntityName).CreateFeature();

            foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAEOAttribute)))
            {
                 feat.set_Value(feat.Fields.FindField(_property.Attribute.FieldName),
                               Convert.ChangeType(_property.Property.GetValue(AEOModel, null), _property.Attribute.FieldType));
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


            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !"OBJECTID".Equals(x.Attribute.FieldName) && !(x.Attribute is EntityShapeFieldAEOAttribute)))    
            {
                feat.set_Value(feat.Fields.FindField(_property.Attribute.FieldName),
                               Convert.ChangeType(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType));
            }

            feat.Shape = ((GISModel)BaseModel).Geometry;
            feat.Store();
        }

        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();

            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AOWhereClause;

            IFeatureCursor _rows = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.EntityName).Search(_queryParamns, true);
            IFeature _feature = _rows.NextFeature();

            while (_feature != null)
            {
                object[] _parameters = { _workspace };
                object _model = Activator.CreateInstance(AEOModel.GetType(), _parameters);

                foreach (ModelProperty _property in AEOModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAEOAttribute)))
                {
                    _property.Property.SetValue(_model,
                                                Convert.ChangeType(_feature.get_Value(_feature.Fields.FindField(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType), null);
                }
                ((GISModel)_model).Geometry = _feature.ShapeCopy;
                _ModelsReturn.Add((BaseModel)_model);
                _feature = _rows.NextFeature();

            }

            return _ModelsReturn;

        }

        private IWorkspace _workspace;

    }
}
