using ArcEasyObjects;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using testeArcEasyObjects.Cartografia.Model;

namespace testeArcEasyObjects.Cartografia.Model
{
    [TestClass]
    public class TesteCldTest
    {

        [ClassInitialize]
        public static void Initialization(TestContext context)
        {
            _Connection = _openConnection("Data Source=GSEPROJ;User Id=neogse;Password=coelba;");
        }


        
        public void deveSalvarUmTesteCld(OracleConnection Connection)
        {
            TesteCld _testeCld = new TesteCld(Connection);
            _testeCld.Identificador = 99;
            _testeCld.Nome = "teste claudio";
            _testeCld.Save();
        }

        [TestMethod]
        public void deveCarregarUmTesteCld()
        {
            _Connection.Open();
            deveSalvarUmTesteCld(_Connection);

            TesteCld _testeCld = new TesteCld(_Connection);
            _testeCld.Load(99);

            Assert.IsTrue(_testeCld.Identificador == 99);
            Assert.IsTrue(_testeCld.Nome == "teste claudio");

            _testeCld.Delete();
            _Connection.Close();
        }

        [TestMethod]
        public void deveAtualizarUmTesteCld()
        {
            _Connection.Open();
            deveSalvarUmTesteCld(_Connection); 
            TesteCld _testeCld = new TesteCld(_Connection);
            _testeCld.Load(99);

            _testeCld.Nome = "teste claudio denovo";
            _testeCld.Update();
            Assert.IsTrue(_testeCld.Identificador == 99);
            Assert.IsTrue(_testeCld.Nome == "teste claudio denovo");
            _testeCld.Delete();
            _Connection.Close();
        }

        [TestMethod]
        public void deveBuscarUmTesteCld()
        {
            _Connection.Open();
            deveSalvarUmTesteCld(_Connection);

            TesteCld _testeCld = new TesteCld(_Connection);
            var _testesCld = _testeCld.Search("TesteCld.Nome = 'teste claudio'");

            Assert.IsTrue(_testesCld.Count>0);
            _testeCld.Delete();
            _Connection.Close();
        }

        [TestMethod]
        public void deveDeletarUmTesteCld()
        {
            _Connection.Open();
            deveSalvarUmTesteCld(_Connection);
            TesteCld _testeCld = new TesteCld(_Connection);
            _testeCld.Load(99);
            _testeCld.Delete();

            var _testesCld = _testeCld.Search("TesteCld.Nome = 'teste claudio denovo'");

            Assert.IsTrue(_testesCld.Count <= 0);
            _Connection.Close();
        }
        #region Métodos e Atributos Privados
        private static OracleConnection _Connection;

        private static OracleConnection _openConnection(string ConnectionString)
        {
            return new OracleConnection(ConnectionString);
        }

        #endregion


    }
}
