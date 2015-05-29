// ===================================================================
// Description
// ===================================================================
// gerrywang.net @Copy Right 2010-2011
// Name: DbAccess.cs
// Date: 2011/11/26
// Auth: Gerry Wang
// Spec: 自动生成数据处理基础类 不可修改此文件
// ===================================================================
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace GerryBlog.Models
{
    /// <summary> 
    /// DbAccess 数据库访问类
    /// </summary> 
    public partial class DbAccess
    {
        private String appKeyName = String.Empty;
        private SQLiteConnection sqliteConnection = null;

        public DbAccess(String connectionStringName)
        {
            this.appKeyName = String.IsNullOrEmpty(connectionStringName) ? "sqlite" : connectionStringName;
        }

        /// <summary>
        /// 根据应用程序关键字，从配置文件返回相应数据库连接字符串。
        /// </summary>
        /// <param name="AppKey"></param>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string cs = ConfigurationManager.ConnectionStrings[appKeyName].ConnectionString;
            if (String.IsNullOrEmpty(cs.Trim()))
            {
                cs = ConfigurationManager.AppSettings.Get(appKeyName);
            }
            if (String.IsNullOrEmpty(cs.Trim()))
            {
                throw new Exception("ConnectionString Error !");
            }
            return cs;
        }


        /// <summary>
        /// 根据参数名、数据类型和值获得DbParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter GetParameter(String name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            return param;
        }

        #region 连接的打开关闭
        /// <summary>
        /// 根据应用程序关键字，打开相应的数据库连接
        /// </summary>
        /// <param name="AppKey"></param>
        /// <returns></returns>
        private void OpenConnection()
        {
            if (sqliteConnection == null)
            {
                sqliteConnection = new SQLiteConnection();
            }

            if (sqliteConnection.State != ConnectionState.Open)
            {
                sqliteConnection.ConnectionString = GetConnectionString();
                sqliteConnection.Open();
            }
        }

        /// <summary>
        /// 关闭和释放数据库连接对象
        /// </summary>
        /// <param name="conn"></param>
        private void CloseConnection()
        {
            if (sqliteConnection != null && sqliteConnection.State == ConnectionState.Open)
            {
                sqliteConnection.Close();//关闭连接
                sqliteConnection.Dispose();//释放内存
            }
        }

        public SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = GetConnectionString();
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 关闭一个数据库连接
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConnection(SQLiteConnection conn)
        {
            if (conn.State.ToString().ToLower() == "open")
            {
                //如果连接状态为打开 
                conn.Close();//关闭连接
                conn.Dispose();//释放内存
            }
        }

        #endregion

        #region Data Access

        /// <summary>
        /// GetDataReader by Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IDataReader GetDataReader(string sql)
        {
            OpenConnection();

            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        /// <summary>
        /// GetDataReader by Sql and CommandParameters
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IDataReader GetDataReader(string sql, DbParameter[] commandParameters)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.Parameters.AddRange(commandParameters);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        /// <summary>
        /// GetDataReader by Sql,CommandType and CommandParameters
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IDataReader GetDataReader(string sql, CommandType commandType, DbParameter[] commandParameters)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(commandParameters);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        /// <summary>
        /// GetDataSet by Sql and tableName
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string tableName)
        {
            OpenConnection();
            DataSet ds = new DataSet();

            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, sqliteConnection);
            da.Fill(ds, tableName);

            CloseConnection();
            return ds;
        }
        /// <summary>
        /// GetDataSet by Sql and tableName and CommandParameters
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string tableName, DbParameter[] commandParameters)
        {
            OpenConnection();

            DataSet ds = new DataSet();
            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.Parameters.AddRange(commandParameters);

            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds, tableName);
            CloseConnection();
            return ds;
        }

        /// <summary>
        /// GetExecuteNonQuery by Sql
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetExecuteNonQuery(string sql)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            int flag = cmd.ExecuteNonQuery();
            CloseConnection();
            return flag;
        }

        /// <summary>
        /// GetExecuteNonQuery by Sql and CommandParameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public int GetExecuteNonQuery(string sql, DbParameter[] sqlParameters)
        {
            OpenConnection();
            int flag = 0;

            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.Parameters.AddRange(sqlParameters);
            flag = cmd.ExecuteNonQuery();
            cmd.Dispose();

            CloseConnection();
            return flag;
        }

        /// <summary>
        /// GetExecuteScalar by Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object GetExecuteScalar(string sql)
        {
            OpenConnection();
            object obj = null;

            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);

            obj = cmd.ExecuteScalar();
            cmd.Dispose();

            CloseConnection();
            return obj;
        }
        /// <summary>
        /// GetExecuteScalar by Sql and CommandParameters
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public object GetExecuteScalar(string sql, DbParameter[] commandParameters)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.Parameters.AddRange(commandParameters);

            object obj = cmd.ExecuteScalar();
            cmd.Dispose();
            CloseConnection();
            return obj;
        }

        /// <summary>
        /// InsertAndReturnId by Sql and commandParameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public int InsertAndReturnId(string sql, DbParameter[] sqlParameters)
        {
            if (sql.IndexOf("Select last_insert_rowid()") < 0)
            {
                sql += " ;Select last_insert_rowid()";
            }

            OpenConnection();
            object obj = 0;

            SQLiteCommand cmd = new SQLiteCommand(sql, sqliteConnection);
            cmd.Parameters.AddRange(sqlParameters);
            obj = cmd.ExecuteScalar();
            cmd.Dispose();

            CloseConnection();

            return int.Parse(obj.ToString());
        }

        #endregion
    }

}

