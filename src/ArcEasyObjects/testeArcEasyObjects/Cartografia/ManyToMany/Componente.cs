using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.TB_COMPONENTE", Type.GISTable)]
    public class Componente : ArcEasyObjects.BaseModel
    {

        public Componente(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyField("NU_COMPONENTE_ID",typeof(Int64),"NEOSDE.SEQ_TB_COMPONENTE")]
        public Int64 Identificador { get; set; }

        [EntityField("CD_IDENTIFICADOR_SAP", typeof(string))]
        public string CodigoSAP { get; set; }
        [EntityField("DE_DESCRICAO", typeof(string))]
        public string Descricao { get; set; }
        [EntityField("DE_CATEGORIA", typeof(string))]
        public string Categoria { get; set; }
        [EntityField("NU_VALOR_UNITARIO", typeof(double))]
        public double ValorUnitario { get; set; }
        [EntityField("DE_UNIDADE_MEDIDA", typeof(string))]
        public string UnidadeMedida { get; set; }
        [EntityField("FL_UAR", typeof(bool))]
        public bool IndUAR { get; set; }
        //public ModConstrutivo.eAcao IndAcao { get; set; }
        [EntityField("CD_ACAO", typeof(Int16))]
        public Int16 IndAcao { get; set; }
        [EntityField("QT_PESO_LIQUIDO", typeof(double))]
        public double PesoLiquido { get; set; }
        [EntityField("QT_PESO_BRUTO", typeof(double))]
        public double PesoBruto { get; set; }

    }
}