#region Build Information
// octapush.SPProcessor : QueryExecutor.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-21
// CratedTime  : 12:23
#endregion

#region Namespaces
using System;
using System.Data;
using System.IO;
using octapush.Utilities.DbHelper;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.SPProcessor
{
    internal class SpExecutor
    {
        #region CTOR
        private readonly ApplicationProcessor _appProcessor;
        private readonly QueryProcessor _queryProcessor;

        public SpExecutor(string repositoryPath)
        {
            if (repositoryPath.IsNullOrEmpty())
                throw new Exception("RepositoryPath is not defined.");

            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);

            repositoryPath = Path.Combine(repositoryPath, "SPStorage.DB");

            _appProcessor = new ApplicationProcessor(repositoryPath);
            _queryProcessor = new QueryProcessor(repositoryPath);
        }
        #endregion CTOR

        #region PUBLIC
        public DataTable ExecQuery(string appName, string spName)
        {
            var a = _appProcessor.GetByName(appName);
            var q = _queryProcessor.GetByName(appName, spName);

            return q.Query.ExecQuery(a.ConnectionString);
        }

        public object ExecScalar(string appName, string spName)
        {
            var a = _appProcessor.GetByName(appName);
            var q = _queryProcessor.GetByName(appName, spName);

            return q.Query.ExecScalar(a.ConnectionString);
        }

        public void ExecNonQuery(string appName, string spName)
        {
            var a = _appProcessor.GetByName(appName);
            var q = _queryProcessor.GetByName(appName, spName);

            q.Query.ExecNonQuery(a.ConnectionString);
        }
        #endregion PUBLIC
    }
}