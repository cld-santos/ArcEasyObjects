import os
import json
from json import JSONEncoder


class EasyJSON:
    json_source = ""

    def __init__(self, json_source):
        self.json_source = json_source

    def list(self, json_source="", ancestor=""):

        json_source = self.json_source if json_source == "" else json_source

        for item in json_source.keys():
            if type(json_source[item]) is dict:
                json_source[item] = self.list(json_source[item], ancestor+"\\"+item)
            else:
                print("{key}: {val}".format(key=ancestor+"\\"+item, val=json_source[item]))
        return json_source

    def get(self, key):
        _item = self.json_source

        for _key in key.split("\\"):
            if _key == "": continue
            _item = _item[_key]

        return _item

    def add(self, key_search, key, value):
        _item = self.get(key_search)
        _item[key] = value
        return self.json_source

    def delete(self, key):
        _item = self.json_source
        _source = key.split("\\")
        for _key in _source:
            if _key != "" and _source[len(_source)-1] == _key:
                print(_item[_key])
                del _item[_key]
            elif _key != "":
                _item = _item[_key]

        return self.json_source

    def save(self, file_name):
        if os.path.exists(file_name):
            src_env = open(file_name, "r")
            _model = json.load(src_env)
            src_env.close()
        else:
            src_env = open(file_name, "w")
            _model = {}
            src_env.close()

        _model[self.key] = self.__dict__

        src_env = open(file_name, "w")
        src_env.write(unicode(json.dumps(_model, cls=AEMEncoder)))
        src_env.close()


class AEMEncoder(JSONEncoder):
    def default(self, obj):
        if isinstance(obj, (list, dict, str, unicode, int, float, bool, type(None))):
            return JSONEncoder.default(self, obj)
        return obj.__dict__

