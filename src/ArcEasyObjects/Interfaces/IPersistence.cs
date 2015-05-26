using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public interface IPersistence
    {
        void Save(Model AEOModel);
        void Load(Model AEOModel, int KeyFieldValue);
    }
}
