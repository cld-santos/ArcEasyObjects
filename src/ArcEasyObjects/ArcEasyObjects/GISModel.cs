using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public class GISModel : BaseModel
    {
        public GISModel(IWorkspace Workspace) : base(Workspace) { }
        
        [EntityField("OBJECTID", typeof(Int32))]
        public Int32 ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        [EntityShapeField(typeof(IGeometry))]
        public IGeometry Geometry
        {
            get { return _Geometry; }
            set { _Geometry = value; }
        }

        private int _ObjectId;
        private IGeometry _Geometry;

    }
}
