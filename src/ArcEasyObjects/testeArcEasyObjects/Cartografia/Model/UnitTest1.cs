using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ArcEasyObjects;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("NEOSDE.TB_PLANCUSTOS_LOG", BaseModel.Type.Table)]
    public class LogPlanejamentoDeCustos : BaseModel
    {
        public LogPlanejamentoDeCustos(IWorkspace workspace) : base(workspace) { }

        int _Identificador;
        int _ProjetoId;
        string _RespostaSAP = "";
        string _RequisicaoGSE = "";

        [EntityKeyField("nu_plancustos_id", typeof(int), "neosde.seq_plancustos_log")]
        public int Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        [EntityField("nu_projeto_id", typeof(int))]
        public int ProjetoId
        {
            get { return _ProjetoId; }
            set { _ProjetoId = value; }
        }

        [EntityField("de_resposta_sap", typeof(String))]
        public string RespostaSAP
        {
            get { return _RespostaSAP; }
            set { _RespostaSAP = value; }
        }

        [EntityField("de_requisicao_gse", typeof(String))]
        public string RequisicaoGSE
        {
            get { return _RequisicaoGSE; }
            set { _RequisicaoGSE = value; }
        }



    }

}
