using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testeArcEasyObjects.Cartografia.ManyToMany;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.Collections.Generic;

namespace testeArcEasyObjects.Tests
{
    [TestClass]
    public class ManyToManyTest
    {
        [TestMethod]
        public void mustLoadAModel()
        {
            Favorito _Favorito = new Favorito(_workspace);
            _Favorito.CodigoEmpreiteira = 9;
            _Favorito.CodigoUsuario = 7;
            _Favorito.ehPonto = false;
            _Favorito.Nome = "Incluído pelo ArcEasy";

            ModConstrutivo _modConstrutivo = new ModConstrutivo(_workspace);

            var _listModConstrutivo = _modConstrutivo.Search("ModConstrutivo.Identificador in (544,285,284,283)");

            _Favorito.ModulosConstrutivos = new List<ModConstrutivo>();

            foreach (ModConstrutivo _item in _listModConstrutivo)
            {
                _Favorito.ModulosConstrutivos.Add(_item);
            }

            _Favorito.Save();
            Favorito _FavoritoLoaded = new Favorito(_workspace);
            _Favorito.Load(_Favorito.Identificador);

            Assert.IsNotNull(_Favorito.ModulosConstrutivos);

        }

        [TestMethod]
        public void mustSearchAModel()
        {
            Favorito _Favorito = new Favorito(_workspace);
            var _listFavorito = _Favorito.Search("Favorito.CodigoUsuario = 7");

            Assert.IsNotNull(_listFavorito);


            foreach (Favorito _item in _listFavorito)
            {
                Assert.IsNotNull(_item.ModulosConstrutivos);
            }
        }

        [TestMethod]
        public void mustSaveAModel()
        {
            Favorito _Favorito = new Favorito(_workspace);
            _Favorito.CodigoEmpreiteira = 9;
            _Favorito.CodigoUsuario = 7;
            _Favorito.ehPonto = false;
            _Favorito.Nome = "Incluído pelo ArcEasy";

            ModConstrutivo _modConstrutivo = new ModConstrutivo(_workspace);

            var _listModConstrutivo = _modConstrutivo.Search("ModConstrutivo.Identificador in (544,285,284,283)");

            _Favorito.ModulosConstrutivos = new List<ModConstrutivo>();

            foreach (ModConstrutivo _item in _listModConstrutivo)
            {
                _Favorito.ModulosConstrutivos.Add(_item);
            }


            Componente _Componente = new Componente(_workspace);
            _Componente.Load(511);
            FavoritoComponente _favComponente = new FavoritoComponente(_workspace);
            _favComponente.Componente = _Componente;
            _favComponente.IdentificadorFavorito = _Favorito.Identificador;
            _favComponente.Quantidade = 10;
            _favComponente.Valor = 150.50;
            _favComponente.Save();
            _Favorito.ComponentesFavoritos = new List<FavoritoComponente>();
            _Favorito.ComponentesFavoritos.Add(_favComponente);


            _Favorito.Save();

        }

        [TestMethod]
        public void mustUpdateAModel()
        {
            Favorito _Favorito = new Favorito(_workspace);
            _Favorito.CodigoEmpreiteira = 9;
            _Favorito.CodigoUsuario = 7;
            _Favorito.ehPonto = false;
            _Favorito.Nome = "Incluído pelo ArcEasy";

            ModConstrutivo _modConstrutivo = new ModConstrutivo(_workspace);

            var _listModConstrutivo = _modConstrutivo.Search("ModConstrutivo.Identificador in (544,285,284,283)");

            _Favorito.ModulosConstrutivos = new List<ModConstrutivo>();

            foreach (ModConstrutivo _item in _listModConstrutivo)
            {
                _Favorito.ModulosConstrutivos.Add(_item);
            }

            _Favorito.Save();

            _Favorito.Load(_Favorito.Identificador);

            _listModConstrutivo = _modConstrutivo.Search("ModConstrutivo.Identificador in (256, 258, 259)");

            foreach (ModConstrutivo _item in _listModConstrutivo)
            {
                _Favorito.ModulosConstrutivos.Add(_item);
            }

            _Favorito.Update();

        }

        [TestMethod]
        public void mustDeleteAModel()
        {
            Favorito _Favorito = new Favorito(_workspace);
            _Favorito.CodigoEmpreiteira = 9;
            _Favorito.CodigoUsuario = 7;
            _Favorito.ehPonto = false;
            _Favorito.Nome = "Incluído pelo ArcEasy";

            ModConstrutivo _modConstrutivo = new ModConstrutivo(_workspace);

            var _listModConstrutivo = _modConstrutivo.Search("ModConstrutivo.Identificador in (544,285,284,283)");

            _Favorito.ModulosConstrutivos = new List<ModConstrutivo>();

            foreach (ModConstrutivo _item in _listModConstrutivo)
            {
                _Favorito.ModulosConstrutivos.Add(_item);
            }

            _Favorito.Save();

            int chave = _Favorito.Identificador;
            _Favorito.Delete();


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
