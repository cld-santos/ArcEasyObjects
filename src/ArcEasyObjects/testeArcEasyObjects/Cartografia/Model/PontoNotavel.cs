﻿using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("PT_PONTO_NOTAVEL", Type.FeatureClass)]
    public class PontoNotavel : ArcEasyObjects.GISModel
    {
        //TODO: Remover dependencia explicita da classe pai
        public PontoNotavel(IWorkspace Workspace) : base(Workspace) { }
    
        [EntityKeyField("CD_PN", typeof(Int32))]
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        
        [EntityField("NOME", typeof(String))]
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        [EntityField("DESCRICAO", typeof(String))]
        public String Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private InformacaoExtra _InformacaoExtra;

        [EntityOneToOneField(typeof(InformacaoExtra), "CD_INFO_EXTRA_FK", typeof(Int32))]
        public InformacaoExtra InformacaoExtra
        {
            get { return _InformacaoExtra; }
            set { _InformacaoExtra = value; }
        }



        private Int32 _Codigo;
        private String _Nome;
        private String _Descricao;

    }
}
