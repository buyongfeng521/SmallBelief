using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Configuration;

namespace DapperDAL
{
    public class DapperDALBase<T> where T:class,new()
    {
        private static ConnectionStringSettings connStr = ConfigurationManager.ConnectionStrings["SmallBeliefConn"];
        /// <summary>
        /// 写连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetWriteConnection()
        {
            DbConnection connection = DbProviderFactories.GetFactory(connStr.ProviderName).CreateConnection();
            connection.ConnectionString = connStr.ConnectionString;
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 读连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetReadConnection()
        {
            try
            {
                DbConnection connection = DbProviderFactories.GetFactory(connStr.ProviderName).CreateConnection();
                connection.ConnectionString = connStr.ConnectionString;
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                return GetWriteConnection();
            }
        }

        /// <summary>
        /// 查询得到第一个实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T QuerySql(string sql, object obj)
        {
            using (DbConnection connection = GetReadConnection())
            {
                var model =
                    connection.Query<T>(sql, obj)
                        .FirstOrDefault();
                connection.Close();
                return model;
            }
        }

        /// <summary>
        /// 查询得到集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<T> QueryListSql(string sql, object obj)
        {
            using (DbConnection connection = GetReadConnection())
            {
                List<T> list = connection.Query<T>(sql, obj).ToList();
                connection.Close();
                return list;
            }
        }

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, object obj)
        {
            using (DbConnection connection = GetWriteConnection())
            {
                int result =
                    connection.Execute(sql, obj);
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int ExecuteTransactionSql(string sql, object obj)
        {
            using (DbConnection connection = GetWriteConnection())
            {
                var trans = connection.BeginTransaction();
                var rows = connection.Execute(sql, obj, trans);
                if (rows > 0)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
                connection.Close();
                return rows;
            }
        }

    }
}
