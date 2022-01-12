using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;

namespace bookStoreV2.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        //untuk return single value: count/any first value = int/boolean value
        T Single<T>(string procedureName, DynamicParameters param = null);

        //untuk common skenario: add, delete, eksekusi kegiatan yg tidak membutuhkan return
        void Execute(string procedureName, DynamicParameters param = null);

        //untuk retrieve one complete row/record
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        //untuk retrieve all rows
        IEnumerable<T> List<T> (string procedureName, DynamicParameters param = null);

        //untuk retrieve SP dengan 2 tabel
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
