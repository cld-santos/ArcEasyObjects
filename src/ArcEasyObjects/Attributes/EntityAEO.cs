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

        public string EntityName
        {
            get { return _EntityName; }
        }

        public EntityAEOAttribute(string EntityName, BaseModel.Type TypeEntity)
        {
            _EntityName = EntityName;
            _TypeEntity = TypeEntity;
        }

        private string _EntityName;
        private BaseModel.Type _TypeEntity;

    }
}
