using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public interface IPersistence
    {
        void Save(BaseModel AEOModel);
        void Load(BaseModel AEOModel, int KeyFieldValue);
        List<BaseModel>  Search(BaseModel AEOModel,string AOWhereClause);
        //List<T> Search<T>(Model AEOModel, string AOWhereClause);
        void Update(BaseModel baseModel);
        void Delete(BaseModel baseModel);
    }
}
