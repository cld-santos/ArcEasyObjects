using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testeArcEasyObjects.Cartografia.ManyToMany;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.Linq;

namespace testeArcEasyObjects.Tests
{
    [TestClass]
    public class OneToManyTest
    {
        [TestMethod]
        public void mustEagerLoadAModel()
        {
            ModConstrutivo _modConstrutivo = new ModConstrutivo(_workspace);
            _modConstrutivo.Load(544);
            _modConstrutivo.Load(246);
            _modConstrutivo.Load(247);
            _modConstrutivo.Load(249);
            _modConstrutivo.Load(250);
            _modConstrutivo.Load(251);
            _modConstrutivo.Load(252);
            _modConstrutivo.Load(255);
            _modConstrutivo.Load(256);
            _modConstrutivo.Load(258);
            _modConstrutivo.Load(259);
            _modConstrutivo.Load(261);
            _modConstrutivo.Load(262);
            _modConstrutivo.Load(264);
            _modConstrutivo.Load(265);
            _modConstrutivo.Load(267);
            _modConstrutivo.Load(268);
            _modConstrutivo.Load(269);
            _modConstrutivo.Load(270);
            _modConstrutivo.Load(271);
            _modConstrutivo.Load(273);
            _modConstrutivo.Load(274);
            _modConstrutivo.Load(276);
            _modConstrutivo.Load(277);
            _modConstrutivo.Load(279);
            _modConstrutivo.Load(280);
            _modConstrutivo.Load(282);
            _modConstrutivo.Load(283);
            _modConstrutivo.Load(285);
            _modConstrutivo.Load(286);
            _modConstrutivo.Load(288);
            _modConstrutivo.Load(289);
            _modConstrutivo.Load(290);
            _modConstrutivo.Load(291);   
            Assert.IsTrue(_modConstrutivo.Componentes.Count > 0);
            Assert.IsNull(_modConstrutivo.Componentes[0].objComponente);
            Assert.IsNull(_modConstrutivo.Componentes[1].objComponente);
            Assert.IsNull(_modConstrutivo.Componentes[2].objComponente);
        }


        [TestMethod]
        public void mustSearchAllComponentes()
        {
            Componente componente = new Componente(_workspace);
            var lista =  componente.Search(String.Format("Componente.IndUAR = {0}", 1),ArcEasyObjects.BaseModel.LoadMethod.Lazy);
            System.Console.WriteLine(lista.Count);
        }

        [TestMethod]
        public void mustSearchAllModConstrutivo()
        {
            ModConstrutivo _ModConstrutivo = new ModConstrutivo(_workspace);
            var lista = _ModConstrutivo.Search("", ArcEasyObjects.BaseModel.LoadMethod.Lazy).Cast<ModConstrutivo>().ToList<ModConstrutivo>();
            System.Console.WriteLine(lista.Count);

            var _ModConstrutivosLINQ = from item in lista where
                                (int)item.IndAcao == 3 && item.IdObjetoReal == 16 && item.Atributo_2.Contains("")
                                && item.Atributo_3.Contains("") && item.Atributo_4.Contains("")
                                && !item.IndPropExcluir && item.CodigoSAP.Trim().EndsWith("C")
                                        select new { item.IndAcao, item.CodigoSAP, item.Descricao };
            ;
            System.Console.WriteLine(_ModConstrutivosLINQ.Count());

        }

        
        [TestMethod]
        public void mustSearchAllModConstrutivoWithDate()
        {
            ModConstrutivo _ModConstrutivo = new ModConstrutivo(_workspace);
            //_modConstrutivoDAO = (ModConstrutivoDAOImpl)TableFactory.getInstance((typeof(IModConstrutivoDAO)), _workspaceProjeto);
            ModConstrutivo modConstrutivo = new ModConstrutivo(_workspace);
            string sql = "ModConstrutivo.DataAtualizacao >= TO_TIMESTAMP('{0}','DD/MM/YYYY')";
            sql = string.Format(sql, DateTime.Now.AddMonths(-5).ToShortDateString());
            var _listModAtualizado = modConstrutivo.Search(sql);
            System.Console.WriteLine(_listModAtualizado.Count);


        }

        [TestMethod]
        public void mustSearchAllFavoritsFromAUser()
        {
            Favorito _favorito = new Favorito(_workspace);
            var _Favoritos = _favorito.Search("Favorito.CodigoUsuario = " + 7, ArcEasyObjects.BaseModel.LoadMethod.Lazy);
            Assert.IsTrue(_Favoritos.Count > 0);
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
