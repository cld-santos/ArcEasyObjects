using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcEasyObjects.Persistencia
{
    public class FeatureClassDAO:IPersistence
    {

        public FeatureClassDAO(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }

        public void Save(Model AEOModel)
        {
            
            IFeature feat = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.NomeFeatureClass).CreateFeature();


            foreach (ModelProperty _property in AEOModel.ModelProperties)
            {
                feat.set_Value(feat.Fields.FindField(_property.Attribute.FieldName), 
                               Convert.ChangeType(_property.Property.GetValue(AEOModel), _property.Attribute.FieldType));
            }
            
            feat.Store();

        }

        public void Load(Model AEOModel, int KeyFieldValue)
        {

            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AEOModel.KeyField + "=" + KeyFieldValue;

            IFeatureCursor _rows = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.NomeFeatureClass).Search(_queryParamns, true);
            IFeature _feature = _rows.NextFeature();
            if (_feature != null)
            {
                foreach (ModelProperty _property in AEOModel.ModelProperties)
                {
                    _property.Property.SetValue(AEOModel, 
                                                Convert.ChangeType(_feature.get_Value(_feature.Fields.FindField(_property.Attribute.FieldName)), 
                                                                   _property.Attribute.FieldType));
                }
            }




        }
    
        private IWorkspace _workspace;


    }
}
