using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEasyObjects
{
    public interface IPersistencia
    {

        void Salvar(Model model);
    }
}
