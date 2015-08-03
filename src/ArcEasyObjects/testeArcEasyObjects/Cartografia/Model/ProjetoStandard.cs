using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("NEOSDE.DOM_PROJETO_STANDARD", Type.Table)]
    public class ProjetoStandard : ArcEasyObjects.BaseModel
    {
        public ProjetoStandard() : base() { }
        //TODO: Remover dependencia explicita da classe pai
        public ProjetoStandard(IWorkspace Workspace) : base(Workspace) { }

        //NU_PROJETO_STANDARD_ID NUMBER(3) NOT NULL PRIMARY KEY,
        [EntityKeyFieldAEO("NU_PROJETO_STANDARD_ID",typeof(Int16))]
        public Int16 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        //CD_TI NUMBER(3) NOT NULL,
        [EntityFieldAEO("CD_TI", typeof(Int16))]
        public Int16 CodigoTI
        {
            get { return _CodigoTI; }
            set { _CodigoTI = value; }
        }

        //NO_NOME VARCHAR(50) NOT NULL,
        [EntityFieldAEO("NO_NOME", typeof(string))]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        //CD_TIPO_PROJETO NUMBER(3) NOT NULL,
        [EntityFieldAEO("CD_TIPO_PROJETO", typeof(Int16))]
        public Int16 CodigoTipoProjeto
        {
            get { return _CodigoTipoProjeto; }
            set { _CodigoTipoProjeto = value; }
        }

        //NO_EMPRESA VARCHAR(10) NOT NULL, 
        [EntityFieldAEO("NO_EMPRESA", typeof(string))]
        public string Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }

        //NO_PROJETO_STANDARD VARCHAR(10) NOT NULL
        [EntityFieldAEO("NO_PROJETO_STANDARD", typeof(string))]
        public string CodigoProjetoStandard
        {
            get { return _CodigoProjetoStandard; }
            set { _CodigoProjetoStandard = value; }
        }

        private Int16 _Codigo;
        private Int16 _CodigoTI;
        private string _Nome;
        private Int16 _CodigoTipoProjeto;
        private string _Empresa;
        private string _CodigoProjetoStandard;

    }
}
