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
    public class AvaliacaoTest
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = openWorkspace();
        }

        
        public void mustSaveAModel()
        {
            Avaliacao _Avaliacao = new Avaliacao(_workspace);
                
            _Avaliacao.Codigo = 1;
            _Avaliacao.Comentario = "Relativamente bom!";
            _Avaliacao.Nome = "Teste";

            _Avaliacao.Save();
        }

        [TestMethod]
        public void mustLoadAModel()
        {
            mustSaveAModel();
            Avaliacao _Avaliacao = new Avaliacao(_workspace);

            _Avaliacao.Load(1);

            Assert.AreEqual(_Avaliacao.Codigo,1);
            Assert.AreEqual(_Avaliacao.Comentario,"Relativamente bom!");
            Assert.AreEqual(_Avaliacao.Nome,"Teste");
            _Avaliacao.Delete();

        }
            
        [TestMethod]
        public void mustSearchSomeFeatures()
        {
            mustSaveAModel();
            Avaliacao _Avaliacao = new Avaliacao(_workspace);
            var _Avaliacoes = _Avaliacao.Search("Avaliacao.Codigo = 1");

            foreach (Avaliacao _item in _Avaliacoes)
            {
                Assert.AreEqual(_item.Comentario, "Relativamente bom!");
                Assert.AreEqual(_item.Nome, "Teste");
            }

            Assert.IsTrue(_Avaliacoes.Count > 0);
            _Avaliacao.Load(1);
            _Avaliacao.Delete();
        }

        [TestMethod]
        public void mustUpdateAModel()
        {
            mustSaveAModel();
            Avaliacao _Avaliacao = new Avaliacao(_workspace);

            _Avaliacao.Load(1);

            _Avaliacao.Comentario = "Sem Testes";

            _Avaliacao.Update();

            Avaliacao _AvaliacaoResultado = new Avaliacao(_workspace);
            _AvaliacaoResultado.Load(1);
            Assert.AreEqual(_AvaliacaoResultado.Comentario, "Sem Testes");
            _Avaliacao.Delete();
        }

        [TestMethod]
        public void mustDeleteAModel()
        {
            mustSaveAModel();
            Avaliacao _Avaliacao = new Avaliacao(_workspace);

            _Avaliacao.Load(1);
            _Avaliacao.Delete();

            Avaliacao _AvaliacaoResultado = new Avaliacao(_workspace);
            _AvaliacaoResultado.Load(1);
            Assert.IsNull(_AvaliacaoResultado.Comentario);
        }

        [TestMethod]
        public void mustLoadASubModel()
        {
            PontoNotavel _pn = new PontoNotavel(_workspace);

            _pn.Codigo = 989;
            _pn.Descricao = "Testando a inclusao GISTAble.";
            _pn.Nome = "Teste Inclusao.";

            IPoint _ponto = new PointClass();

            _ponto.PutCoords(-5118733.117, -2667994.655);

            _pn.Geometry = _ponto;

            _pn.Save();

            Avaliacao _Avaliacao = new Avaliacao(_workspace);

            _Avaliacao.Codigo = 898;
            _Avaliacao.Comentario = "Relativamente bom!";
            _Avaliacao.Nome = "Teste";
            _Avaliacao.PontoNotavelAvaliado = _pn;

            _Avaliacao.Save();

            Avaliacao _AvaliacaoTeste = new Avaliacao(_workspace);
            _AvaliacaoTeste.Load(898);

            Assert.IsNotNull(_AvaliacaoTeste.PontoNotavelAvaliado);
            Assert.AreEqual(_AvaliacaoTeste.PontoNotavelAvaliado.Nome, "Teste Inclusao.");

            //TODO:Sem dar load não consigo deletar;
            _pn.Load(989);
            _pn.Delete();
            _Avaliacao.Load(898);
            _Avaliacao.Delete();
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
