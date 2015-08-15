using ArcEasyObjects.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.RC_FAVORITO_MODCONSTRUTIVO", Type.Table)]
    public class FavoritoToModuloConstrutivo: ArcEasyObjects.BaseModel
    {
        [EntityField("NU_FAVORITO_ID",typeof(int))] //NUMBER(9) NOT NULL,
        public int IdentificadorFavorito { get; set; }
        [EntityField("CD_MODCONSTRUTIVO", typeof(int))] //INTEGER
        public int IdentificadorModuloConstrutivo { get; set; }
    }

}
