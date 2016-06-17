# -*- coding: utf-8 -*-
import arcpy

def Delete(nome):
    try:
        arcpy.Delete_management(arcpy.env.workspace + "\\" + nome)
        print u"Entidade {} deletada.".format(nome)
    except Exception as ex:
        print u"ERRO:", nome, ex.message

def RegisterAsVersioned(nome):
    try:
        description = arcpy.Describe(nome)

        if description.isVersioned:
            raise VersionamentoError(u"A tabela {} ja eh versionada.".format(nome))

        if not description.canVersion:
            raise VersionamentoError(u"A tabela {} nao pode ser versionada.".format(nome))

        arcpy.RegisterAsVersioned_management(arcpy.env.workspace + "\\" + nome, "NO_EDITS_TO_BASE")
        print u"Entidade {} registrada como versionada".format(nome)
    except Exception as ex:
        print u"ERRO:", nome, ex.message

def CreateDataset(nome, spatialReference):
    try:
        arcpy.CreateFeatureDataset_management(arcpy.env.workspace, nome, spatialReference)
        print u"Dataset {} criado com sucesso".format(nome)
    except Exception as ex:
        print u"ERRO:", nome, ex.message

def CreateTable(nome, aliasName):
    try:
        arcpy.CreateTable_management(arcpy.env.workspace, nome)
        arcpy.AlterAliasName(arcpy.env.workspace + "\\" + nome, aliasName)
        print u"SDE Table {} criado com sucesso".format(nome)
    except Exception as ex:
        print u"ERRO:", nome, ex.message

def CreateFeatureClass(nome, aliasName, tipo, dataset, spatialReference):
    try:
        arcpy.CreateFeatureclass_management(arcpy.env.workspace + "\\" + dataset , nome, tipo, "", "DISABLED", "DISABLED", spatialReference)
        arcpy.AlterAliasName(arcpy.env.workspace + "\\" + dataset + "\\" + nome, aliasName)
        print u"Feature Class {} do tipo {} criado com sucesso".format(dataset+"."+nome,tipo)
    except Exception as ex:
        print u"ERRO:", dataset + "." + nome, ex.message

def AddGlobalIDField(dataset):
    try:
        arcpy.AddGlobalIDs_management(arcpy.env.workspace + "\\" + dataset)
        print u"Campo GlobalID adicionado em {} com sucesso".format(dataset)
    except Exception as ex:
        print u"ERRO:", dataset, ex.message

def AddField(feature, alias, nome, tipo, precisao=None, escala=None, tamanho=None):
    try:
        arcpy.AddField_management(feature, nome, tipo, precisao, escala, tamanho, alias, "NULLABLE", "NON_REQUIRED")
        print u"O Campo {} da feature class {} foi criado com sucesso.".format(nome, feature)
    except Exception as ex:
        print u"ERR: O Campo {} da feature class {} nao foi criado.".format(nome, feature), ex.message

def CreateGeometricNetwork(dataset, nome, netFeatures, tolerance):
    try:
        arcpy.CreateGeometricNetwork_management(dataset, nome, netFeatures , tolerance)
        print u"Rede Geometrica criada com sucesso"
    except Exception as ex:
        print u"ERRO GN:",nome,  ex.message

def CreateDomain(workspace, nome, desc, field_type, domain_type, split_policy, merge_policy):
    try:
        domain_names = [d.name for d in arcpy.da.ListDomains(workspace)]
        if nome in domain_names:
            print u"Dominio {} já existe.".format(nome)
        else:
            arcpy.CreateDomain_management(workspace, nome, desc, field_type, domain_type, split_policy, merge_policy)
            print u"Dominio {} foi criado com sucesso.".format(nome)
    except Exception as ex:
            print u"ERRO ao criar dominio:", nome,  ex.message

def AddCodedValueToDomain(workspace, domain_name, code, code_description):
    try:
        arcpy.AddCodedValueToDomain_management(workspace, domain_name, code, code_description)
        print u"Valor {} adicionado ao dominio {} com sucesso.".format(code, domain_name)
    except Exception as ex:
            print u"ERRO ao adicionar valor {} ao dominio {}:".format(code, domain_name),  ex.message

def AssignDomainToField(in_table, field_name, domain_name):
    try:
        arcpy.AssignDomainToField_management(in_table, field_name, domain_name)
        print u"Campo {}.{} associado ao dominio {} com sucesso.".format(in_table, field_name, domain_name)
    except Exception as ex:
            print u"ERRO ao adicionar dominio ao campo {}.{}:".format(in_table, field_name),  ex.message

