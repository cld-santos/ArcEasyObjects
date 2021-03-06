﻿using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("TB_EXTRA_INFO",  Type.GISTable)]
    public class InformacaoExtra : ArcEasyObjects.GISModel
    {

        
        
        //TODO: Remover dependencia explicita da classe pai
        public InformacaoExtra(IWorkspace Workspace) : base(Workspace) { }


        [EntityOIDField("OBJECTID", typeof(Int32))]
        public override Int32 ObjectId
        {
            get { return base.ObjectId; }
            set { base.ObjectId = value; }
        }

        [EntityKeyField("CD_EXTRAINFO", typeof(Int32))]
        public Int32 CodigoInformacaoExtra
        {
            get { return _CodigoInformacaoExtra; }
            set { _CodigoInformacaoExtra = value; }
        }

        [EntityField("CD_PN", typeof(Int32))]
        public Int32 CodigoPontoNotavel
        {
            get { return _CodigoPontoNotavel; }
            set { _CodigoPontoNotavel = value; }
        }

        [EntityField("DE_INFORMACOES", typeof(String))]
        public String Informacoes
        {
            get { return _Informacoes; }
            set { _Informacoes = value; }
        }

        [EntityDateField("DT_CADASTRO", typeof(DateTime))]
        public DateTime? DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }

        private Int32 _CodigoInformacaoExtra;
        private Int32 _CodigoPontoNotavel;
        private String _Informacoes;
        private DateTime? _DataCadastro;

    }
}
