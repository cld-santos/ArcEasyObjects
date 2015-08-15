using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityKeyFieldAttribute : Attribute, IEntityField
    {
        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type FieldType
        {
            get { return _fieldType; }
        }

        public EntityKeyFieldAttribute(string FieldName, Type FieldType)
        {
            _fieldName = FieldName;
            _fieldType = FieldType;
        }


        public EntityKeyFieldAttribute(string FieldName, Type FieldType, String Sequence)
            : this(FieldName, FieldType)
        {
            _Sequence = Sequence;
        }


        public string Sequence
        {
            get { return _Sequence; }
            set { _Sequence = value; }
        }




        public void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            if (!String.IsNullOrEmpty(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)).ToString()))
            {
                Property.Property.SetValue(BaseModel,
                                                    Convert.ChangeType(Row.get_Value(Row.Fields.FindField(Property.Attribute.FieldName)),
                                                                    Property.Attribute.FieldType), null);
            }
        }

        public void Load(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Row, BaseModel, Property);
        }

        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            if (!String.IsNullOrEmpty(Feature.get_Value(Feature.Fields.FindField(Property.Attribute.FieldName)).ToString()))
            {
                Property.Property.SetValue(BaseModel,
                                             Convert.ChangeType(Feature.get_Value(Feature.Fields.FindField(Property.Attribute.FieldName)),
                                                                Property.Attribute.FieldType),
                                             null);
            }
        }

        public void Load(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property, BaseModel.LoadMethod ChooseLoadMethod)
        {
            this.Load(Workspace, Feature, BaseModel, Property);
        }

        private string _fieldName;
        private Type _fieldType;
        private string _Sequence;



        public void Save(IWorkspace Workspace, IFeature Feature, BaseModel BaseModel, ModelProperty Property)
        {
            EntityKeyFieldAttribute _keyField = (EntityKeyFieldAttribute)Property.Attribute;
            if (String.IsNullOrEmpty(_keyField.Sequence))
            {
                Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType));

            }
            else
            {
                ICursor cursor = Helper.GDBCursor.obterCursor((IFeatureWorkspace)Workspace, "SYS.DUAL", _keyField.Sequence + ".NEXTVAL", "");
                IRow row = cursor.NextRow();
                Feature.set_Value(Feature.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(row.get_Value(0).ToString(), Property.Attribute.FieldType));
            }
        }

        public void Save(IWorkspace Workspace, IRow Row, BaseModel BaseModel, ModelProperty Property)
        {
            EntityKeyFieldAttribute _keyField = (EntityKeyFieldAttribute)Property.Attribute;
            if (String.IsNullOrEmpty(_keyField.Sequence))
            {
                Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType));
            }
            else
            {
                ICursor cursor = Helper.GDBCursor.obterCursor((IFeatureWorkspace)Workspace, "SYS.DUAL", _keyField.Sequence + ".NEXTVAL", "");
                IRow row = cursor.NextRow();
                Row.set_Value(Row.Fields.FindField(Property.Attribute.FieldName), Convert.ChangeType(row.get_Value(0).ToString(), Property.Attribute.FieldType));
            }

        }



        public string Save(IWorkspace Workspace, BaseModel BaseModel, ModelProperty Property)
        {
            EntityKeyFieldAttribute _keyField = (EntityKeyFieldAttribute)Property.Attribute;
            if (String.IsNullOrEmpty(_keyField.Sequence))
            {
                return FieldFormatHelper.FormatField(Property.Property.GetValue(BaseModel, null), Property.Attribute.FieldType);
            }
            else
            {
                ICursor cursor = Helper.GDBCursor.obterCursor((IFeatureWorkspace)Workspace, "SYS.DUAL", _keyField.Sequence + ".NEXTVAL", "");
                IRow row = cursor.NextRow();
                
                Property.Property.SetValue(BaseModel, Convert.ChangeType(row.get_Value(0).ToString(), Property.Attribute.FieldType), null);

                return row.get_Value(0).ToString();
            }
        }
    }
}