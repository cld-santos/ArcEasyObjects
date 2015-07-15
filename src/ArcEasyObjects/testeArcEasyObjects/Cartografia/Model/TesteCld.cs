using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("NEOSDE.TB_TESTE_CLD", Type.OracleTable)]
    public class TesteCld : ArcEasyObjects.BaseModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public TesteCld(System.Data.OracleClient.OracleConnection Connection) : base(Connection) { }

        [EntityKeyFieldAEO("IDENTIFICADOR", typeof(Int32))]
        public Int32 Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        [EntityFieldAEO("NOME", typeof(String))]
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        private int _Identificador;
        private string _Nome;

    }
}
