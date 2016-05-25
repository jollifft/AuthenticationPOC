using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MaterialTest
{
    public interface IBaaS
    {
        void DefineTable<T>(bool isSynced = true);
        Task InitializeAsync();
        Task SyncDataAsync<T>(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetDataAsync<T>(Expression<Func<T, bool>> predicate = null, int take = 100);
        Task<T> WriteDataAsync<T>(T data) where T : ITableData;
    }
}

