using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace MaterialTest
{
    /// <summary>
    /// Implementation of the IBaaS interface for Microsoft.Azure.Mobile.Client SDK
    /// </summary>
	public class AzureService : IBaaS
    {
        private static IMobileServiceClient _azureClient;
		public IMobileServiceClient AzureClient {
			get { return _azureClient; }
		}

        private IMobileServiceLocalStore _store;
        private List<object> _syncTables;
        private int _timeout;

        /// <summary>
        /// Sets up your Azure service with offline support
        /// </summary>
        /// <param name="client">Inject IMobileServiceClient here</param>
        /// <param name="store">Inject MobileServiceSQLiteStore</param>
        /// <param name="timeout">How long you care to wait before results return.</param>
		public AzureService(IMobileServiceClient client, IMobileServiceLocalStore store, int timeout = 7000)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _azureClient = client;
            _store = store;
            _syncTables = new List<object>();
            _timeout = timeout;
        }

        #region IBaaS implementation

        /// <summary>
        /// Defines sync tables and then you can pass in whether you actually want to sync them or not.
        /// </summary>
        /// <param name="isSynced">If set to <c>true</c> is synced with Azure.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void DefineTable<T>(bool isSynced = true)
        {
            var store = _store as MobileServiceSQLiteStore;

            if (_store != null)
            {
                store.DefineTable<T>();
            }

            if (isSynced)
            {
                _syncTables.Add(_azureClient.GetSyncTable<T>());
            }
        }

        /// <summary>
        /// Initialize your SyncContext, this should be done AFTER you have defined your tables
        /// </summary>
        public async Task InitializeAsync()
        {
            await _azureClient.SyncContext.InitializeAsync(_store);
        }

        /// <summary>
        /// Syncs the data.
        /// </summary>
        /// /// <param name="predicate">Applies the specified filter predicate to the source query.</param>
        /// <typeparam name="T">Data type you are syncing.</typeparam>
        public async Task SyncDataAsync<T>(Expression<Func<T, bool>> predicate = null)
        {
            var cts = new CancellationTokenSource();
            using (cts)
            {
                IMobileServiceSyncTable<T> syncTable;
                try
                {
                    syncTable = _syncTables.OfType<IMobileServiceSyncTable<T>>().First();
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("It doesn't appear that you defined that table as isSynced", ex);
                }

                var query = syncTable.CreateQuery();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var task = syncTable.PullAsync(typeof(T).Name, query, cts.Token); //Will only pull updated items from the last time we pulled typeof(T).Name query

                if (await Task.WhenAny(task, Task.Delay(_timeout, cts.Token)) == task)
                {
                    if (task.Status != TaskStatus.RanToCompletion)
                    {
                        throw new InvalidOperationException("SyncData did not complete successfully", task.Exception);
                    }
                }
                else
                {
                    throw new TimeoutException("SyncData did not complete within the timout period");
                }
                cts.Cancel();
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="predicate">Applies the specified filter predicate to the source query.</param>
        /// <param name="take">Limit returned results to this amount.</param>
        /// <typeparam name="T">Data type you are expecting.</typeparam>
        public async Task<IEnumerable<T>> GetDataAsync<T>(Expression<Func<T, bool>> predicate = null, int take = 100)
        {
            if (!_azureClient.SyncContext.IsInitialized)
            {
                throw new InvalidOperationException("SyncContext must first be initialized");
            }

            var query = _azureClient.GetSyncTable<T>().CreateQuery();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var results = await query.Take(take).ToEnumerableAsync();

            return results;
        }

        /// <summary>
        /// Writes data to the local store. Write will not sync to Azure until SyncData is called
        /// </summary>
        /// <returns>The object you just wrote.</returns>
        /// <param name="data">Data to write.</param>
        /// <typeparam name="T">Data type your are writing.</typeparam>
        public async Task<T> WriteDataAsync<T>(T data) where T : ITableData
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (!_azureClient.SyncContext.IsInitialized)
            {
                throw new InvalidOperationException("SyncContext must first be initialized");
            }

            var table = _azureClient.GetSyncTable<T>();

            if (string.IsNullOrEmpty(data.Id))
            {
                await table.InsertAsync(data);
            }
            else
            {
                await table.UpdateAsync(data);
            }

            return data;
        }

        /// <summary>
        /// Deletes data from the local store. Delete will not sync to Azure until SyncData is called
        /// </summary>
        /// <param name="data">Data to delete.</param>
        /// <typeparam name="T">Data type you are deleting.</typeparam>
        public async Task DeleteDataAsync<T>(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (!_azureClient.SyncContext.IsInitialized)
            {
                throw new InvalidOperationException("SyncContext must first be initialized");
            }

            var table = _azureClient.GetSyncTable<T>();
            await table.DeleteAsync(data);
        }

        #endregion
    }

}



