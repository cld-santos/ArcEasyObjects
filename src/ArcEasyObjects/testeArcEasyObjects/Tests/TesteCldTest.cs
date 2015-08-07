using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using testeArcEasyObjects.Cartografia.Model;

namespace testeArcEasyObjects
{
    [TestClass]
    public class TesteCldTest
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = _openWorkspace("");
        }


        [TestMethod]
        public TesteCld deveSalvarUmTesteCld()
        {
            TesteCld _testeCld = new TesteCld(_workspace);
            _testeCld.Nome = "teste claudio";
            _testeCld.Data = DateTime.Now.Date;
            _testeCld.Tempo = DateTime.Now;
            _testeCld.Flag = true;            
            _testeCld.Save();
            return _testeCld;
        }


        [TestMethod]
        public void deveAtualizarUmTesteCld()
        {
            TesteCld _testeCld = deveSalvarUmTesteCld();

            _testeCld.Nome = "teste claudio denovo";
            _testeCld.Data = DateTime.Now.Date;
            _testeCld.Tempo = DateTime.Now;
            _testeCld.Flag = true; 
            _testeCld.Update();

            Assert.IsTrue(_testeCld.Nome == "teste claudio denovo");
            _testeCld.Delete();
        }

        [TestMethod]
        public void deveBuscarUmTesteCld()
        {
            TesteCld _testeCld = deveSalvarUmTesteCld();
            
            var _testesCld = _testeCld.Search("TesteCld.Nome = 'teste claudio'");

            Assert.IsTrue(_testesCld.Count > 0);
            _testeCld.Delete();
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
