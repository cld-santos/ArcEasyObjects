using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.ManyToMany
{
    [EntityClass("NEOSDE.TB_FAVORITO",Type.Table)]
    public class Favorito:ArcEasyObjects.BaseModel
    {
        public Favorito(IWorkspace Workspace) : base(Workspace){}

        [EntityKeyField("NU_FAVORITO_ID",typeof(Int32),"NEOSDE.SEQ_FAVORITO")]
        public Int32 Identificador {get; set;}

        [EntityField("NO_NOME",typeof(string))]
        public string Nome {get; set;}

        [EntityField("CD_USUARIO",typeof(Int32))]
        public Int32 CodigoUsuario {get; set;}

        [EntityField("CD_EMPREITEIRA",typeof(Int32))]
        public Int32 CodigoEmpreiteira { get; set; }

        [EntityField("FL_PONTO", typeof(bool))]
        public bool ehPonto { get; set; }

        [EntityManyToManyField(typeof(FavoritoToComponente), "IdentificadorFavorito")]
        IList<Componente> Componentes { get; set; }

        [EntityManyToManyField(typeof(FavoritoToModuloConstrutivo), "IdentificadorFavorito")]
        IList<ModConstrutivo> ModulosConstrutivos { get; set; }
    }
}

