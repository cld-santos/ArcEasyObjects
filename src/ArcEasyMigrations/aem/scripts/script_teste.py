# -*- coding: utf-8 -*-
import arcpy
import aem.GDBManager
import argparse

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Monta o GSE Projetos.')
    parser.add_argument('--sde', type=str, required=True, help='Caminho do SDE para conectar ao banco.')
    args = parser.parse_args()

    arcpy.env.workspace = args.sde

    aem.GDBManager.Delete('AEMSDETable')
    aem.GDBManager.CreateTable('AEMSDETable', 'Revisões Empreiteira')
    aem.GDBManager.AddField('AEMSDETable', 'NU_REVEMPREITEIRA_PLANTA_ID', 'NU_REVEMPREITEIRA_PLANTA_ID','Integer', 5, 0, 4)
    aem.GDBManager.AddField('AEMSDETable', 'NU_PROJETO_ID', 'NU_PROJETO_ID', 'Integer', 9, 0, 4)
    aem.GDBManager.AddField('AEMSDETable', 'NU_USUARIO_INFORMANTE_ID', 'NU_USUARIO_INFORMANTE_ID', 'Integer', 9,0, 4)
    aem.GDBManager.AddField('AEMSDETable', 'NU_EMPREITEIRA_ID', 'NU_EMPREITEIRA_ID', 'Integer', 9, 0, 4)
    aem.GDBManager.AddField('AEMSDETable', 'DT_DATA', 'DT_DATA', 'Date', 0, 0, 36)
    aem.GDBManager.AddField('AEMSDETable', 'NO_ATIVIDADE', 'NO_ATIVIDADE', 'String', 0, 0, 15)
    aem.GDBManager.AddField('AEMSDETable', 'NO_RESP_TECNICO', 'NO_RESP_TECNICO', 'String', 0, 0, 80)
    aem.GDBManager.AddField('AEMSDETable', 'DE_CREA', 'DE_CREA', 'String', 0, 0, 50)
    aem.GDBManager.AddField('AEMSDETable', 'CD_STATUS_PROJETO', 'CD_STATUS_PROJETO', 'Integer', 9, 0, 4)
    arcpy.ChangePrivileges_management(arcpy.env.workspace + "\\AEMSDETable", "NEOGSE", "GRANT", "GRANT")
