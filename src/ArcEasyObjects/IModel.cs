using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public abstract class IModel
    {
        IPersistencia _persistencia;

        public void salvar()
        {
            _persistencia.salvar();
        }
    }
}
