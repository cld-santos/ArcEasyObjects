﻿using ArcEasyObjects.Attributes;
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

        public void Load(BaseModel BaseModel, int KeyFieldValue, BaseModel.LoadMethod ChooseLoadMethod)
        {

            string _WhereClause = BaseModel.KeyField + "=" + KeyFieldValue;
            string _fields = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _fields += _property.Attribute.FieldName + ",";
            }

            _fields = _fields.Substring(0, _fields.Length - 1);

            ESRI.ArcGIS.Geodatabase.IQueryDef2 queryDef = (ESRI.ArcGIS.Geodatabase.IQueryDef2)((IFeatureWorkspace)_workspace).CreateQueryDef();
            queryDef.Tables = BaseModel.EntityName;
            queryDef.SubFields = _fields;
            queryDef.WhereClause = _WhereClause;
            // queryDef.PostfixClause = postFixClause;
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


        public void Save(BaseModel BaseModel)
        {

            string _dml = "INSERT INTO {0}({1}) VALUES({2})";
            string _fields = "", _values = "";



            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _fields += _property.Attribute.FieldName + ",";

                if (_property.Attribute is EntityKeyFieldAttribute)
                {
                    EntityKeyFieldAttribute _keyField = (EntityKeyFieldAttribute)_property.Attribute;
                    if (String.IsNullOrEmpty(_keyField.Sequence))
                    {
                        _values += _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType) + ",";
                    }
                    else
                    {
                        ICursor cursor = Helper.GDBCursor.obterCursor((IFeatureWorkspace)_workspace, "SYS.DUAL", _keyField.Sequence + ".NEXTVAL", "");
                        IRow row = cursor.NextRow();
                        _values += row.get_Value(0).ToString() + ",";
                        _property.Property.SetValue(BaseModel,
                                                    Convert.ChangeType(row.get_Value(0).ToString(), _property.Attribute.FieldType), null);
                    }
                }
                else if (_property.Attribute is EntityOneToOneFieldAttribute)
                {
                    BaseModel _bm = (BaseModel)_property.Property.GetValue(BaseModel, null);
                    if (_bm != null)
                    {
                        ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                        Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                        _values += _getFormatedValue(_keyValue, _property.Attribute.FieldType) + ",";
                    }
                }
                else if (_property.Attribute is EntityDateFieldAttribute)
                {
                    string _DateValue = Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToShortDateString();

                    _values += "'" + _getFormatedValue(_DateValue, _property.Attribute.FieldType) + "',";
                }
                else if (_property.Attribute is EntityDateTimeFieldAttribute)
                {
                    string _DateValue = Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToShortDateString() + " " + 
                                        Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToLongTimeString();

                    _values += "'" + _getFormatedValue(_DateValue, _property.Attribute.FieldType) + "',";
                }
                else
                {
                    _values += _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType) + ",";
                }

            }

            _fields = _fields.Substring(0,_fields.Length-1);
            _values = _values.Substring(0,_values.Length-1);

            _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName, _fields, _values));

        }

        private string _getFormatedValue(object Value, Type Type)
        {
            if (typeof(String) == Type)
            {
                return "'" + Convert.ToString(Value) + "'";
            }
            else if (typeof(bool) == Type)
            {
                return (bool)Value ? "1" : "0";
            }

            return Convert.ToString(Value);
        }

        public void Delete(BaseModel BaseModel)
        {
            string _dml = "DELETE FROM {0} WHERE {1}";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityKeyFieldAttribute)))
            {
                String _WhereClause = BaseModel.KeyField + "=" + _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType);
                _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName,_WhereClause));
            }
        }

        public void Update(BaseModel BaseModel)
        {
            string _dml = "UPDATE {0} {1} WHERE {2}";
            string _set = "SET {0} = {1} ";
            string _WhereClause ="", _values="";
            object _Value;

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityKeyFieldAttribute)))
            {
                _WhereClause = BaseModel.KeyField + "=" + _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType);
            }

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityKeyFieldAttribute)))
            {
                if (_property.Attribute is EntityOneToOneFieldAttribute)
                {
                    BaseModel _bm = (BaseModel)_property.Property.GetValue(BaseModel, null);
                    if (_bm != null)
                    {
                        ModelProperty _keyProperty = _bm.ModelProperties.Where(x => x.Attribute is EntityKeyFieldAttribute).First<ModelProperty>();
                        Int32 _keyValue = (Int32)_keyProperty.Property.GetValue(_bm, null);

                        _Value = _property.Property.GetValue(BaseModel, null);
                        _values = String.Format(_set, _property.Attribute.FieldName, _getFormatedValue(_keyValue, _property.Attribute.FieldType));
                    }
                }
                else if (_property.Attribute is EntityDateFieldAttribute)
                {
                    string _DateValue = Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToShortDateString();

                    _Value = "'" + _getFormatedValue(_DateValue, _property.Attribute.FieldType) + "'";
                    _values = String.Format(_set, _property.Attribute.FieldName, _Value);

                }
                else if (_property.Attribute is EntityDateTimeFieldAttribute)
                {
                    string _DateValue = Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToShortDateString() + " " +
                                        Convert.ToDateTime(_property.Property.GetValue(BaseModel, null)).ToLongTimeString();

                    _Value = "'" + _getFormatedValue(_DateValue, _property.Attribute.FieldType) + "'";
                    _values = String.Format(_set, _property.Attribute.FieldName, _Value);
                }
                else
                {
                    _Value = _property.Property.GetValue(BaseModel, null);
                    _values = String.Format(_set, _property.Attribute.FieldName, _getFormatedValue(_Value, _property.Attribute.FieldType));
                }

                
                _workspace.ExecuteSQL(String.Format(_dml, BaseModel.EntityName, _values, _WhereClause));

            }



        }

        public List<BaseModel> Search(BaseModel BaseModel, string AOWhereClause, BaseModel.LoadMethod ChooseLoadMethod)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();

            string _fields = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _fields += _property.Attribute.FieldName + ",";
            }

            _fields = _fields.Substring(0, _fields.Length - 1);

            ESRI.ArcGIS.Geodatabase.IQueryDef2 queryDef = (ESRI.ArcGIS.Geodatabase.IQueryDef2)((IFeatureWorkspace)_workspace).CreateQueryDef();
            queryDef.Tables = BaseModel.EntityName;
            queryDef.SubFields = _fields;
            queryDef.WhereClause = AOWhereClause;

            ICursor _rows = queryDef.Evaluate();

            IRow _row;

            while ((_row = _rows.NextRow()) != null)
            {
                object[] _parameters = { _workspace };
                object _model = Activator.CreateInstance(BaseModel.GetType(), _parameters);

                foreach (ModelProperty _property in BaseModel.ModelProperties)
                {
                    _property.Attribute.Load(_workspace, _row, BaseModel, _property, ChooseLoadMethod);
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
