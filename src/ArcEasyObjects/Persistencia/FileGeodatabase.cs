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

            foreach (ModelProperty _property in AEOModel.ModelProperties)
            {
                feat.set_Value(feat.Fields.FindField(_property.Attribute.FieldName), Convert.ChangeType(_property.Property.GetValue(AEOModel), _property.Attribute.FieldType));
            }
            
            feat.Store();

        }
    
        private IWorkspace _workspace;
    }
}
