using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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

        public void mustSaveAModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Codigo = 1;
            _pn.Descricao = "Testando a inclusao por uma camada transparente.";
            _pn.Nome = "Teste Inclusao.";

            IPoint _ponto = new PointClass();

            _ponto.PutCoords(-5118733.117, -2667994.655);
            
            _pn.Geometry = _ponto;

            _pn.Save();

        }

        [TestMethod]
        public void mustLoadAModel()
        {
            mustSaveAModel();
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Load(1);

            Assert.AreEqual(_pn.Descricao, "Testando a inclusao por uma camada transparente.");
            Assert.AreEqual(_pn.Nome ,"Teste Inclusao.");
            
            _pn.Delete();
        }
            
        [TestMethod]
        public void mustSearchSomeFeatures()
        {   
            //TODO: Criar uma lista do tipo desejado, aplicar generics.
            //List<PontoNotavel> _pns = new List<PontoNotavel>();
            mustSaveAModel();
            PontoNotavel _pn = new PontoNotavel(_workspace);

            //TODO:Replace only words
            var _pns = _pn.Search("PontoNotavel.Codigo = 1");
            
            foreach (PontoNotavel _item in _pns)
            {
                Assert.AreEqual(_item.Nome, "Teste Inclusao.");
            }
                       
            Assert.IsTrue(_pns.Count > 0);
            _pn.Load(1);
            _pn.Delete();
        }

        [TestMethod]
        public void mustUpdateAModel()  
        {
            mustSaveAModel();
            PontoNotavel _pn = new PontoNotavel(_workspace);
            _pn.Load(1);
            _pn.Descricao = "Ponto Notável Atualizado";

            IPoint _ponto = new PointClass();
            _ponto.PutCoords(-5119200.437, -2668197.160);
            _pn.Geometry = _ponto;
            _pn.Update();

            _pn.Load(1);

            Assert.AreEqual(_pn.Descricao, "Ponto Notável Atualizado");
            Assert.AreEqual(_pn.Nome, "Teste Inclusao.");
            _pn.Delete();
        }

        [TestMethod]
        public void mustDeleteAModel()
        {
            mustSaveAModel();
            PontoNotavel _pn = new PontoNotavel(_workspace);
            
            _pn.Load(1);
            _pn.Delete();

            Assert.IsNull(_pn.Descricao);
            Assert.IsNull(_pn.Nome);

        }

        [TestMethod]
        public void mustLoadASubModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Load(999);

            Assert.IsNotNull(_pn.InformacaoExtra);
            Assert.AreEqual(_pn.InformacaoExtra.Informacoes, "Testes");

        }
        
        [TestMethod]
        public void mustSaveASubModel()
        {
            InformacaoExtra _infoExtra = new InformacaoExtra(_workspace);

            _infoExtra.CodigoInformacaoExtra = 99;
            _infoExtra.CodigoPontoNotavel = 0;
            _infoExtra.Informacoes = "Testes Novos com SubModel";

            _infoExtra.Save();

            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Codigo = 88;
            _pn.Descricao = "Testando a inclusao por uma camada transparente.";
            _pn.Nome = "Teste Inclusao.";
            _pn.InformacaoExtra = _infoExtra;
            IPoint _ponto = new PointClass();

            _ponto.PutCoords(-5118733.117, -2667994.655);

            _pn.Geometry = _ponto;

            _pn.Save();

            _pn.Load(88);

            Assert.IsNotNull(_pn.InformacaoExtra);
            Assert.AreEqual(_pn.InformacaoExtra.Informacoes, "Testes Novos com SubModel");

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
