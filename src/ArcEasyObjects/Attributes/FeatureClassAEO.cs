using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class FeatureClassAEOAttribute : Attribute
    {
        private string _nomeFeatureClass;

        public string NomeFeatureClass
        {
            get { return _nomeFeatureClass; }
        }

        public FeatureClassAEOAttribute(string NomeFeatureClass)
        {
            _nomeFeatureClass = NomeFeatureClass;
        }
    }
}
