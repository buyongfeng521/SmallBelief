using DapperDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperBLL
{
    public class DapperBLLBase<T> where T:class,new()
    {
        private DapperDALBase<T> dal = null;
        public DapperBLLBase()
        {
            dal = new DapperDALBase<T>();
        }
        /// <summary>
        /// 查询得到第一个实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T QuerySql(string sql, object obj)
        {
            return dal.QuerySql(sql,obj);
        }

        /// <summary>
        /// 查询得到集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<T> QueryListSql(string sql, object obj)
        {
            return dal.QueryListSql(sql,obj);
        }

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ExecuteSql(string sql, object obj)
        {
            return dal.ExecuteSql(sql,obj) > 0;
        }

        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ExecuteTransactionSql(string sql, object obj)
        {
            return dal.ExecuteTransactionSql(sql,obj) > 0;
        }
    }
}
