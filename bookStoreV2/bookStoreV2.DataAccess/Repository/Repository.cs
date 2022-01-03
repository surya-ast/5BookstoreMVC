using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using bookStoreV2.DataAccess.Data;
using bookStoreV2.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace bookStoreV2.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //krn semua fungsi disini akan berhubungan dan mungkin mengubah DB, maka perlu DBContext
        //drpd menggunakan DbContext yg di dapat langsung dari Entity Framework Core,
        //kita bisa menggunakan ApplicationDbContext yg sudah implement IdentityDbContext dan dapat digunakan langsung utk mengakses DB
        //private readonly DbContext _db;
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        //constructor utk initial db dan dbset
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //cek apakah filter ada isinya ato ndak, kalo ada isinya, filter dimasukkan ke dalam query
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //selanjutnya kita perlu eager loading.
            //kita cek dulu, apakah properti bawaannya ada isinya atau tidak (includeProperties)
            //jika hasilnya adalah != null, maka kita perlu eager loading
            //eager loading adalah jika kita punya PRODUK, maka dalam suatu PRODUK kita punya CategoryID
            //CategoryID tsb merupakan ForeignKeu Reference ke CategoryID dalam CATEGORY yg menghubungkan PRODUK dg CATEGORY
            //sehingga, saat kita membuka PRODUK kita juga perlu membuka CATEGORY,
            //jadi berdasarkan CategoryId tsb, kita bisa menampilkan CategoryName
            //tahapan tsb ingin kita lakukan dalam SATU KALi LOAD
            //hubungan antar tabel tsb, ingin kita tampilkan dalam suatu string yg dipisahkan dengan COMMA (,)

            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    //jika ditemukan, maka property tsb ditambahkan ke dalam query
                    query = query.Include(includeProp);
                }
            }

            //menangani order by
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            //sehingga disini, kita bisa mengembalikan IEnumberable dari object berdasarkan kondisi yang disyaratkan
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            //sama dengan GetAll namun tidak mengembalikan IEnumberable, krn hanya 1 object saja yg dikembalikan
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            //cari entity yg akan dihapus, baru di hapus
            T entity = dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
