using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("NEOSDE.TB_TESTE_CLD", Type.Table)]
    public class TesteCld : ArcEasyObjects.BaseModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public TesteCld(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyField("IDENTIFICADOR", typeof(Int32),"NEOSDE.SEQ_TESTE_CLD")]
        public Int32 Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        [EntityField("NOME", typeof(String))]
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        [EntityDateField("DT_TESTE")]
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        [EntityDateTimeField("DT_TEMPO_TESTE")]
        public DateTime Tempo
        {
            get { return _Tempo; }
            set { _Tempo = value; }
        }

        [EntityField("FL_TESTE",typeof(bool))]
        public bool Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        private int _Identificador;
        private string _Nome;
        private  DateTime _Data;
        private  DateTime _Tempo;
        private bool _Flag;

    }
}
