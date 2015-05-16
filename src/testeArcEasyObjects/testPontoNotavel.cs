﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testeArcEasyObjects.Cartografia.Model;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Geodatabase;
using ArcEasyObjects.Persistencia;
using ESRI.ArcGIS.esriSystem;

namespace testeArcEasyObjects
{
    [TestClass]
    public class testPontoNotavel
    {

        [TestMethod]
        public void deveMontarListaDeAtributos()
        {
        }

        [TestMethod]
        public void deveInvocarPropriedadeExistente()
        {
        }

        [TestMethod]
        public void deveSalvarModelo()
        {
            inicializaLicenca();

            PontoNotavel _pn = new PontoNotavel(new FileGeodatabase(abrirWorkspace()));

            _pn.Codigo = 1;
            _pn.Descricao = "Testando a inclusao por uma camada transparente.";
            _pn.Nome = "Teste Inclusao.";

            _pn.Salvar();
            
        }

        private void inicializaLicenca()
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



        public virtual IWorkspace abrirWorkspace()
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.OpenFromFile(@"E:\csantos\src\ArcEasyObjects\data\aeoSample.gdb", 0);
        }

    }
}