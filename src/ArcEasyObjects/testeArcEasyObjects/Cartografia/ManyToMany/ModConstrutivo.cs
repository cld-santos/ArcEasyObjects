using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.TB_MODCONSTRUTIVO",Type.GISTable)]
    public class ModConstrutivo : ArcEasyObjects.GISModel
    {

        public enum eAcao
        {
            INSTALACAO = 1,
            DESATIVACAO = 2,
            RELOCACAO = 3,
            INDEFINIDO = -1
        }

        public ModConstrutivo(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyField("NU_MODCONSTRUTIVO_ID", typeof(Int64), "NEOSDE.SEQ_TB_MODCONSTRUTIVO")]
        public Int64 Identificador { get; set; }

        [EntityField("CD_IDENTIFICADOR_SAP", typeof(string))]
        public string CodigoSAP { get; set; }

        [EntityField("NU_INTERNO_SAP", typeof(Int64))]
        public Int64 NumeroInternoSAP { get; set; }

        [EntityField("DE_DESCRICAO", typeof(string))]
        public string Descricao { get; set; }

        [EntityField("DE_ATRIBUTO2", typeof(string))]
        public string Atributo_2 { get; set; }
        [EntityField("DE_ATRIBUTO3", typeof(string))]
        public string Atributo_3 { get; set; }
        [EntityField("DE_ATRIBUTO4", typeof(string))]
        public string Atributo_4 { get; set; }
        [EntityField("DE_ATRIBUTO5", typeof(string))]
        public string Atributo_5 { get; set; }
        [EntityField("DE_ATRIBUTO6", typeof(string))]
        public string Atributo_6 { get; set; }
        [EntityField("FL_PROPOSTO_EXCLUIR", typeof(bool))]
        public bool IndPropExcluir { get; set; }

        [EntityDateTimeField("DT_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [EntityDateTimeField("DT_ATUALIZACAO")]
        public DateTime DataAtualizacao { get; set; }

        [EntityField("CD_ACAO", typeof(Int32))]
        public Int32 IndAcao { get; set; }

        [EntityField("DE_ANOTACAO", typeof(string))]
        public string Anotacao { get; set; }

        [EntityField("NU_OBJREAL_ID", typeof(int))]
        public int IdObjetoReal { get; set; }

        [EntityOneToManyField(typeof(ModConstrutivoComponente), "NU_MODCONSTRUTIVO_ID", typeof(Int32))]
        public IList<ModConstrutivoComponente> Componentes { get; set; }
    }
}