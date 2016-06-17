import io
import json
from aem import EasyJSON


class Environment(EasyJSON.EasyJSON):

    key = ""
    sde_filepath= ""
    user= ""
    password= ""
    tns_name = ""

    def __init__(self, key, sde_filepath="", user="", password="", tns_name=""):
        self.key = key
        self.sde_filepath = sde_filepath
        self.user = user
        self.password = password
        self.tns_name = tns_name

    def set_sde_file(self, sde_filepath):
        self.sde_filepath = sde_filepath

    def set_db_paramns(self, user, password, tns_name):
        self.user = user
        self.password = password
        self.tns_name = tns_name


def load_environment(key):
    src_env = io.open("environment.json", "r")
    src_json_env = json.load(src_env)
    src_env.close()
    return Environment(key, src_json_env[key]["sde_filepath"], src_json_env[key]["user"], src_json_env[key][ "password"], src_json_env[key]["tns_name"])



