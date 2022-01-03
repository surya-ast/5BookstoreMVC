using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace bookStoreV2.DataAccess.Repository.IRepository
{
    //kita definisikan class sebagai generic Class dengan type T, krn IRepository akan dipanggil utk seluruh objek yg ada (yg disimpan sbg class Model)
    public interface IRepository<T> where T : class
    {
        //bikin utk memanggil 1 entity berdasar Id
        T Get(int id);

        //bikin untuk memanggil seluruh entity dan menyediakan clause where/filter dan order by
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
            string includeProperties = null
            );

        //buat utk memanggil 1 entity menggunakan clause where/filter
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        //bikin utk menambah entity
        void Add(T entity);

        //bikin utk menghapus entity by Id
        void Remove(int id);

        //bikin utk menghapus entity langsung
        void Remove(T entity);

        //bikin utk menghapus beberapa entity sekaligus
        void RemoveRange(IEnumerable<T> entity);

    }
}
