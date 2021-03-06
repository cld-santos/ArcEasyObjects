﻿using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS;
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
    public class ProjetoStandardTest
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            inicializaLicenca();
            _workspace = _openWorkspace("");
        }

        
        public void mustSaveAModel()
        {
            ProjetoStandard _projStandard = new ProjetoStandard(_workspace);

            _projStandard.Codigo = 999;
            _projStandard.CodigoProjetoStandard = "1";
            _projStandard.CodigoTI = 10;
            _projStandard.CodigoTipoProjeto = 1;
            _projStandard.Empresa = "teste";
            _projStandard.Nome = "teste";

            _projStandard.Save();

        }

        [TestMethod]
        public void mustLoadAModel()
        {
            ProjetoStandard _projStandard = new ProjetoStandard(_workspace);
            mustSaveAModel();
            _projStandard.Load(999);

            Assert.AreEqual(_projStandard.Codigo,999);
            Assert.AreEqual(_projStandard.CodigoProjetoStandard,"1");
            Assert.AreEqual(_projStandard.CodigoTI,10);
            Assert.AreEqual(_projStandard.CodigoTipoProjeto,1);
            Assert.AreEqual(_projStandard.Empresa,"teste");
            Assert.AreEqual(_projStandard.Nome,"teste");

            _projStandard.Delete();
        
        }

        [TestMethod]
        public void mustSearchSomeFeatures()
        {
            ProjetoStandard _projStandard = new ProjetoStandard(_workspace);
            mustSaveAModel();

            var _listaProjetos = _projStandard.Search("ProjetoStandard.Empresa = 'teste' and ProjetoStandard.CodigoTI = 10");
            
            Assert.IsTrue(_listaProjetos.Count > 0);
            _projStandard.Load(999);
            _projStandard.Delete();
        }

        [TestMethod]
        public void mustUpdateAModel()
        {
            ProjetoStandard _projStandard = new ProjetoStandard(_workspace);
            mustSaveAModel();

            _projStandard.Load(999);

            _projStandard.CodigoProjetoStandard = "1";
            _projStandard.CodigoTI = 10;
            _projStandard.Update();

            _projStandard.Load(999);

            Assert.AreEqual(_projStandard.CodigoProjetoStandard, "1");
            Assert.AreEqual(_projStandard.CodigoTI, 10);
            _projStandard.Delete();
        }

        [TestMethod]       
        public void mustDeleteAModel()
        {
            ProjetoStandard _projStandard = new ProjetoStandard(_workspace);
            mustSaveAModel();

            _projStandard.Load(999);
            _projStandard.Delete();

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
