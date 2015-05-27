using ArcEasyObjects;
using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeArcEasyObjects.Cartografia.Model
{
    [FeatureClassAEO("PT_PONTO_NOTAVEL")]
    public class PontoNotavel : ArcEasyObjects.Model
    {
        public PontoNotavel(IPersistence MetodoDePersistencia) : base(MetodoDePersistencia) { }

        [FeatureClassKeyFieldAEO("CD_PN", typeof(Int32))]
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        
        [FeatureClassFieldAEO("NOME", typeof(String))]
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        [FeatureClassFieldAEO("DESCRICAO", typeof(String))]
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
