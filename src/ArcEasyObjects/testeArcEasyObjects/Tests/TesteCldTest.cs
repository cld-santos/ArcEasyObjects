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
            _testeCld.Valor = 15.55M;
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
            _testeCld.Valor = 15.55M;
            _testeCld.Update();

            Assert.IsTrue(_testeCld.Nome == "teste claudio denovo");
            Assert.IsTrue(_testeCld.Data == DateTime.Now.Date);
            _testeCld.Delete();
        }

        [TestMethod]
        public void deveSalvarUmaDataNula()
        {
            TesteCld _testeCld = new TesteCld(_workspace);

            _testeCld.Nome = "teste claudio denovo";
            _testeCld.Data = null;
            _testeCld.Tempo = null;
            _testeCld.Flag = true;
            _testeCld.Valor = 15.55M;
            _testeCld.Save();

            TesteCld _testeCld2 = new TesteCld(_workspace);
            _testeCld2.Load(_testeCld.Identificador);
            Assert.IsTrue(_testeCld2.Nome == "teste claudio denovo");
            Assert.IsTrue(_testeCld2.Tempo == null);
            Assert.IsTrue(_testeCld2.Data == null);
            //_testeCld.Delete();

        }

        [TestMethod]
        public void deveBuscarUmTesteCld()
        {
            TesteCld _testeCld = deveSalvarUmTesteCld();
            
            var _testesCld = _testeCld.Search("TesteCld.Nome = 'teste claudio'");

            Assert.IsTrue(_testesCld.Count > 0);
            _testeCld.Delete();



        }

        [TestMethod]
        public void testAtualizaSolicitacaoIntervencao()
        {
            SolicitacaoIntervencaoOP si = new SolicitacaoIntervencaoOP(_workspace);
            si.De_telefone_responsavel = "testes";
            si.Nu_bairro_id = 1;
            si.Nu_usuario = 7;
            si.De_nome_responsavel = "testes";
            si.De_comentario = "testes";
            //si.Nu_solic_intervencao_op_id = sera gerado automaticamente pela sequence
            si.Dt_solicitacao = DateTime.Now;
            si.Nu_regional_id = 1;
            si.Cd_codigo_area = 2;
            si.Nu_tipo_natureza_op_id = 2;
            si.Nu_tipo_solic_interv_op_id = 2;
            si.Nu_motivo_solic_interv_op_id = 2;
            si.De_desenho = "1111";
            si.Dt_inicio_previsto = Convert.ToDateTime("29/08/1982");
            si.Dt_termino_previsto = Convert.ToDateTime("29/08/1982");
            si.De_endereco = "testes";
            si.Nu_empresa_id = 1;
            si.Cd_tipo_desligam_solic_interv = 1;
            si.De_referencia = "1";
            si.Cd_status_solic_intervencao = 1;
            si.Cd_cancelamento_solic_interv = 0;
            //si.Nu_instalacao_mt_id = xxx
            si.Nu_empreiteira_id = 1;
            si.Nu_ocorrencia_op_id = 0;
            si.Save();
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
