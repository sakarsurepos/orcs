using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Odbc;

namespace Robot
{
    public static class DB_mysql
    {
        //mysql commands
        public static string ConnectionString = @"driver={MySQL ODBC 3.51 Driver};server=localhost;database=web_control;uid=root;pwd=indiana;";
        public static string CommandText = "select * from web_control_table order by notice limit 10"; //Select *from emp where empno=200  //select * from table1; select * from table2
        public static string CommandText2 = "select * from test2";
        public static string CommandText_drop = "DROP TABLE IF EXISTS test2";
        public static string CommandText_create = "CREATE TABLE test2(id int not null primary key, name varchar(20))";
        public static string CommandText_open = "INSERT INTO test2(id,name) values(5,'close')";
        public static string CommandText_close = "INSERT INTO test2(id,name) values(5,'open')";
        public static string CommandText_update_open = "UPDATE test2 SET name='open' WHERE id=1"; //"txt text, dt date, tm time, ts timestamp)"
        public static string CommandText_update_close = "UPDATE test2 SET name='close' WHERE id=1";//UPDATE test2 SET dt='12:30' WHERE id=1"; //"txt text, dt date, tm time, ts timestamp)"

        public static OdbcConnection myConnection = new OdbcConnection(ConnectionString); //connection
        public static OdbcCommand myCommand = new OdbcCommand(CommandText, myConnection); //command
        public static OdbcCommand myCommand2 = new OdbcCommand(CommandText2, myConnection); //command
        public static OdbcCommand myCommand_open = new OdbcCommand(CommandText_open, myConnection); //command
        public static OdbcCommand myCommand_close = new OdbcCommand(CommandText_close, myConnection); //command
        public static OdbcCommand myCommand_drop = new OdbcCommand(CommandText_drop, myConnection); //command drop
        public static OdbcCommand myCommand_create = new OdbcCommand(CommandText_create, myConnection); //command drop
        public static OdbcCommand myCommand_update_open = new OdbcCommand(CommandText_update_open, myConnection); //command drop
        public static OdbcCommand myCommand_update_close = new OdbcCommand(CommandText_update_close, myConnection); //command drop
        //mysq commands end

        //mysql commands store time
        public static string CommandText_update_close2 = "UPDATE test2 SET dt='" + DateTime.Now.ToLongTimeString() + "' WHERE id=1"; //
        public static OdbcCommand myCommand_update_close2 = new OdbcCommand(CommandText_update_close2, myConnection); //command drop
        //mysql commands store time

        //mysql store user

        //mysql store user
    }
}
