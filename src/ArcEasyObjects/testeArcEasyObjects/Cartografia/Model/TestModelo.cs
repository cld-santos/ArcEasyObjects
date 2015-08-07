using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using System.Runtime.Serialization;
namespace testeArcEasyObjects
{
    [DataContract]
    [EntityAEO("CSANTOS.PT_TESTMODELO", Type.FeatureClass)]
    public class TestModel : ArcEasyObjects.BaseModel
    {
        private int _campoChave;

        [DataMember]
        [EntityField("NU_CAMPO_ID", typeof(Int32))]
        public int CampoChave
        {
            get { return _campoChave; }
            set { _campoChave = value; }
        }
    }
}
