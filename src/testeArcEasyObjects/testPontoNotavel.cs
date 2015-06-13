using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using testeArcEasyObjects.Cartografia.Model;

namespace testeArcEasyObjects
{
    [TestClass]
    public class testPontoNotavel
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = openWorkspace();
        }

        [TestMethod]
        public void mustSaveAModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Codigo = 1;
            _pn.Descricao = "Testando a inclusao por uma camada transparente.";
            _pn.Nome = "Teste Inclusao.";

            _pn.Save();
            
        }

        [TestMethod]
        public void mustLoadAModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Load(1);

            Assert.AreEqual(_pn.Descricao, "Testando a inclusao por uma camada transparente.");
            Assert.AreEqual(_pn.Nome ,"Teste Inclusao.");

        }
            
        [TestMethod]
        public void mustSearchSomeFeatures()
        {   
            //TODO: Criar uma lista do tipo desejado, aplicar generics.
            //List<PontoNotavel> _pns = new List<PontoNotavel>();
            
            PontoNotavel _pn = new PontoNotavel(_workspace);

            //TODO:Replace only words
            var _pns = _pn.Search("PontoNotavel.Codigo = 1");
            
            foreach (PontoNotavel _item in _pns)
            {
                Assert.AreEqual(_item.Nome, "Teste Inclusao.");
            }
                       
            Assert.IsTrue(_pns.Count > 0);

        }

        [TestMethod]
        public void mustUpdateAModel()  
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Load(1);
            _pn.Descricao = "Ponto Notável Atualizado";
            _pn.Update();

            _pn.Load(1);

            Assert.AreEqual(_pn.Descricao, "Ponto Notável Atualizado");
            Assert.AreEqual(_pn.Nome, "Teste Inclusao.");

        }

        [TestMethod]
        public void mustdeleteAModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);
            
            _pn.Load(1);
            _pn.Delete();

            Assert.IsNull(_pn.Descricao);
            Assert.IsNull(_pn.Nome);

        }



        #region Métodos e Atributos Privados
        private static void inicializaLicenca()
        {

            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                {
                    Console.WriteLine("This application could not load the correct version of ArcGIS.");
                    return;
                }
            }

            LicenseInitializer aoLicenseInitializer = new LicenseInitializer();
            if (!aoLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork }))
            {
                Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown. LicenseMessage: " + aoLicenseInitializer.LicenseMessage());
                aoLicenseInitializer.ShutdownApplication();
                return;
            }
        }



        public static IWorkspace openWorkspace()
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.OpenFromFile(@"E:\csantos\src\ArcEasyObjects\data\aeoSample.gdb", 0);
        }
        private static IWorkspace _workspace;
        #endregion

    }
}
