using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("NEOSDE.TB_USUARIO_SAP", Type.Table)]
    public class UsuarioSAP : ArcEasyObjects.BaseModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public UsuarioSAP(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyFieldAEO("NU_USUARIOSAP_ID", typeof(Int32), "NEOSDE.SEQ_USUARIO_SAP")]
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        [EntityFieldAEO("CD_USUARIO_ID", typeof(Int32))]
        public Int32 CodigoUsuarioGSE
        {
            get { return _CodigoUsuarioGSE; }
            set { _CodigoUsuarioGSE = value; }
        }

        [EntityFieldAEO("DE_CHAVE", typeof(string))]
        public string Chave
        {
            get { return _Chave; }
            set { _Chave = value; }
        }

        [EntityFieldAEO("NO_NOME", typeof(string))]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        private Int32 _Codigo;
        private Int32 _CodigoUsuarioGSE;
        private string _Chave;
        private string _Nome;

    }
}
