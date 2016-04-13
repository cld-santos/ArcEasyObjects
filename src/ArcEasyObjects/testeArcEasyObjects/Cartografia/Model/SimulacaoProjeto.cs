using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("NEOSDE.TB_SIMULACAO_PROJETO", Type.GISTable)]
    public class SimulacaoProjeto : ArcEasyObjects.GISModel, IComparable<SimulacaoProjeto>
    {
        private string _no_versao;
        private string _de_observacao;
        private DateTime _dt_criacao;
        private Int32 _cd_status_projeto;
        private string _no_status_projeto;
        private string _de_status_simulacao;
        private DateTime? _dt_calculo;
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
        private double _nu_custo_por_km;

        public SimulacaoProjeto(IWorkspace Workspace) : base(Workspace) { }

        [EntityField("NO_VERSAO", typeof(String))]
        public String no_versao
        {
            get { return _no_versao; }
            set { _no_versao = value; }
        }

        [EntityField("NU_PROJETO_ID", typeof(Int32))]
        public Int32 nu_projeto_id
        {
            get { return _nu_projeto_id; }
            set { _nu_projeto_id = value; }
        }

        [EntityField("DE_OBSERVACAO", typeof(String))]
        public String de_observacao
        {
            get { return _de_observacao; }
            set { _de_observacao = value; }
        }

        [EntityField("DT_CRIACAO", typeof(DateTime))]
        public DateTime dt_criacao
        {
            get { return _dt_criacao; }
            set { _dt_criacao = value; }
        }

        [EntityField("CD_STATUS_PROJETO", typeof(Int32))]
        public Int32 cd_status_projeto
        {
            get { return _cd_status_projeto; }
            set { _cd_status_projeto = value; }
        }

        [EntityField("NO_STATUS_PROJETO", typeof(string))]
        public string no_status_projeto
        {
            get { return _no_status_projeto; }
            set { _no_status_projeto = value; }
        }

        [EntityField("DE_STATUS_SIMULACAO", typeof(String))]
        public String de_status_simulacao
        {
            get { return _de_status_simulacao; }
            set { _de_status_simulacao = value; }
        }

        [EntityDateField("DT_CALCULO", typeof(DateTime))]
        public DateTime? dt_calculo
        {
            get { return _dt_calculo; }
            set { _dt_calculo = value; }
        }

        [EntityField("NU_VALOR_TOTAL", typeof(Double))]
        public Double nu_valor_total
        {
            get { return _nu_valor_total; }
            set { _nu_valor_total = value; }
        }

        public string nu_valor_total_cifrado
        {
            get { return string.Format("R$ {0}", _nu_valor_total); }
        }

        [EntityField("NU_TOTAL_CLIENTES", typeof(Int32))]
        public Int32 nu_total_clientes
        {
            get { return _nu_total_clientes; }
            set { _nu_total_clientes = value; }
        }

        [EntityField("NU_TOTAL_MAO_DE_OBRA", typeof(Double))]
        public Double nu_total_mao_de_obra
        {
            get { return _nu_total_mao_de_obra; }
            set { _nu_total_mao_de_obra = value; }
        }

        public string nu_total_mao_de_obra_cifrado
        {
            get { return string.Format("R$ {0}", _nu_total_mao_de_obra); }
        }

        [EntityField("NU_TOTAL_MATERIAIS", typeof(Double))]
        public Double nu_total_materiais
        {
            get { return _nu_total_materiais; }
            set { _nu_total_materiais = value; }
        }

        public string nu_total_materiais_cifrado
        {
            get { return string.Format("R$ {0}", _nu_total_materiais); }
        }

        [EntityField("NU_TOTAL_POSTES", typeof(Int32))]
        public Int32 nu_total_postes
        {
            get { return _nu_total_postes; }
            set { _nu_total_postes = value; }
        }

        [EntityField("NU_TOTAL_TRAFOS", typeof(Int32))]
        public Int32 nu_total_trafo
        {
            get { return _nu_total_trafo; }
            set { _nu_total_trafo = value; }
        }

        [EntityField("NU_TOTAL_EQUIP", typeof(Int32))]
        public Int32 nu_total_equip
        {
            get { return _nu_total_equip; }
            set { _nu_total_equip = value; }
        }

        [EntityField("NU_TOTAL_KM_MT_AT", typeof(Double))]
        public Double nu_total_km_mt_at
        {
            get { return Math.Round(_nu_total_km_mt_at, 3); }
            set { _nu_total_km_mt_at = value; }
        }

        [EntityField("NU_TOTAL_KM_BT", typeof(Double))]
        public Double nu_total_km_bt
        {
            get { return Math.Round(_nu_total_km_bt, 3); }
            set { _nu_total_km_bt = value; }
        }

        [EntityField("NU_MEDIA_CLIENTE", typeof(Double))]
        public Double nu_media_cliente
        {
            get { return Math.Round(_nu_media_cliente, 3); }
            set { _nu_media_cliente = value; }
        }

        public string nu_media_cliente_cifrado
        {
            get { return string.Format("R$ {0}", Math.Round(_nu_media_cliente, 3)); }
        }

        /// <summary>
        /// Campo virtual para obter o custo por km
        /// </summary>
        public string nu_custo_por_km
        {
            get
            {
                if ((_nu_total_km_bt + _nu_total_km_mt_at) == 0)
                    return "R$ 0";
                return string.Format("R$ {0}", Math.Round((_nu_valor_total / (_nu_total_km_bt + _nu_total_km_mt_at)), 3));
            }
        }

        public int CompareTo(SimulacaoProjeto sp)
        {
            return this.no_versao.CompareTo(sp.no_versao);
        }
    }
}
