using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testeArcEasyObjects.Cartografia.Model;

namespace testeArcEasyObjects.Tests
{
    [TestClass]
    public class LoadModelTest
    {
        [TestMethod]
        public void LoadSomeModels()
        {
            for (int c = 0; c < 100; c++)
            {
                ProjetoStandard _projetoStd = new ProjetoStandard();
            }
        }
    }
}
