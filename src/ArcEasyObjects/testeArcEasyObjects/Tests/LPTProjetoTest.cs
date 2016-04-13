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
    public class LPTProjetoTest
    {
        

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = _openWorkspace();
        }


        [TestMethod]
        public void deveBuscarLPTProjeto()
        {
            LPTProjeto lptProjeto = new LPTProjeto(_workspace);
            var lptprojetos = lptProjeto.Search(String.Format("LPTProjeto.NotaObra = '{0}'", "9003005019"));
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




        private static IWorkspace _openWorkspace()
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

            IPropertySet prop = new PropertySet();
            prop.SetProperty("SERVER", "10.0.71.26");
            prop.SetProperty("INSTANCE", "sde:oracle11g:CLBD.COELBANET");
            prop.SetProperty("USER", "CLBLPTADMQA");
            prop.SetProperty("PASSWORD", "coelba");
            //prop.SetProperty("VERSION", Versao);

            return workspaceFactory.Open(prop, 0);
        }

        #endregion


    }
}
