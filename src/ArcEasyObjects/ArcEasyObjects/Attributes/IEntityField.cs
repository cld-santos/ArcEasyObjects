using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public interface IEntityField
    {
        string FieldName { get; }
        Type FieldType { get; }
        void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property);
        void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod);
    }
}
