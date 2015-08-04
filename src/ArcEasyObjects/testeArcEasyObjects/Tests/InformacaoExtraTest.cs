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
    public class InformacaoExtraTest
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = openWorkspace();
        }

        
        public void mustSaveAModel()
        {
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);
                
            _infoExtra.CodigoInformacaoExtra = 1;
            _infoExtra.CodigoPontoNotavel = 1;
            _infoExtra.Informacoes = "Testes";

            _infoExtra.Save();
        }

        [TestMethod]
        public void mustLoadAModel()
        {
            mustSaveAModel();
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);

            _infoExtra.Load(1);

            Assert.AreEqual(_infoExtra.CodigoInformacaoExtra,1);
            Assert.AreEqual(_infoExtra.CodigoPontoNotavel, 1);
            Assert.AreEqual(_infoExtra.Informacoes, "Testes");
            _infoExtra.Delete();

        }
            
        [TestMethod]
        public void mustSearchSomeFeatures()
        {
            mustSaveAModel();
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);
            var _infosExtra = _infoExtra.Search("InformacaoExtra.CodigoPontoNotavel = 1");

            foreach (InformacaoExtra _item in _infosExtra)
            {
                Assert.AreEqual(_item.Informacoes, "Testes");

            }

            Assert.IsTrue(_infosExtra.Count > 0);
            _infoExtra.Load(1);
            _infoExtra.Delete();
        }

        [TestMethod]
        public void mustUpdateAModel()
        {
            mustSaveAModel();
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);

            _infoExtra.Load(1);

            _infoExtra.Informacoes = "Sem Testes";

            _infoExtra.Update();

            InformacaoExtra _infoExtraResultado = new InformacaoExtra(_workspace);
            _infoExtraResultado.Load(1);
            Assert.AreEqual(_infoExtraResultado.Informacoes, "Sem Testes");
            _infoExtra.Delete();
        }

        [TestMethod]
        public void mustDeleteAModel()
        {
            mustSaveAModel();
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);

            _infoExtra.Load(1);
            _infoExtra.Delete();

            InformacaoExtra _infoExtraResultado = new InformacaoExtra(_workspace);
            _infoExtraResultado.Load(1);
            Assert.IsNull(_infoExtraResultado.Informacoes);
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
