using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using testeArcEasyObjects.Cartografia.Model;

namespace testeArcEasyObjects.Cartografia.Model
{
    [TestClass]
    public class UsuarioSAPTest
    {
        

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = _openWorkspace("");
        }

        [TestMethod]
        public void deveSalvarUmUsuarioSAP()
        {
            int _IdUsuarioSAP;
            UsuarioSAP _usuarioSap = new UsuarioSAP(_workspace);
            _usuarioSap.Nome = "Teste";
            _usuarioSap.Chave = "testes testes";
            _usuarioSap.CodigoUsuarioGSE = 99;
            _usuarioSap.Save();
            _IdUsuarioSAP = _usuarioSap.Codigo;
            _usuarioSap.Load(_IdUsuarioSAP);
            _usuarioSap.Delete();
        }

        [TestMethod]
        public void deveCarregarUmUsuarioSAP()
        {
            int _IdUsuarioSAP;
            UsuarioSAP _usuarioSap = new UsuarioSAP(_workspace);
            _usuarioSap.Nome = "Teste";
            _usuarioSap.Chave = "testes testes";
            _usuarioSap.CodigoUsuarioGSE = 99;
            _usuarioSap.Save();
            _IdUsuarioSAP = _usuarioSap.Codigo;

            _usuarioSap.Load(_IdUsuarioSAP);
            Assert.AreEqual(_usuarioSap.Chave, "testes testes");
            Assert.AreEqual(_usuarioSap.CodigoUsuarioGSE, 99);
            _usuarioSap.Load(_IdUsuarioSAP);
            _usuarioSap.Delete();
        }

        [TestMethod]
        public void deveAtualizarUmUsuarioSAP()
        {
            int _IdUsuarioSAP;
            UsuarioSAP _usuarioSap = new UsuarioSAP(_workspace);
            _usuarioSap.Nome = "Teste";
            _usuarioSap.Chave = "testes testes";
            _usuarioSap.CodigoUsuarioGSE = 99;
            _usuarioSap.Save();
            _IdUsuarioSAP = _usuarioSap.Codigo;

            _usuarioSap.Load(_IdUsuarioSAP);
            _usuarioSap.Chave = "setset setset";
            _usuarioSap.Update();
            Assert.AreEqual(_usuarioSap.Chave, "setset setset");
            Assert.AreEqual(_usuarioSap.CodigoUsuarioGSE, 99);
            _usuarioSap.Load(_IdUsuarioSAP);
            _usuarioSap.Delete();
        }

        [TestMethod]
        public void deveDeletarUmUsuarioSAP()
        {
            int _IdUsuarioSAP;
            UsuarioSAP _usuarioSap = new UsuarioSAP(_workspace);
            _usuarioSap.Nome = "Teste";
            _usuarioSap.Chave = "testes testes";
            _usuarioSap.CodigoUsuarioGSE = 99;
            _usuarioSap.Save();
            _IdUsuarioSAP = _usuarioSap.Codigo;

            _usuarioSap.Load(_IdUsuarioSAP);
            _usuarioSap.Delete();
        }

        #region Métodos e Atributos Privados
        private static IWorkspace _workspace;

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



        public static IWorkspace _openWorkspace()
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
        #endregion


    }
}
