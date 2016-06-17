using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("NEOSDE.TB_INTERFACE_PLAN_CUSTOS", Type.Table)]
    public class InterfacePlanejamentoCusto : ArcEasyObjects.BaseModel
    {
        private IWorkspace _workspace;

        public InterfacePlanejamentoCusto(IWorkspace Workspace)
            : base(Workspace)
        {
            _workspace = Workspace;
        }

        [EntityKeyField("CD_CHAVE", typeof(string))]
        public string Chave { get; set; }

        [EntityField("NU_PROJETO_SAP", typeof(string))]
        public string ProjetoSAP { get; set; }

        [EntityDateTimeField("DT_INICIO", typeof(DateTime))]
        public DateTime DataInicio { get; set; }

        [EntityDateField("DT_FIM", typeof(DateTime))]
        public DateTime DataFim { get; set; }

        [EntityField("CD_STATUS", typeof(Int32))]
        public Int32 Status { get; set; }

        [EntityField("NU_VALOR_ORCAMENTO", typeof(Decimal))]
        public decimal ValorOrcamento { get; set; }
    }
}