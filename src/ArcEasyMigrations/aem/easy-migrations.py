# -*- coding: utf-8 -*-
import argparse
import aem
import io
import re
from aem.Environment import Environment
from aem.Migration import Migration
from aem.ScriptEngine import ScriptEngine

ENVIRONMENT_FILE = "environment.json"
MIGRATION_FILE = "migration.json"


def add_migration(args):
    _migration = aem.Migration.load_migration_file(MIGRATION_FILE)

    if args.ancestor is None:
        _migration = Migration(args.name, args.file, args.description)
        _migration.save(MIGRATION_FILE)
    else:
        _migration[args.ancestor.split("\\")[0]].get(args.ancestor).add_migration(Migration(unicode(args.name), args.file, unicode(args.description)))
        _migration[args.ancestor.split("\\")[0]].save(MIGRATION_FILE)


def list_all_migration(args):
    _migrations = aem.Migration.load_migration_file(MIGRATION_FILE)
    for _migration in _migrations:
        for _item in _migrations[_migration].list():
            print(_item.keys()[0])
            print(_item.values()[0])


def add_environment(args):
    env = Environment(args.name)

    env.set_sde_file(args.sde_file)
    env.set_db_paramns(args.user, args.password, args.tns_name)

    env.save(ENVIRONMENT_FILE)


def deploy(args):

    _env = aem.Environment.load_environment(args.environment_name)
    _migrations = aem.Migration.load_migration_file(MIGRATION_FILE)

    _list_all_migrations = []

    for _migration in _migrations:
        for _item in _migrations[_migration].list_migration_to_execute(_env.key):
            _list_all_migrations.append(_item)

    print(u"Ambiente à ser executado: " + _env.key)

    for _item in _list_all_migrations:
        script_name = _item.values()[0]
        script_engine = ScriptEngine()

        if re.match(r'(.*\.sql)$', script_name):
            script_file = io.open(script_name)
            script_engine.run_sql_script(script_file, _env)
        elif re.match(r'(.*\.py)$', script_name):
            script_engine.run_python_script(script_name, _env)



if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Provisionamento de Geodatabase.')
    subparser = parser.add_subparsers()

    _list = subparser.add_parser('list', help="List all Migrations")
    _list.add_argument('--key', required=False, help='Searchable key "\\node_main\\node_son".')
    _list.set_defaults(func=list_all_migration)

    _add_env = subparser.add_parser('add_env', help="Adds a Environment to provision a geodatabase")
    _add_env.add_argument('--name', required=True, help='Searchable key "\\node_main\\node_son".')
    _add_env.add_argument('--tns_name', required=True, help='Searchable key "\\node_main\\node_son".')
    _add_env.add_argument('--user', required=True, help='Searchable key "\\node_main\\node_son".')
    _add_env.add_argument('--password', required=True, help='Searchable key "\\node_main\\node_son".')
    _add_env.add_argument('--sde_file', required=True, help='Searchable key "\\node_main\\node_son".')
    _add_env.set_defaults(func=add_environment)

    _add_migration = subparser.add_parser("add", help="Adds a Migration")
    _add_migration.add_argument('--name', required=True, help='Name of a Migration')
    _add_migration.add_argument('--file', required=True, help='File Path to the script.')
    _add_migration.add_argument('--description', required=True, help='Description of a migration')
    _add_migration.add_argument('--ancestor', required=False, help='Migrations Ancestor, that depends upon to execute.')
    _add_migration.set_defaults(func=add_migration)

    _deploy = subparser.add_parser('deploy', help="List all Migrations")
    _deploy.add_argument('--environment_name', required=True, help='Searchable key "\\node_main\\node_son".')
    _deploy.set_defaults(func=deploy)

    args = parser.parse_args()
    args.func(args)
