using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testeArcEasyObjects
{
    [TestClass]
    public class testArcEasyObjects
    {


        [TestMethod]
        public void mustGetFeatureClassName()
        {
            ArcEasyObjects.BaseModel _modelo = new TestModel();
            ArcEasyObjects.EntityAEO _featureAEO = new ArcEasyObjects.EntityAEO(_modelo);

            string _FeatureClassName = _featureAEO.getFeatureClassName();

            Assert.AreEqual(_FeatureClassName, "CSANTOS.PT_TESTMODELO");


        }

        [TestMethod]
        public void mustGetOFeatureClassFields()
        {
            ArcEasyObjects.BaseModel _model = new TestModel();
            ArcEasyObjects.EntityAEO _featureAEO = new ArcEasyObjects.EntityAEO(_model);

            //string _nomeFeatureClassField = _featureAEO.obterAtributosFeatureClass();

            //Assert.AreEqual(_nomeFeatureClassField, "NU_CAMPO_ID");


        }
    }
}
