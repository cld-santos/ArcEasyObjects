using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects;
using System.Reflection;

namespace ArcEasyObjects
{
    public class FeatureAEO
    {
        private IModel _modelo;

        public FeatureAEO(IModel Modelo)
        {
            this._modelo = Modelo;
        }

        public string obterNomeFeatureClass()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_modelo.GetType());

            foreach (System.Attribute attr in attrs)
            {
                if (attr is FeatureClassAEOAttribute)
                {
                    FeatureClassAEOAttribute a = (FeatureClassAEOAttribute)attr;
                    return a.NomeFeatureClass;
                }
            }
            return "";
        }

        public string obterAtributosFeatureClass()
        {
            MemberInfo[] _members = _modelo.GetType().GetMembers();

            foreach (MemberInfo member in _members.Where(x => x.MemberType == MemberTypes.Property))
            {
                object[] attributes = member.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is FeatureClassFieldsAEOAttribute)
                    {
                        FeatureClassFieldsAEOAttribute a = (FeatureClassFieldsAEOAttribute)attribute;
                        return a.FieldName;
                    }

                }
            }
            return "";

        }
    }
}
