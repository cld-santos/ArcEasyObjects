using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.Model
{

    [EntityClass("CLBLPTADMQA.PROJETO", Type.Table)]
    public class LPTProjeto : ArcEasyObjects.BaseModel
    {
        private IWorkspace _workspace;

        public LPTProjeto(IWorkspace Workspace)
            : base(Workspace)
        {
            _workspace = Workspace;
        }

        [EntityField("CD_SIT_PROJ", typeof(Int32))]
        public Int32 IdSitProj { get; set; }
        
        //[EntityField("COR", typeof(string))]
        //public string Cor { get; set; }
        
        [EntityDateTimeField("DT_CADASTRO")]
        public DateTime DataCadastro { get; set; }
        
        //[EntityField("DWG", typeof(string))]
        //public string DWG { get; set; }
        
        [EntityField("ID", typeof(Int32))]
        public Int32 ID { get; set; }
        
        [EntityField("ID_PROJETO", typeof(Int32))]
        public Int32 IdProjeto { get; set; }
        
        [EntityField("MUNICIPIO_ID", typeof(Int32))]
        public Int32 IdMunicipio { get; set; }
        
        [EntityField("NOME_CONTATO", typeof(string))]
        public string NomeContato { get; set; }
        
        [EntityField("NOTA_OBRA", typeof(string))]
        public string NotaObra { get; set; }
        
        [EntityField("NUMERO", typeof(string))]
        public string Numero { get; set; }

        [EntityField("OBS", typeof(string))]
        public string Obs { get; set; }
        
        [EntityField("PENDENCIA_AMBIENTAL", typeof(Int32))]
        public Int32 PendenciaAmbiental { get; set; }
        
        //[EntityField("SSF", typeof(string))]
        //public string SSF { get; set; }
        
        [EntityField("TELEFONE_CONTATO", typeof(string))]
        public string TelefoneContato { get; set; }
        
        [EntityField("TIPO_COMUNIDADE_ID", typeof(Int32))]
        public Int32 IdTipoComunidade { get; set; }
        
        [EntityField("TITULO_LOCALIDADE", typeof(string))]
        public string TituloLocalidade { get; set; }

        //[EntityField("TRILHA", typeof(string))]
        //public string Trilha { get; set; }

        //[EntityOneToManyField(typeof(LPTCliente), "PROJETO_ID", typeof(Int32))]
        //public IList<LPTCliente> ProjetoClientes { get; set; }

    }
}

