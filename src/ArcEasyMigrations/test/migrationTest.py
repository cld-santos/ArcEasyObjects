# -*- coding: utf-8 -*-
import unittest
import aem
from aem.Migration import Migration


class MigrationTest(unittest.TestCase):

    def test_deve_adicionar_uma_migration(self):
        _migrations = aem.Migration.load_migration_file("migration.json")
        _dependent = Migration("ef_29", "e:\\csantos\\migration\\test_noventa.sql", u"maluquice rapá")
        _migrations["ef_01"].add_migration(_dependent)
        _migrations["ef_01"].save("migration.json")
        self.assertTrue(len(_migrations["ef_01"].dependent) > 0)

    def test_deve_salvar_um_migration(self):
        _migration = Migration("ef_01", "e:\\csantos\\migration\\test.sql", u"Script de Atualização do monstro")
        _migration.add_migration(Migration("ef_21", "e:\\csantos\\migration\\test_dois.sql",u"Script de Atualizacao do maluco"))
        _migration.add_migration(Migration("ef_22", "e:\\csantos\\migration\\test_tres.sql",u"Script de Atualizacao do doidao"))
        _migration.save("migration.json")

        _migration = Migration("ef_50", "e:\\csantos\\migration\\test.sql", u"Script de Atualização do monstro")
        _migration.add_migration(Migration("ef_21", "e:\\csantos\\migration\\test_dois.sql", u"Script de Atualizacao do maluco"))
        _migration.save("migration.json")


    def test_deve_carregar_uma_migration_existente(self):
        _migrations = aem.Migration.load_migration_file("migration.json")
        for _migration in _migrations:
            _migrations[_migration].list()
        self.assertTrue(len(_migrations["ef_50"].dependent) > 0)

    def test_obter_uma_migration_por_chave(self):

        _description = "testando"

        _migrations = aem.Migration.load_migration_file("migration.json")

        for _migration in _migrations.values():
            _item = _migration.get("\\ef_01\\ef_21")

            if _item is None: continue
            _item.add_migration(Migration("ef_50", "e:\\zlalala\\lalala\\sql.sql", _description))
            _migration.save("migration.json")

            _migrations_B = aem.Migration.load_migration_file("migration.json")
            _item_B = None
            for _migration_b in _migrations_B.values():
                _item_B = _migration_b.get("\\ef_01\\ef_21\\ef_50")
                if _item_B is not None:
                    break

            self.assertTrue(_item_B is not None)
