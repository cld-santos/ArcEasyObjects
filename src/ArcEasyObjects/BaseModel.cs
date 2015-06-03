using ArcEasyObjects.Attributes;
using ArcEasyObjects.Persistencia;
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
        public enum Type { FeatureClass, Table };

        public string EntityName { get { return _FeatureClassConfig.EntityName; } }
        public string KeyField { get { return _KeyField; } }
        internal HashSet<ModelProperty> ModelProperties { get { return _ModelProperties; } }

        private IDictionary<Type, Func<IPersistence>> _createEntityPersistence = new Dictionary<Type, Func<IPersistence>>();

        protected BaseModel()
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
            _createEntityPersistence.Add(Type.Table, () => { return new TableDAO(Workspace); }); 

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

        
        [EntityFieldAEO("OBJECTID", typeof(Int32))]
        public Int32 ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        private int _ObjectId;


        private string toAOWhereClause(string AEOWhereClause)
        {
            foreach (string _item in _ModelAttributes.Keys)
            {

                AEOWhereClause = AEOWhereClause.Replace(_item, _ModelAttributes[_item]);
            }

            return AEOWhereClause;

        }


        private IPersistence _persistence;
        private string _FeatureClassName;
        private HashSet<ModelProperty> _ModelProperties;
        private IDictionary<string, string> _ModelAttributes;
        private string _KeyField;
        private EntityAEOAttribute _FeatureClassConfig;

    }
}
