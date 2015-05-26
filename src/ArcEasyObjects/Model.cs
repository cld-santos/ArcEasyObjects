using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public abstract class Model
    {
        IPersistence _persistence;
        ArcEasyObjects.FeatureAEO _featureAEO;
        string _FeatureClassName;
        HashSet<ModelProperty> _modelProperties;

        internal HashSet<ModelProperty> ModelProperties
        {
            get { return _modelProperties; }
        }

        protected Model()
        {
            _featureAEO = new ArcEasyObjects.FeatureAEO(this);
            _FeatureClassName = _featureAEO.getFeatureClassName();
            _modelProperties = _featureAEO.getFeatureClassFields();
        }

        protected Model(IPersistence MetodoDePersistencia) : this()
        {
            _persistence = MetodoDePersistencia;
        }


        public string NomeFeatureClass { get { return _FeatureClassName; } }

        public void Save()
        {
            _persistence.Save(this);
        }

        public void Load(int KeyFieldValue)
        {
            _persistence.Load(this, KeyFieldValue);
        }

    }
}
