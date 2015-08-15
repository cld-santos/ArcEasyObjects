using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.TB_COMPONENTE_MODCONSTRUTIVO", Type.GISTable)]
    public class ModConstrutivoComponente : ArcEasyObjects.GISModel
    {

        public ModConstrutivoComponente(IWorkspace Workspace) : base(Workspace) { }

        public ModConstrutivoComponente(Int64 IdModConstrutivo, Int64 IdComponente, double Quantidade, double ValorTotal, Componente objComponente)
        {
            this.IdModConstrutivo = IdModConstrutivo;
            this.IdComponente = IdComponente;
            this.Quantidade = Quantidade;
            this.ValorTotal = ValorTotal;
            this.objComponente = objComponente;
        }

        [EntityField("NU_MODCONSTRUTIVO_ID",typeof(Int64))]
        public Int64 IdModConstrutivo { get; set; }
        [EntityField("NU_COMPONENTE_ID", typeof(Int64))]
        public Int64 IdComponente { get; set; }
        [EntityField("QT_QUANTIDADE", typeof(double))]
        public double Quantidade { get; set; }
        [EntityField("NU_VALOR_TOTAL", typeof(double))]
        public double ValorTotal { get; set; }
        [EntityOneToOneField(typeof(Componente), "NU_COMPONENTE_ID", typeof(Int64))]
        public Componente objComponente { get; set; }
    }
}

