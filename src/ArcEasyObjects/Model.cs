using ArcEasyObjects.Attributes;
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
        HashSet<ModelProperty> _modelProperties;

        internal HashSet<ModelProperty> ModelProperties
        {
            get { return _modelProperties; }
        }

        protected Model()
        {
            _featureAEO = new ArcEasyObjects.FeatureAEO(this);
            _nomeFeatureClass = _featureAEO.obterNomeFeatureClass();
            _modelProperties = _featureAEO.obterAtributosFeatureClass();
        }

        protected Model(IPersistencia MetodoDePersistencia) : this()
        {
            _persistencia = MetodoDePersistencia;
        }


        public string NomeFeatureClass { get { return _nomeFeatureClass; } }

        public void Salvar()
        {
            _persistencia.Salvar(this);
        }
    }
}
