using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcEasyObjects.Persistence
{

    public class OracleTableDAO : IPersistence
    {

        public OracleTableDAO(OracleConnection Connection)
        {
            _Connection = Connection;
        }

        public void Load(BaseModel BaseModel, int KeyFieldValue)
        {

            string _dml = "SELECT * FROM {0} WHERE {1}";
            string _WhereClause = BaseModel.KeyField + "=" + KeyFieldValue;

            OracleCommand _Command = new OracleCommand(String.Format(_dml,BaseModel.EntityName,_WhereClause));
            _Command.Connection = _Connection;
            
            OracleDataReader _Reader = _Command.ExecuteReader();
            if (_Reader.Read())
            {
                foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Property.SetValue(BaseModel,
                                                Convert.ChangeType(_Reader.GetValue(_Reader.GetOrdinal(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType), null);
                }
            }

        }

        public void Save(BaseModel BaseModel)
        {
            OracleCommand _Command;
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
                        _Command = new OracleCommand(String.Format("SELECT {0} FROM SYS.DUAL", _keyField.Sequence + ".NEXTVAL"));
                        _Command.Connection = _Connection;
                        OracleDataReader _Reader = _Command.ExecuteReader();
                        if (_Reader.Read())
                        {
                            _values += _Reader.GetString(0) + ",";
                        }
                    }

                }
                else
                {
                    _values += _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType) + ",";
                }

            }

            _fields = _fields.Substring(0,_fields.Length-1);
            _values = _values.Substring(0,_values.Length-1);

            _Command = new OracleCommand(String.Format(_dml, BaseModel.EntityName, _fields, _values));
            _Command.Connection = _Connection;
            try
            {
                _Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Delete(BaseModel BaseModel)
        {
            OracleCommand _Command;
            string _dml = "DELETE FROM {0} WHERE {1}";

            foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => (x.Attribute is EntityKeyFieldAttribute)))
            {
                String _WhereClause = BaseModel.KeyField + "=" + _getFormatedValue(_property.Property.GetValue(BaseModel, null), _property.Attribute.FieldType);

                _Command = new OracleCommand(String.Format(_dml, BaseModel.EntityName, _WhereClause));
                _Command.Connection = _Connection;
                
                _Command.ExecuteNonQuery();
            }
        }

        public void Update(BaseModel BaseModel)
        {
            OracleCommand _Command;
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
                _Value = _property.Property.GetValue(BaseModel, null);
                _values = String.Format(_set,_property.Attribute.FieldName,_getFormatedValue(_Value, _property.Attribute.FieldType));

                _Command = new OracleCommand(String.Format(_dml, BaseModel.EntityName, _values, _WhereClause));
                _Command.Connection = _Connection;
                
                _Command.ExecuteNonQuery();
            }



        }

        public List<BaseModel> Search(BaseModel BaseModel, string AOWhereClause)
        {
            List<BaseModel> _ModelsReturn = new List<BaseModel>();
            string _fields = "";

            foreach (ModelProperty _property in BaseModel.ModelProperties)
            {
                _fields += _property.Attribute.FieldName + ",";
            }

            _fields = _fields.Substring(0, _fields.Length - 1);

            OracleCommand _Command = new OracleCommand(String.Format("SELECT * FROM {0} WHERE {1}", BaseModel.EntityName, AOWhereClause));
            _Command.Connection = _Connection;
            

            OracleDataReader _Reader = _Command.ExecuteReader();
            while (_Reader.Read())
            {
                object[] _parameters = { _Connection };
                object _model = Activator.CreateInstance(BaseModel.GetType(), _parameters);

                foreach (ModelProperty _property in BaseModel.ModelProperties.Where(x => !(x.Attribute is EntityShapeFieldAttribute)))
                {
                    _property.Property.SetValue(_model,
                                                Convert.ChangeType(_Reader.GetValue(_Reader.GetOrdinal(_property.Attribute.FieldName)),
                                                                   _property.Attribute.FieldType), null);
                }

                _ModelsReturn.Add((BaseModel)_model);
            }

            return _ModelsReturn;
        }

        private string _getFormatedValue(object Value, Type Type)
        {
            if (typeof(String) == Type)
            {
                return "'" + Convert.ToString(Value) + "'";
            }
            else if (typeof(DateTime) == Type)
            {
                return Convert.ToDateTime(Value).ToShortDateString();
            }

            return Convert.ToString(Value);
        }

        private System.Data.OracleClient.OracleConnection _Connection;



        public List<BaseModel> Search(BaseModel AEOModel, string AOWhereClause, BaseModel.LoadMethod ChooseMethod)
        {
            throw new NotImplementedException();
        }
    }
}
