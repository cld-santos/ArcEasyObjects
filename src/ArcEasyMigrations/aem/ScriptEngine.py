import cx_Oracle
import subprocess


class ScriptEngine:

    def run_sql_script(self, script_file, environment):
        con = cx_Oracle.connect(environment.user, environment.password, environment.tns_name)

        full_sql = script_file.read()
        sql_commands = full_sql.split(';')
        curs = con.cursor()

        for sql_command in sql_commands[0:len(sql_commands)-1]:
            sql_command = sql_command.replace('\n', ' ').replace('\r', '')
            print(sql_command)
            if sql_command != "" or sql_command is not None:
                curs.execute(sql_command)

        con.close()

    def run_python_script(self, script_name, environment):

        subprocess.call("python {script} --sde {sde_file}".format(script=script_name, sde_file=environment.sde_filepath))


