using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("TB_EXTRA_INFO",  Type.Table)]
    public class InformacaoExtra : ArcEasyObjects.BaseModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public InformacaoExtra(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyFieldAEO("CD_EXTRAINFO", typeof(Int32))]
        public Int32 CodigoInformacaoExtra
        {
            get { return _CodigoInformacaoExtra; }
            set { _CodigoInformacaoExtra = value; }
        }

        [EntityFieldAEO("CD_PN", typeof(Int32))]
        public Int32 CodigoPontoNotavel
        {
            get { return _CodigoPontoNotavel; }
            set { _CodigoPontoNotavel = value; }
        }

        [EntityFieldAEO("DE_INFORMACOES", typeof(String))]
        public String Informacoes
        {
            get { return _Informacoes; }
            set { _Informacoes = value; }
        }

        private Int32 _CodigoInformacaoExtra;
        private Int32 _CodigoPontoNotavel;
        private String _Informacoes;

    }
}
