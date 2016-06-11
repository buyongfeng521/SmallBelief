using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public class EFDBContextFactory
    {
        //public DbContext GetDBContext()
        //{
        //    return new SmallBeliefEntities();
        //}

        /// <summary>
        /// 创建 EF上下文 对象，在线程中共享 一个 上下文对象
        /// </summary>
        /// <returns></returns>
        public DbContext GetDBContext()
        {
            //从当前线程中 获取 EF上下文对象
            DbContext dbContext = CallContext.GetData(typeof(EFDBContextFactory).Name) as DbContext;
            if (dbContext == null)
            {
                dbContext = new SmallBeliefEntities();
                CallContext.SetData(typeof(EFDBContextFactory).Name, dbContext);
            }
            return dbContext;
        } 

    }
}
