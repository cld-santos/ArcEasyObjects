﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public class EntityKeyFieldAEOAttribute : EntityFieldAEOAttribute
    {
        public EntityKeyFieldAEOAttribute(string FieldName, Type FieldType) : base(FieldName, FieldType) { }
    }
}