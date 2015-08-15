using ArcEasyObjects;
using ArcEasyObjects.ExceptionAEO;
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
    public class SimulacaoProjetoTest
    {

        
        public void mustSaveAModel()
        {
            SimulacaoProjeto sp = new SimulacaoProjeto(_workspace);
            sp.de_observacao = "Sem Testes";
            sp.no_versao = "tests";
            sp.nu_projeto_id = 9999;
            sp.dt_criacao = DateTime.Now.Date;
            sp.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(KeyFieldNotFoundException))]
        public void mustLoadAModel()
        {
            SimulacaoProjeto sp = new SimulacaoProjeto(_workspace);
            sp.Load(9999);

        }
            
        [TestMethod]
        public void mustSearchSomeFeatures()
        {
            mustSaveAModel();

            SimulacaoProjeto sp = new SimulacaoProjeto(_workspace);
            var _simulacoes = sp.Search("SimulacaoProjeto.nu_projeto_id = 9999");

            foreach (SimulacaoProjeto _item in _simulacoes)
            {
                Assert.AreEqual(_item.no_versao, "tests");
            }
            Assert.IsTrue(_simulacoes.Count > 0);
            sp = (SimulacaoProjeto)_simulacoes[0];
            sp.Delete();
        }

        [TestMethod]
        public void mustUpdateAModel()
        {
            mustSaveAModel();
            SimulacaoProjeto sp = new SimulacaoProjeto(_workspace);
            var _simulacoes = sp.Search("SimulacaoProjeto.nu_projeto_id = 9999");

            foreach (SimulacaoProjeto _item in _simulacoes)
            {
                Assert.AreEqual(_item.no_versao, "tests");
                sp = _item;
            }

            sp.de_observacao = "Sem Testes";
            sp.no_versao = "testado";

            sp.Update();


            _simulacoes = sp.Search("SimulacaoProjeto.nu_projeto_id = 9999");

            foreach (SimulacaoProjeto _item in _simulacoes)
            {
                Assert.AreEqual(_item.no_versao, "testado");
            }

            sp.Delete();

        }

        [TestMethod]
        public void mustDeleteAModel()
        {
            mustSaveAModel();
            SimulacaoProjeto sp = new SimulacaoProjeto(_workspace);
            var _simulacoes = sp.Search("SimulacaoProjeto.nu_projeto_id = 9999");

            foreach (SimulacaoProjeto _item in _simulacoes)
            {
                //Assert.AreEqual(_item.no_versao, "tests");
                sp = _item;
            }

            Assert.AreEqual(sp.de_observacao, "Sem Testes");
            Assert.AreEqual(sp.no_versao, "tests");

            sp.Delete();

        }



        #region Métodos e Atributos Privados
        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = _openWorkspace("");
        }


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

        private static IWorkspace _openWorkspace(string Versao)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

            IPropertySet prop = new PropertySet();
            prop.SetProperty("SERVER", "10.150.17.21");
            prop.SetProperty("INSTANCE", "sde:oracle11g:GSEPROJ");
            prop.SetProperty("USER", "neogse");
            prop.SetProperty("PASSWORD", "coelba");
            prop.SetProperty("VERSION", Versao);

            return workspaceFactory.Open(prop, 0);
        }

        private static IWorkspace _workspace;
        #endregion

    }
}
