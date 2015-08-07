using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("TB_AVALIACAO_PN",  Type.GISTable)]
    public class Avaliacao : ArcEasyObjects.GISModel
    {
        public Avaliacao(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyField("CD_AVALIACAOPN",typeof(Int32))]
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        [EntityField("DE_NOME",typeof(string))]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        [EntityField("DE_COMENTARIO", typeof(string))]
        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        [EntityOneToOneField(typeof(PontoNotavel),"CD_PN", typeof(string))]
        public PontoNotavel PontoNotavelAvaliado
        {
            get { return _PontoNotavelAvaliado; }
            set { _PontoNotavelAvaliado = value; }
        }

        private Int32 _Codigo;
        private string _Nome;
        private string _Comentario;
        private PontoNotavel _PontoNotavelAvaliado;
    }
}
