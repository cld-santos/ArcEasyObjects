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
            ArcEasyObjects.Model _modelo = new TestModelo();
            ArcEasyObjects.FeatureAEO _featureAEO = new ArcEasyObjects.FeatureAEO(_modelo);

            string _nomeFeatureClass = _featureAEO.obterNomeFeatureClass();

            Assert.AreEqual(_nomeFeatureClass, "CSANTOS.PT_TESTMODELO");


        }

        [TestMethod]
        public void deveObterOFeatureClassFieldDeUmModel()
        {
            ArcEasyObjects.Model _modelo = new TestModelo();
            ArcEasyObjects.FeatureAEO _featureAEO = new ArcEasyObjects.FeatureAEO(_modelo);

            string _nomeFeatureClassField = _featureAEO.obterAtributosFeatureClass();

            Assert.AreEqual(_nomeFeatureClassField, "NU_CAMPO_ID");


        }
    }
}
