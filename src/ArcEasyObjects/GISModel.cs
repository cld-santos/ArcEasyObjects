using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcEasyObjects
{
    public class GISModel : BaseModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public GISModel(IWorkspace Workspace) : base(Workspace) { }


        [EntityFieldAEO("OBJECTID", typeof(Int32))]
        public Int32 ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        private int _ObjectId;

    }
}
