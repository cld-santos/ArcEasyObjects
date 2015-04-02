using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testeArcEasyObjects
{
    [TestClass]
    public class testArcEasyObjects
    {
        [TestMethod]
        public void deveObterOFeatureClassNameDeUmModel()
        {
            ArcEasyObjects.IModel _modelo = new TestModelo();
            ArcEasyObjects.FeatureAEO _featureAEO = new ArcEasyObjects.FeatureAEO(_modelo);

            string _nomeFeatureClass = _featureAEO.obterNomeFeatureClass();

            Assert.AreEqual(_nomeFeatureClass, "CSANTOS.PT_TESTMODELO");


        }

        [TestMethod]
        public void deveObterOFeatureClassFieldDeUmModel()
        {
            ArcEasyObjects.IModel _modelo = new TestModelo();
            ArcEasyObjects.FeatureAEO _featureAEO = new ArcEasyObjects.FeatureAEO(_modelo);

            string _nomeFeatureClass = _featureAEO.obterAtributosFeatureClass();

            Assert.AreEqual(_nomeFeatureClass, "NU_CAMPO_ID");


        }
    }
}
