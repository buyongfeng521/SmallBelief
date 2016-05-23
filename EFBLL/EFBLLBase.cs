using EFDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFBLL
{
    public abstract class EFBLLBase<T> where T:class,new()
    {
        protected EFDALBase<T> dal = null;

        public EFBLLBase()
        {
            SetDAL();
        }

        protected abstract void SetDAL();


        public bool Add(T entity)
        {
            return dal.Add(entity) > 0;
        }

        public bool Add(List<T> listEntity)
        {
            return dal.Add(listEntity) > 0;
        }

        public bool Modify(T entity)
        {
            return dal.Modify(entity) > 0;
        }

        public bool Delete(T entity)
        {
            return dal.Delete(entity) > 0;
        }

        public bool DeleteBy(Expression<Func<T,bool>> where)
        {
            return dal.DeleteBy(where) > 0;
        }

        public T GetModelBy(Expression<Func<T, bool>> where)
        {
            return dal.GetModelBy(where);
        }

        public List<T> GetListBy(Expression<Func<T, bool>> where)
        {
            return dal.GetListBy(where);
        }

        public List<T> GetListBy<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        {
            return dal.GetListBy(where, order);
        }

        public List<T> GetListByDesc<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        {
            return dal.GetListByDesc(where, order);
        }

        public List<T> GetPageList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return dal.GetPageList(pageIndex, pageSize, where, order);
        }

        public List<T> GetPageListDesc<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return dal.GetPageListDesc(pageIndex, pageSize, where, order);
        }

        public int GetCountBy(Expression<Func<T, bool>> where)
        {
            return dal.GetCountBy(where);
        }
    }
}
