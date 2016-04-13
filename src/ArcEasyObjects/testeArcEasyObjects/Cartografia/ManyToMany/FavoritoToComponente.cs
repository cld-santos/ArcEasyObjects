using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.TB_COMPONENTE_FAVORITO", Type.Table)]
    public class FavoritoComponente : ArcEasyObjects.BaseModel
    {
        public FavoritoComponente(IWorkspace Workspace) : base(Workspace) { }

        [EntityKeyField("NU_FAVCOM_ID", typeof(int), "NEOSDE.SEQ_COMPONENTE_FAVORITO")]
        public int Identificador { get; set; }

        [EntityField("NU_FAVORITO_ID", typeof(int))]
        public int IdentificadorFavorito { get; set; }

        [EntityOneToOneField(typeof(Componente), "CD_COMPONENTE", typeof(Int32))]
        public Componente Componente { get; set; }

        [EntityField("NU_QUANTIDADE", typeof(double))]
        public double Quantidade { get; set; }

        [EntityField("NU_VALOR", typeof(double))] //INTEGER
        public double Valor { get; set; }

    }
}
