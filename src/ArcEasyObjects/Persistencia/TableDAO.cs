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
    public class TableDAO:IPersistence
    {

        public TableDAO(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }

        public void Save(Model AEOModel)
        {
            
            IRow _row = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.NomeFeatureClass).CreateRow();

            foreach (ModelProperty _property in AEOModel.ModelProperties)
            {
                _row.set_Value(_row.Fields.FindField(_property.Attribute.FieldName), 
                                Convert.ChangeType(_property.Property.GetValue(AEOModel), 
                                                   _property.Attribute.FieldType));
            }

            _row.Store();

        }
    
        private IWorkspace _workspace;

        public void Load(Model AEOModel, int KeyFieldValue)
        {
            IQueryFilter _queryParamns = new QueryFilter();
            _queryParamns.WhereClause = AEOModel.KeyField + "=" + KeyFieldValue;

            ICursor _rows = ((IFeatureWorkspace)_workspace).OpenTable(AEOModel.NomeFeatureClass).Search(_queryParamns, true);
            IRow _row = _rows.NextRow();
            if (_row != null)
            {   
                foreach (ModelProperty _property in AEOModel.ModelProperties)
                {
                    _property.Property.SetValue(_property, 
                                                Convert.ChangeType(_row.get_Value(_row.Fields.FindField(_property.Attribute.FieldName)), 
                                                                   _property.Attribute.FieldType));
                }
            }
           
        }
    }
}
