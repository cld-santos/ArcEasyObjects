using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects
{
    public abstract class Model
    {

        public string NomeFeatureClass { get { return _FeatureClassName; } }
        public string KeyField { get { return _KeyField; } }
        internal HashSet<ModelProperty> ModelProperties { get { return _ModelProperties; } }

        protected Model()
        {
            //TODO: Ter uma factory ou builder para construir o Model
            _featureAEO = new ArcEasyObjects.FeatureAEO(this);
            _FeatureClassName = _featureAEO.getFeatureClassName();
            _ModelProperties = _featureAEO.getFeatureClassFields();
            _ModelAttributes = _featureAEO.getFeatureClassAttributes();
            _KeyField = _featureAEO.getFeatureClassKeyField();
        }

        protected Model(IPersistence MetodoDePersistencia) : this()
        {
            _persistence = MetodoDePersistencia;
        }



        public void Save()
        {
            _persistence.Save(this);
        }

        public void Load(int KeyFieldValue)
        {
            _persistence.Load(this, KeyFieldValue);
        }

        public List<Model> Search(string AEOWhereClause)
        {
            String AOWhereClause = toAOWhereClause(AEOWhereClause);

            return _persistence.Search(this, AOWhereClause);
            
        }

        private string toAOWhereClause(string AEOWhereClause)
        {
            foreach (string _item in _ModelAttributes.Keys)
            {
                AEOWhereClause = AEOWhereClause.Replace(_item, _ModelAttributes[_item]);
            }

            return AEOWhereClause;

        }


        private IPersistence _persistence;
        private ArcEasyObjects.FeatureAEO _featureAEO;

        private string _FeatureClassName;
        private HashSet<ModelProperty> _ModelProperties;
        private IDictionary<string, string> _ModelAttributes;
        private string _KeyField;

    }
}
