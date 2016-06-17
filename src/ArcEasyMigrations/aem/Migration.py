import io
import json
import os
from aem import EasyJSON


class Migration(EasyJSON.EasyJSON):

    key = ""
    script_path = ""
    description = ""
    env_exec = []
    dependent = []

    def __init__(self, key, script_path="", description="", environment_executed=[]):
        self.key = key
        self.script_path = script_path
        self.env_exec = environment_executed
        self.description = description
        self.dependent = []

    def add_migration(self, dependent):
        _dependent = dict()
        _dependent[dependent.key] = dependent
        self.dependent.append(_dependent)

    def list(self, item="", ancestor=""):
        if item == "":
            item = self

        _list_migrations = []
        if isinstance(item, Migration):
            ancestor = ancestor + "\\" + item.key
            _list_migrations.append({"\\" + ancestor: item.description})

        for dependent_migration in item.dependent:
            for _item in self.list(dependent_migration.values()[0], ancestor):
                _list_migrations.append(_item)

        return _list_migrations

    def list_migration_to_execute(self, environment, item="", ancestor="", ):
        if item == "":
            item = self

        _list_migrations = []
        if isinstance(item, Migration):
            ancestor = ancestor + "\\" + item.key
            if environment not in item.env_exec:
                _list_migrations.append({"\\" + ancestor: item.script_path})

        for dependent_migration in item.dependent:
            for _item in self.list_migration_to_execute(environment, dependent_migration.values()[0], ancestor):
                if environment not in item.env_exec:
                    _list_migrations.append(_item)

        return _list_migrations

    def get(self, key):
        _keys = key.split("\\")[1:len(key.split("\\"))]

        _migration = self
        _migration_result = None

        if _keys[0] != _migration.key:
            return None

        for _key in _keys[1:len(_keys)]:
            _migration_result = None
            for _item in _migration.dependent:
                if _key == _item.values()[0].key:
                    _migration_result = _item.values()[0]
                _migration = _migration_result

        return _migration_result


def load_migration_file(key):
    if not os.path.exists("migration.json"):
        return
    src_migration = io.open("migration.json", "r")
    src_json_env = json.load(src_migration)
    src_migration.close()

    _migrations = dict()

    for _item in src_json_env.keys():
        _migration = load_migration(src_json_env[_item])
        _migrations[_migration.key] = _migration

    return _migrations


def load_migration(json_migration):

    _migration = Migration(json_migration["key"],
                           json_migration["script_path"],
                           json_migration["description"],
                           json_migration["env_exec"])

    for _dep_mig in json_migration["dependent"]:
        _migration.add_migration(load_migration(_dep_mig.values()[0]))

    return _migration



