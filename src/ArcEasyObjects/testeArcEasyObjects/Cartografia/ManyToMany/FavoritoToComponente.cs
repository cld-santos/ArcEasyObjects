using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.RC_FAVORITO_COMPONENTE",Type.Table)]
    public class FavoritoToComponente: ArcEasyObjects.BaseModel
    {
        public FavoritoToComponente(IWorkspace Workspace) : base(Workspace) { }

        [EntityField("NU_FAVORITO_ID",typeof(int))] //NUMBER(9) NOT NULL,
        public int IdentificadorFavorito {get; set;}
        [EntityField("CD_COMPONENTE" ,typeof(int))] //INTEGER
        public int IdentificadorComponente { get; set; }
    }
}
