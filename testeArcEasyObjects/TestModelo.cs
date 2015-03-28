using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects;

namespace testeArcEasyObjects
{
    [FeatureClassAEO("CSANTOS.PT_TESTMODELO")]
    public class TestModelo : ArcEasyObjects.IModel
    {
        private int _campoChave;

        [FeatureClassFieldsAEO("NU_CAMPO_ID", typeof(Int32))]
        public int CampoChave
        {
            get { return _campoChave; }
            set { _campoChave = value; }
        }
    }
}
