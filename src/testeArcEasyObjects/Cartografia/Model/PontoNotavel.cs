using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("PT_PONTO_NOTAVEL", Type.FeatureClass)]
    public class PontoNotavel : ArcEasyObjects.GISModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public PontoNotavel(IWorkspace Workspace) : base(Workspace) { }
    
        [EntityKeyFieldAEO("CD_PN", typeof(Int32))]
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        
        [EntityFieldAEO("NOME", typeof(String))]
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        [EntityFieldAEO("DESCRICAO", typeof(String))]
        public String Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private Int32 _Codigo;
        private String _Nome;
        private String _Descricao;


    }
}
