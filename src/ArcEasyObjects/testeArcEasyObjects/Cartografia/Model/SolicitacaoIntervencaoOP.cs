using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEasyObjects.Attributes;
using ESRI.ArcGIS.Geodatabase;

namespace testeArcEasyObjects.Cartografia.Model
{
    [EntityClass("NEOSDE.TB_SOLICITACAO_INTERVENCAO_OP", Type.Table)]
    public class SolicitacaoIntervencaoOP : ArcEasyObjects.BaseModel, IComparable<SolicitacaoIntervencaoOP>
    {

        public SolicitacaoIntervencaoOP(IWorkspace ws) : base(ws) { }

        [EntityField("NU_OCORRENCIA_OP_ID", typeof(Int32))]
        public int Nu_ocorrencia_op_id
        {
            get { return _nu_ocorrencia_op_id; }
            set { _nu_ocorrencia_op_id = value; }
        }

        [EntityField("DE_TELEFONE_RESPONSAVEL", typeof(String))]
        public string De_telefone_responsavel
        {
            get { return _de_telefone_responsavel; }
            set { _de_telefone_responsavel = value; }
        }

        [EntityField("NU_BAIRRO_ID", typeof(Int32))]
        public int Nu_bairro_id
        {
            get { return _nu_bairro_id; }
            set { _nu_bairro_id = value; }
        }

        [EntityField("NU_USUARIO", typeof(Int32))]
        public int Nu_usuario
        {
            get { return _nu_usuario; }
            set { _nu_usuario = value; }
        }

        [EntityField("DE_NOME_RESPONSAVEL", typeof(String))]
        public string De_nome_responsavel
        {
            get { return _de_nome_responsavel; }
            set { _de_nome_responsavel = value; }
        }

        [EntityField("DE_COMENTARIO", typeof(String))]
        public string De_comentario
        {
            get { return _de_comentario; }
            set { _de_comentario = value; }
        }

        [EntityField("NU_SOLIC_INTERVENCAO_OP_ID", typeof(Int32))]
        public int Nu_solic_intervencao_op_id
        {
            get { return _nu_solic_intervencao_op_id; }
            set { _nu_solic_intervencao_op_id = value; }
        }

        [EntityDateField("DT_SOLICITACAO")]
        public DateTime Dt_solicitacao
        {
            get { return _dt_solicitacao; }
            set { _dt_solicitacao = value; }
        }

        [EntityField("NU_REGIONAL_ID", typeof(Int32))]
        public int Nu_regional_id
        {
            get { return _nu_regional_id; }
            set { _nu_regional_id = value; }
        }

        [EntityField("CD_CODIGO_AREA", typeof(Int32))]
        public int Cd_codigo_area
        {
            get { return _cd_codigo_area; }
            set { _cd_codigo_area = value; }
        }

        [EntityField("NU_TIPO_NATUREZA_OP_ID", typeof(Int32))]
        public int Nu_tipo_natureza_op_id
        {
            get { return _nu_tipo_natureza_op_id; }
            set { _nu_tipo_natureza_op_id = value; }
        }

        [EntityField("NU_TIPO_SOLIC_INTERV_OP_ID", typeof(Int32))]
        public int Nu_tipo_solic_interv_op_id
        {
            get { return _nu_tipo_solic_interv_op_id; }
            set { _nu_tipo_solic_interv_op_id = value; }
        }

        [EntityField("NU_MOTIVO_SOLIC_INTERV_OP_ID", typeof(Int32))]
        public int Nu_motivo_solic_interv_op_id
        {
            get { return _nu_motivo_solic_interv_op_id; }
            set { _nu_motivo_solic_interv_op_id = value; }
        }

        [EntityField("DE_DESENHO", typeof(String))]
        public string De_desenho
        {
            get { return _de_desenho; }
            set { _de_desenho = value; }
        }

        [EntityDateField("DT_INICIO_PREVISTO")]
        public DateTime Dt_inicio_previsto
        {
            get { return _dt_inicio_previsto; }
            set { _dt_inicio_previsto = value; }
        }

        [EntityDateField("DT_TERMINO_PREVISTO")]
        public DateTime Dt_termino_previsto
        {
            get { return _dt_termino_previsto; }
            set { _dt_termino_previsto = value; }
        }

        [EntityDateTimeField("DT_INICIO_REAL")]
        public DateTime Dt_inicio_real
        {
            get { return _dt_inicio_real; }
            set { _dt_inicio_real = value; }
        }

        [EntityDateTimeField("DT_TERMINO_REAL")]
        public DateTime Dt_termino_real
        {
            get { return _dt_termino_real; }
            set { _dt_termino_real = value; }
        }

        [EntityField("DE_ENDERECO", typeof(String))]
        public string De_endereco
        {
            get { return _de_endereco; }
            set { _de_endereco = value; }
        }

        [EntityField("NU_EMPRESA_ID", typeof(Int32))]
        public int Nu_empresa_id
        {
            get { return _nu_empresa_id; }
            set { _nu_empresa_id = value; }
        }

        [EntityField("CD_TIPO_DESLIGAM_SOLIC_INTERV", typeof(Int32))]
        public int Cd_tipo_desligam_solic_interv
        {
            get { return _cd_tipo_desligam_solic_interv; }
            set { _cd_tipo_desligam_solic_interv = value; }
        }

        [EntityField("DE_REFERENCIA", typeof(String))]
        public string De_referencia
        {
            get { return _de_referencia; }
            set { _de_referencia = value; }
        }

        [EntityField("CD_STATUS_SOLIC_INTERVENCAO", typeof(Int32))]
        public int Cd_status_solic_intervencao
        {
            get { return _cd_status_solic_intervencao; }
            set { _cd_status_solic_intervencao = value; }
        }

        [EntityField("CD_CANCELAMENTO_SOLIC_INTERV", typeof(Int32))]
        public int Cd_cancelamento_solic_interv
        {
            get { return _cd_cancelamento_solic_interv; }
            set { _cd_cancelamento_solic_interv = value; }
        }

        [EntityField("NU_INSTALACAO_MT_ID", typeof(Int32))]
        public int Nu_instalacao_mt_id
        {
            get { return _nu_instalacao_mt_id; }
            set { _nu_instalacao_mt_id = value; }
        }

        [EntityField("DE_PM", typeof(String))]
        public string De_pm
        {
            get { return _de_pm; }
            set { _de_pm = value; }
        }

        [EntityField("NU_EMPREITEIRA_ID", typeof(Int32))]
        public int Nu_empreiteira_id
        {
            get { return _nu_empreiteira_id; }
            set { _nu_empreiteira_id = value; }
        }

        [EntityField("DE_END_AFETADOS", typeof(String))]
        public string De_end_afetados
        {
            get { return _de_end_afetados; }
            set { _de_end_afetados = value; }
        }

        [EntityField("CD_CARACTERISTICA", typeof(Int32))]
        public int Cd_caracteristica
        {
            get { return _cd_caracteristica; }
            set { _cd_caracteristica = value; }
        }

        [EntityField("CD_CLASSE", typeof(String))]
        public string Cd_classe
        {
            get { return _cd_classe; }
            set { _cd_classe = value; }
        }

        [EntityField("FL_TRANSF_CARGA", typeof(String))]
        public string Fl_transf_carga
        {
            get { return _fl_transf_carga; }
            set { _fl_transf_carga = value; }
        }

        [EntityField("FL_LINHA_VIVA", typeof(String))]
        public string Fl_linha_viva
        {
            get { return _fl_linha_viva; }
            set { _fl_linha_viva = value; }
        }

        [EntityField("CD_SI_PRINCIPAL", typeof(Int32))]
        public int Cd_si_principal
        {
            get { return _cd_si_principal; }
            set { _cd_si_principal = value; }
        }

        [EntityField("FL_APROVEITAMENTO", typeof(String))]
        public string Fl_aproveitamento
        {
            get { return _fl_aproveitamento; }
            set { _fl_aproveitamento = value; }
        }

        [EntityField("FL_SI_PRINCIPAL", typeof(String))]
        public string Fl_si_principal
        {
            get { return _fl_si_principal; }
            set { _fl_si_principal = value; }
        }

        [EntityField("FL_GERA_PENDENC_CADASTRO", typeof(String))]
        public string Fl_gera_pendenc_cadastro
        {
            get { return _fl_gera_pendenc_cadastro; }
            set { _fl_gera_pendenc_cadastro = value; }
        }

        [EntityField("FL_OBRA_CONCLUIDA", typeof(String))]
        public string Fl_obra_concluida
        {
            get { return _fl_obra_concluida; }
            set { _fl_obra_concluida = value; }
        }

        [EntityField("NU_TRECHO", typeof(String))]
        public string Nu_trecho
        {
            get { return _nu_trecho; }
            set { _nu_trecho = value; }
        }

        [EntityField("NU_CHAVE_PRINCIPAL", typeof(String))]
        public string Nu_chave_principal
        {
            get { return _nu_chave_principal; }
            set { _nu_chave_principal = value; }
        }

        [EntityField("NU_OBJECTID_SUBESTACAO", typeof(Int32))]
        public int Nu_objectid_subestacao
        {
            get { return _nu_objectid_subestacao; }
            set { _nu_objectid_subestacao = value; }
        }

        [EntityField("NU_USU_ID_SUBSTITUTO", typeof(Int32))]
        public int Nu_usu_id_substituto
        {
            get { return _nu_usu_id_substituto; }
            set { _nu_usu_id_substituto = value; }
        }

        [EntityField("DE_FONE_SUBSTITUTO", typeof(String))]
        public string De_fone_substituto
        {
            get { return _de_fone_substituto; }
            set { _de_fone_substituto = value; }
        }

        [EntityField("NU_USU_ID_SUBSTITUTO2", typeof(Int32))]
        public int Nu_usu_id_substituto2
        {
            get { return _nu_usu_id_substituto2; }
            set { _nu_usu_id_substituto2 = value; }
        }

        [EntityField("DE_FONE_SUBSTITUTO2", typeof(String))]
        public string De_fone_substituto2
        {
            get { return _de_fone_substituto2; }
            set { _de_fone_substituto2 = value; }
        }

        [EntityField("NU_USU_ID_SUBSTITUTO3", typeof(Int32))]
        public int Nu_usu_id_substituto3
        {
            get { return _nu_usu_id_substituto3; }
            set { _nu_usu_id_substituto3 = value; }
        }

        [EntityField("DE_FONE_SUBSTITUTO3", typeof(String))]
        public string De_fone_substituto3
        {
            get { return _de_fone_substituto3; }
            set { _de_fone_substituto3 = value; }
        }

        [EntityField("NU_USU_ID_TECNICO", typeof(Int32))]
        public int Nu_usu_id_tecnico
        {
            get { return _nu_usu_id_tecnico; }
            set { _nu_usu_id_tecnico = value; }
        }

        [EntityField("DE_FONE_TECNICO", typeof(String))]
        public string De_fone_tecnico
        {
            get { return _de_fone_tecnico; }
            set { _de_fone_tecnico = value; }
        }

        [EntityField("DE_ISOLACAO_PRECAUCAO", typeof(String))]
        public string De_isolacao_precaucao
        {
            get { return _de_isolacao_precaucao; }
            set { _de_isolacao_precaucao = value; }
        }

        [EntityField("FL_PROJ_CENTRO_CUSTO", typeof(String))]
        public string Fl_proj_centro_custo
        {
            get { return _fl_proj_centro_custo; }
            set { _fl_proj_centro_custo = value; }
        }

        [EntityField("NU_PROJ_CENTRO_CUSTO", typeof(String))]
        public string Nu_proj_centro_custo
        {
            get { return _nu_proj_centro_custo; }
            set { _nu_proj_centro_custo = value; }
        }

        [EntityField("NU_USU_ID_RESPONSAVEL", typeof(Int32))]
        public int Nu_usu_id_responsavel
        {
            get { return _nu_usu_id_responsavel; }
            set { _nu_usu_id_responsavel = value; }
        }

        [EntityField("CD_AREA_RESPONSAVEL", typeof(Int32))]
        public int Cd_area_responsavel
        {
            get { return _cd_area_responsavel; }
            set { _cd_area_responsavel = value; }
        }

        [EntityField("CD_ORGAO_EXECUTOR", typeof(Int32))]
        public int Cd_orgao_executor
        {
            get { return _cd_orgao_executor; }
            set { _cd_orgao_executor = value; }
        }

        [EntityField("FL_MANOBRA", typeof(String))]
        public string Fl_manobra
        {
            get { return _fl_manobra; }
            set { _fl_manobra = value; }
        }

        [EntityField("NU_CONTRATO_CONS_ID", typeof(Int32))]
        public int Nu_contrato_cons_id
        {
            get { return _nu_contrato_cons_id; }
            set { _nu_contrato_cons_id = value; }
        }


        public int CompareTo(SolicitacaoIntervencaoOP si)
        {
            return this.Nu_ocorrencia_op_id.CompareTo(si.Nu_ocorrencia_op_id);
        }

        private int _nu_ocorrencia_op_id;
        private string _de_telefone_responsavel;
        private int _nu_bairro_id;
        private int _nu_usuario;
        private string _de_nome_responsavel;
        private string _de_comentario;
        private int _nu_solic_intervencao_op_id;
        private DateTime _dt_solicitacao;
        private int _nu_regional_id;
        private int _cd_codigo_area;
        private int _nu_tipo_natureza_op_id;
        private int _nu_tipo_solic_interv_op_id;
        private int _nu_motivo_solic_interv_op_id;
        private string _de_desenho;
        private DateTime _dt_inicio_previsto;
        private DateTime _dt_termino_previsto;
        private DateTime _dt_inicio_real;
        private DateTime _dt_termino_real;
        private string _de_endereco;
        private int _nu_empresa_id;
        private int _cd_tipo_desligam_solic_interv;
        private string _de_referencia;
        private int _cd_status_solic_intervencao;
        private int _cd_cancelamento_solic_interv;
        private int _nu_instalacao_mt_id;
        private string _de_pm;
        private int _nu_empreiteira_id;
        private string _de_end_afetados;
        private int _cd_caracteristica;
        private string _cd_classe;
        private string _fl_transf_carga;
        private string _fl_linha_viva;
        private string _fl_aproveitamento;
        private string _fl_si_principal;
        private int _cd_si_principal;
        private string _fl_gera_pendenc_cadastro;
        private string _fl_obra_concluida;
        private string _nu_trecho;
        private string _nu_chave_principal;
        private int _nu_objectid_subestacao;
        private int _nu_usu_id_substituto;
        private string _de_fone_substituto;
        private int _nu_usu_id_substituto2;
        private string _de_fone_substituto2;
        private int _nu_usu_id_substituto3;
        private string _de_fone_substituto3;
        private int _nu_usu_id_tecnico;
        private string _de_fone_tecnico;
        private string _de_isolacao_precaucao;
        private string _fl_proj_centro_custo;
        private string _nu_proj_centro_custo;
        private int _nu_usu_id_responsavel;
        private int _cd_area_responsavel;
        private int _cd_orgao_executor;
        private string _fl_manobra;
        private int _nu_contrato_cons_id;
    }
}
