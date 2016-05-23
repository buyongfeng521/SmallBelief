using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public class EFDALBase<T> where T : class,new()
    {
        DbContext db = new EFDBContextFactory().GetDBContext();


        #region 新增 + int Add(T entity)
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Add(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges();
        }

        #endregion

        #region 批量新增 + int Add(List<T> listEntity)
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public int Add(List<T> listEntity)
        {
            db.Set<T>().AddRange(listEntity);
            return db.SaveChanges();
        }
        #endregion

        #region 修改 + int Modify(T entity)
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Modify(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        #endregion

        #region 删除 + int Delete(T entity)
        public int Delete(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Set<T>().Remove(entity);
            return db.SaveChanges();
        }
        #endregion

        #region 条件删除 + DeleteBy(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="delWhere">删除条件</param>
        /// <returns></returns>
        public int DeleteBy(Expression<Func<T, bool>> delWhere)
        {
            List<T> listDeleting = db.Set<T>().Where(delWhere).ToList();

            listDeleting.ForEach(u =>
            {
                db.Set<T>().Attach(u);
                db.Set<T>().Remove(u);
            });

            return db.SaveChanges();
        }
        #endregion 




        #region 根据条件查询第一的实体 + T GetModelBy(Expression<Func<T, bool>> where)
        /// <summary>
        /// 根据条件查询第一的实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetModelBy(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where).FirstOrDefault();
        }
        #endregion

        #region 根据条件查询 + List<T> GetListBy(Expression<Func<T, bool>> where)
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetListBy(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where).ToList();
        }
        #endregion

        #region 根据条件查询 排序  + List<T> GetListBy<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        /// <summary>
        /// 根据条件查询 + 排序
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<T> GetListBy<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        {
            return db.Set<T>().Where(where).OrderBy(order).ToList();
        }
        #endregion

        #region 根据条件查询 排序（降序）+ List<T> GetListByDesc<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        /// <summary>
        /// 根据条件查询 + 排序（降序）
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> GetListByDesc<Key>(Expression<Func<T, bool>> where, Expression<Func<T, Key>> order)
        {
            return db.Set<T>().Where(where).OrderByDescending(order).ToList();
        }
        #endregion

        #region 分页查询 + List<T> GetPageList<TKey>
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<T> GetPageList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return db.Set<T>().Where(where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 分页查询（降序）+ List<T> GetPageListDesc<TKey>
        /// <summary>
        /// 分页查询（降序）
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> GetPageListDesc<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return db.Set<T>().Where(where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 查询数量 + int GetCountBy(Expression<Func<T, bool>> where)
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetCountBy(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Count(where);
        }
        #endregion

    }
}
