import io
import json
import unittest
from os import path
import aem
from aem.Environment import Environment


class EnvironmentTest(unittest.TestCase):

    def test_deve_criar_um_ambiente_para_implantacao(self):
        key_env = "BRCGI977"
        env = Environment(key_env)
        self.assertEqual(env.key, key_env)

    def test_deve_configurar_os_parametros_de_conexao_ao_geodatabase(self):
        sde_file = "e:\\csantos\\sdeFiles\\neosde@BRCGI977.sde"
        env = Environment("BRCGI977")
        env.set_sde_file(sde_file)
        self.assertEqual(env.sde_filepath, sde_file)

    def test_deve_configurar_os_parametros_de_conexao_ao_oracle(self):

        user = "neosde"
        password = "neosde01"
        tns_name = "BRCGI577_ORACELPE"

        env = Environment("BRCGI977")
        env.set_db_paramns(user, password, tns_name)
        self.assertEqual(env.user, user)
        self.assertEqual(env.tns_name, tns_name)
        self.assertEqual(env.password, password)

    def test_deve_registrar_um_env_criado(self):

        key_env = "BRCGI977"
        user = "neosde"
        password = "neosde01"
        tns_name = "BRCGI577_ORACELPE"
        sde_file = "e:\\csantos\\sdeFiles\\neosde@BRCGI977.sde"

        env = Environment(key_env)

        env.set_sde_file(sde_file)
        env.set_db_paramns(user, password, tns_name)

        env.save("environment.json")

        self.assertTrue(path.isfile("environment.json"))

    def test_carregar_um_environment_existente(self):
        key_env = u"BRCGI977"
        user = u"neosde"
        password = u"neosde01"
        tns_name = u"BRCGI577_ORACELPE"
        sde_file = u"e:\\csantos\\sdeFiles\\neosde@BRCGI977.sde"

        _env = aem.Environment.load_environment("BRCGI977")

        self.assertEqual(_env.key, key_env)
        self.assertEqual(_env.sde_filepath, sde_file)
        self.assertEqual(_env.user, user)
        self.assertEqual(_env.tns_name, tns_name)
        self.assertEqual(_env.password, password)

    def test_deve_gerar_mais_de_um_environment(self):

        key_env = "BRCGI977"
        user = "neosde"
        password = "neosde01"
        tns_name = "BRCGI577_ORACELPE"
        sde_file = "e:\\csantos\\sdeFiles\\neosde@BRCGI977.sde"

        env = Environment(key_env)

        env.set_sde_file(sde_file)
        env.set_db_paramns(user, password, tns_name)

        env.save("environment.json")

        key_env = "BRCGI958"
        user = "neosde"
        password = "neosdelalala"
        tns_name = "BRCGI958_ORACELPE"
        sde_file = "e:\\csantos\\sdeFiles\\neosde@BRCGI958.sde"

        env = Environment(key_env)

        env.set_sde_file(sde_file)
        env.set_db_paramns(user, password, tns_name)

        env.save("environment.json")

        self.assertTrue(path.isfile("environment.json"))

        src_env = open("environment.json", "r")
        _environment = json.load(src_env)
        src_env.close()
        self.assertTrue(len(_environment) > 2)

