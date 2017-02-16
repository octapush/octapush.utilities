#region Build Information
// octapush.SPProcessor : QueryProcessor.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-21
// CratedTime  : 11:45
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteDB;
using octapush.SPProcessor.Enums;
using octapush.SPProcessor.Models;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.SPProcessor
{
    public class QueryProcessor
    {
        #region CTOR
        private const string TableName = "Queries";
        private readonly string _repositoryPath;

        public QueryProcessor(string repositoryPath)
        {
            if (repositoryPath.IsNullOrEmpty())
                throw new Exception("RepositoryPath is not defined");

            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);

            _repositoryPath = Path.Combine(repositoryPath, "SPStorage.DB");
        }
        #endregion CTOR

        #region PRIVATE
        private void DoIndexing(LiteCollection<QueryModel> collections)
        {
            collections.EnsureIndex(x => x.Id);
            collections.EnsureIndex(x => x.ApplicationId);
            collections.EnsureIndex(x => x.Name);
            collections.EnsureIndex(x => x.Query);
            collections.EnsureIndex(x => x.IsActive);
        }
        #endregion PRIVATE

        #region PUBLIC
        public List<QueryModel> Gets(Guid appId)
        {
            List<QueryModel> lQue;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lQue = db
                    .GetCollection<QueryModel>(TableName)
                    .FindAll()
                    .Where(x => x.ApplicationId == appId)
                    .ToList();
            }

            return lQue;
        }

        public QueryModel Get(Guid appId, Guid id)
        {
            QueryModel lQue;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lQue = db
                    .GetCollection<QueryModel>(TableName)
                    .FindOne(x => x.ApplicationId == appId && x.Id == id);
            }

            return lQue;
        }

        public QueryModel GetByName(string appName, string queryName)
        {
            QueryModel lQue;
            var app = new ApplicationProcessor(_repositoryPath).GetByName(appName);

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lQue = db
                    .GetCollection<QueryModel>(TableName)
                    .FindOne(x => x.ApplicationId == app.Id && x.Name == queryName);
            }

            return lQue;
        }

        public ApiOutputModel Insert(Guid appId, QueryModel data)
        {
            if (data == null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.InvalidSuppliedData};

            var app = new ApplicationProcessor(Path.GetDirectoryName(_repositoryPath)).Get(appId);
            if (app == null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.SourceNotFound};

            if (GetByName(app.Name, data.Name) != null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.DataConflict};

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lQue = db.GetCollection<QueryModel>(TableName);

                data.Id = new Guid();
                data.ApplicationId = appId;

                lQue.Insert(data);
                DoIndexing(lQue);
            }

            return new ApiOutputModel{Result = EnumSpProcessorCallResult.Success, Supplement = data};
        }

        public ApiOutputModel Update(Guid appId, Guid id, QueryModel data)
        {
            if (data == null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.InvalidSuppliedData};

            var app = new ApplicationProcessor(Path.GetDirectoryName(_repositoryPath)).Get(appId);
            if (app == null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.SourceNotFound};

            if (GetByName(app.Name, data.Name) != null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.DataConflict};

            data.Id = id;
            data.ApplicationId = appId;

            // make sure we not lost any old data fields
            var oldData = Get(appId, id);
            if (oldData == null)
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.SourceNotFound};

            data.Name = data.Name ?? oldData.Name;
            data.Query = data.Query ?? oldData.Query;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lQue = db.GetCollection<QueryModel>(TableName);
                lQue.Update(data);
            }

            return new ApiOutputModel{Result = EnumSpProcessorCallResult.Success, Supplement = data};
        }

        public ApiOutputModel Delete(Guid appId, Guid id)
        {
            var que = Get(appId, id);
            if (que == null) 
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.InvalidSuppliedData};

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lQue = db.GetCollection<QueryModel>(TableName);
                lQue.Delete(x => x.ApplicationId == appId && x.Id == id);
                db.Shrink();
            }

            return new ApiOutputModel{Result = EnumSpProcessorCallResult.Success};
        }

        public ApiOutputModel DeleteAll(Guid appId)
        {
            var app = new ApplicationProcessor(Path.GetDirectoryName(_repositoryPath)).Get(appId);
            if (app == null) 
                return new ApiOutputModel{Result = EnumSpProcessorCallResult.SourceNotFound};

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lQue = db.GetCollection<QueryModel>(TableName);
                lQue.Delete(x => x.ApplicationId == appId);
                db.Shrink();
            }

            return new ApiOutputModel{Result = EnumSpProcessorCallResult.Success};
        }
        #endregion PUBLIC
    }
}