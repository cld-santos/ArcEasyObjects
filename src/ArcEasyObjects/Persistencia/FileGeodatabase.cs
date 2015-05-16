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
    public class FileGeodatabase:IPersistencia
    {

        public FileGeodatabase(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }

        public void Salvar(Model AEOModel)
        {
            
            IFeature feat = ((IFeatureWorkspace)_workspace).OpenFeatureClass(AEOModel.NomeFeatureClass).CreateFeature();

            PropertyInfo[] _properties = AEOModel.GetType().GetProperties();

            foreach (PropertyInfo _property in _properties)
            {

                object[] attributes = _property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is FeatureClassFieldsAEOAttribute)
                    {
                        FeatureClassFieldsAEOAttribute a = (FeatureClassFieldsAEOAttribute)attribute;
                        feat.set_Value(feat.Fields.FindField(a.FieldName), Convert.ChangeType(_property.GetValue(AEOModel),a.FieldType));
                    }

                }
            }
            feat.Store();

        }
    
        private IWorkspace _workspace;
        private IFeatureClass _featureClass;
    }
}
