using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityAEOAttribute : Attribute
    {

        public BaseModel.Type TypeEntity
        {
            get { return _TypeEntity; }
        }

        public string FeatureClassName
        {
            get { return _FeatureClassName; }
        }

        public EntityAEOAttribute(string FeatureClassName, BaseModel.Type TypeEntity)
        {
            _FeatureClassName = FeatureClassName;
            _TypeEntity = TypeEntity;
        }

        private string _FeatureClassName;
        private BaseModel.Type _TypeEntity;

    }
}
