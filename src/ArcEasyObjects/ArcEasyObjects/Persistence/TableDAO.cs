using ArcEasyObjects.Attributes;
using ArcEasyObjects.Helper;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects.Persistence
{

    public class TableDAO : IPersistence
    {
        
        public TableDAO(IWorkspace Workspace)
        {
            _workspace = Workspace;
        }


        public void Save(BaseModel BaseModel)
        {
            string _dml = "INSERT INTO {0}({1}) VALUES({2})";
            string _fields = "", _values = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityManyToManyFieldAttribute) && !(x.Attribute is EntityOneToManyFieldAttribute)))
            {
                _fields += _property.Attribute.FieldName + ",";
                _values += _property.Attribute.Save(_workspace,BaseModel,_property) + ",";
            }

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityManyToManyFieldAttribute)))
            {
                _property.Attribute.Save(_workspace, BaseModel, _property);
            }

            _fields = _fields.Substring(0,_fields.Length-1);
            _values = _values.Substring(0,_values.Length-1);

            _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName, _fields, _values));

        }



        public void Delete(BaseModel BaseModel)
        {
            string _dml = "DELETE FROM {0} WHERE {1}";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityKeyFieldAttribute) && !(x.Attribute is EntityManyToManyFieldAttribute)))
            {
                String _WhereClause = BaseModel.KeyField + "=" + FieldFormatHelper.FormatField(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType);
                _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName,_WhereClause));
            }

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityManyToManyFieldAttribute)))
            {
                ((EntityManyToManyFieldAttribute)_property.Attribute).Delete(_workspace, BaseModel, _property);
            }

        }

        public void Delete(BaseModel BaseModel, string AEOWhereClause)
        {
            if (String.IsNullOrEmpty(AEOWhereClause)) return;
            string _dml = "DELETE FROM {0} WHERE {1}";
            _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName, AEOWhereClause));

        }


        public void Update(BaseModel BaseModel)
        {
            string _dml = "UPDATE {0} {1} WHERE {2}";
            string _set = "SET {0} = {1} ";
            string _WhereClause ="", _values="";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityKeyFieldAttribute)))
            {
                _WhereClause = BaseModel.KeyField + "=" + FieldFormatHelper.FormatField(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType);
            }

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityKeyFieldAttribute) && !(x.Attribute is EntityManyToManyFieldAttribute) && !(x.Attribute is EntityOneToManyFieldAttribute)))
            {
                _values = String.Format(_set, _property.Attribute.FieldName, _property.Attribute.Save(_workspace, BaseModel,_property));
                _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName, _values, _WhereClause));
            }

        }

        public void Load(BaseModel BaseModel, int KeyFieldValue, BaseModel.LoadMethod ChooseLoadMethod)
        {

            string _WhereClause = BaseModel.KeyField + "=" + KeyFieldValue;
            string _fields = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x=> !(x.Attribute is EntityManyToManyFieldAttribute)))
            {
                _fields += _property.Attribute.FieldName + ",";
            }

            _fields = _fields.Substring(0, _fields.Length - 1);

            ESRI.ArcGIS.Geodatabase.IQueryDef2 queryDef = (ESRI.ArcGIS.Geodatabase.IQueryDef2)((IFeatureWorkspace)_workspace).CreateQueryDef();
            queryDef.Tables = BaseModel.EntityName;
            queryDef.SubFields = _fields;
            queryDef.WhereClause = _WhereClause;
            ICursor _rows = queryDef.Evaluate();


            IRow _row = _rows.NextRow();
            if (_row != null)
            {
                foreach (ModelProperty _property in BaseModel.ModelProperties)
                {
                    _property.Attribute.Load(_workspace, _row, BaseModel, _property, ChooseLoadMethod);
                }
            }
        }



        public List<BaseModel> Search(BaseModel BaseModel, string AOWhereClause, BaseModel.LoadMethod ChooseLoadMethod)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();

            string _fields = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityManyToManyFieldAttribute)))
            {
                _fields += _property.Attribute.FieldName + ",";
            }

            _fields = _fields.Substring(0, _fields.Length - 1);

            ESRI.ArcGIS.Geodatabase.ITable tbProjeto = ((IFeatureWorkspace)_workspace).OpenTable(BaseModel.EntityName);
            ICursor _rows = GDBCursor.obterCursor(tbProjeto, _fields, AOWhereClause,"");

            IRow _row;

            while ((_row = _rows.NextRow()) != null)
            {
                object[] _parameters = { _workspace };
                BaseModel _model = (BaseModel)Activator.CreateInstance(BaseModel.GetType(), _parameters);
                
                foreach (ModelProperty _property in BaseModel.ModelProperties)
                {
                    _property.Attribute.Load(_workspace, _row, (BaseModel)_model, _property, ChooseLoadMethod);
                }

                _ModelsReturn.Add((BaseModel)_model);
            }

            return _ModelsReturn;
        }

        private IWorkspace _workspace;



        public void Load(BaseModel AEOModel, int KeyFieldValue)
        {
            this.Load(AEOModel, KeyFieldValue, BaseModel.LoadMethod.Eager);
        }

        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause)
        {
            return this.Search(AEOModel, AOWhereClause, BaseModel.LoadMethod.Eager);
        }

    }
}
