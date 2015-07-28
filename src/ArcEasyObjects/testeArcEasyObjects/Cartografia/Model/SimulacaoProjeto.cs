using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityAEO("NEOSDE.TB_SIMULACAO_PROJETO", Type.GISTable)]
    public class SimulacaoProjeto : ArcEasyObjects.GISModel
    {
        private string _no_versao;
        private string _de_observacao;
        private DateTime _dt_criacao;
        private string _de_status_simulacao;
        private double _nu_valor_total;
        private int _nu_total_clientes;
        private double _nu_total_mao_de_obra;
        private double _nu_total_materiais;
        private int _nu_total_postes;
        private int _nu_total_trafo;
        private int _nu_total_equip;
        private double _nu_total_km_bt;
        private double _nu_media_cliente;
        private int _nu_projeto_id;
        private double _nu_total_km_mt_at;

        public SimulacaoProjeto(IWorkspace Workspace) : base(Workspace) { }

        [EntityFieldAEO("NO_VERSAO", typeof(String))]
        public String no_versao
        {
            get { return _no_versao; }
            set { _no_versao = value; }
        }

        [EntityFieldAEO("NU_PROJETO_ID", typeof(Int32))]
        public Int32 nu_projeto_id
        {
            get { return _nu_projeto_id; }
            set { _nu_projeto_id = value; }
        }

        [EntityFieldAEO("DE_OBSERVACAO", typeof(String))]
        public String de_observacao
        {
            get { return _de_observacao; }
            set { _de_observacao = value; }
        }

        [EntityFieldAEO("DT_CRIACAO", typeof(DateTime))]
        public DateTime dt_criacao
        {
            get { return _dt_criacao; }
            set { _dt_criacao = value; }
        }

        [EntityFieldAEO("DE_STATUS_SIMULACAO", typeof(String))]
        public String de_status_simulacao
        {
            get { return _de_status_simulacao; }
            set { _de_status_simulacao = value; }
        }

        [EntityFieldAEO("NU_VALOR_TOTAL", typeof(Double))]
        public Double nu_valor_total
        {
            get { return _nu_valor_total; }
            set { _nu_valor_total = value; }
        }

        [EntityFieldAEO("NU_TOTAL_CLIENTES", typeof(Int32))]
        public Int32 nu_total_clientes
        {
            get { return _nu_total_clientes; }
            set { _nu_total_clientes = value; }
        }

        [EntityFieldAEO("NU_TOTAL_MAO_DE_OBRA", typeof(Double))]
        public Double nu_total_mao_de_obra
        {
            get { return _nu_total_mao_de_obra; }
            set { _nu_total_mao_de_obra = value; }
        }

        [EntityFieldAEO("NU_TOTAL_MATERIAIS", typeof(Double))]
        public Double nu_total_materiais
        {
            get { return _nu_total_materiais; }
            set { _nu_total_materiais = value; }
        }

        [EntityFieldAEO("NU_TOTAL_POSTES", typeof(Int32))]
        public Int32 nu_total_postes
        {
            get { return _nu_total_postes; }
            set { _nu_total_postes = value; }
        }

        [EntityFieldAEO("NU_TOTAL_TRAFOS", typeof(Int32))]
        public Int32 nu_total_trafo
        {
            get { return _nu_total_trafo; }
            set { _nu_total_trafo = value; }
        }

        [EntityFieldAEO("NU_TOTAL_EQUIP", typeof(Int32))]
        public Int32 nu_total_equip
        {
            get { return _nu_total_equip; }
            set { _nu_total_equip = value; }
        }

        [EntityFieldAEO("NU_TOTAL_KM_MT_AT", typeof(Double))]
        public Double nu_total_km_mt_at
        {
            get { return _nu_total_km_mt_at; }
            set { _nu_total_km_mt_at = value; }
        }

        [EntityFieldAEO("NU_TOTAL_KM_BT", typeof(Double))]
        public Double nu_total_km_bt
        {
            get { return _nu_total_km_bt; }
            set { _nu_total_km_bt = value; }
        }

        [EntityFieldAEO("NU_MEDIA_CLIENTE", typeof(Double))]
        public Double nu_media_cliente
        {
            get { return _nu_media_cliente; }
            set { _nu_media_cliente = value; }
        }

    }
}
