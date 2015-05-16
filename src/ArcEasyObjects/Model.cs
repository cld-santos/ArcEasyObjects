using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public abstract class Model
    {
        IPersistencia _persistencia;
        ArcEasyObjects.FeatureAEO _featureAEO;
        string _nomeFeatureClass;

        protected Model()
        {
            _featureAEO = new ArcEasyObjects.FeatureAEO(this);
            _nomeFeatureClass = _featureAEO.obterNomeFeatureClass();
        }



        public void setMetodoDePersistencia(IPersistencia PersistenciaObject)
        {
            _persistencia = PersistenciaObject;
        }

        public string NomeFeatureClass { get { return _nomeFeatureClass; } }

        public void Salvar()
        {
            _persistencia.Salvar(this);
        }
    }
}
