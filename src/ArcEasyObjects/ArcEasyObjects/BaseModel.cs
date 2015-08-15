using ArcEasyObjects.Attributes;
using ArcEasyObjects.Persistence;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ArcEasyObjects
{
    public abstract class BaseModel 
    {
        public enum Type { FeatureClass, GISTable, Table, OracleTable };
        public enum LoadMethod { Eager, Lazy };
        public string EntityName { get { return _FeatureClassConfig.EntityName; } }
        public string KeyField { get { return _KeyField; } }
        public HashSet<ModelProperty> ModelProperties { get { return _ModelProperties; } }

        private IDictionary<Type, Func<IPersistence>> _createEntityPersistence = new Dictionary<Type, Func<IPersistence>>();

        public BaseModel()
        {
            EntityAEO _featureAEO = new EntityAEO(this);
            _FeatureClassConfig = _featureAEO.getFeatureClassConfig();
            _ModelProperties = _featureAEO.getFeatureClassFields();
            _ModelAttributes = _featureAEO.getFeatureClassAttributes();
            _KeyField = _featureAEO.getFeatureClassKeyField();
        }
    
        public BaseModel(ESRI.ArcGIS.Geodatabase.IWorkspace Workspace) : this()
        {
            _createEntityPersistence.Add(Type.FeatureClass, () => { return new FeatureClassDAO(Workspace); });
            _createEntityPersistence.Add(Type.GISTable, () => { return new GISTableDAO(Workspace); });
            _createEntityPersistence.Add(Type.Table, () => { return new TableDAO(Workspace); }); 

            _persistence = _createEntityPersistence[_FeatureClassConfig.TypeEntity]();
        }

        public BaseModel(System.Data.OracleClient.OracleConnection Connection) : this()
        {
            _createEntityPersistence.Add(Type.OracleTable, () => { return new OracleTableDAO(Connection); });
            _persistence = _createEntityPersistence[_FeatureClassConfig.TypeEntity]();
        }

        public void Save()
        {
            _persistence.Save(this);
        }
        public void Update()
        {
            _persistence.Update(this);
        }
        public void Load(int KeyFieldValue)
        {
            _persistence.Load(this, KeyFieldValue);
        }
        public void Delete()
        {
            _persistence.Delete(this);
        }

        public List<BaseModel> Search(string AEOWhereClause)
        {

            String AOWhereClause = toAOWhereClause(AEOWhereClause);

            return _persistence.Search(this, AOWhereClause);

        }

        public List<BaseModel> Search(string AEOWhereClause, LoadMethod ChooseLoadMethod)
        {

            String AOWhereClause = toAOWhereClause(AEOWhereClause);

            return _persistence.Search(this, AOWhereClause, ChooseLoadMethod);

        }

        
        private string toAOWhereClause(string AEOWhereClause)
        {

            foreach (string _item in _ModelAttributes.Keys)
            {
                Regex padrao = new Regex(@"\b"+_item+@"\b");
                AEOWhereClause = padrao.Replace(AEOWhereClause, _ModelAttributes[_item]);
            }

            return AEOWhereClause;

        }


        private IPersistence _persistence;
        private HashSet<ModelProperty> _ModelProperties;
        private IDictionary<string, string> _ModelAttributes;
        private string _KeyField;
        private EntityClassAttribute _FeatureClassConfig;

    }
}
