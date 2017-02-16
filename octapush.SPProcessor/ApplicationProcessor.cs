#region Build Information
// octapush.SPProcessor : ApplicationProcessor.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-18
// CratedTime  : 22:01
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
    public class ApplicationProcessor
    {
        #region CTOR
        private const string TableName = "Applications";
        private readonly string _repositoryPath;

        public ApplicationProcessor(string repositoryPath)
        {
            if (repositoryPath.IsNullOrEmpty())
                throw new Exception("RepositoryPath is not defined.");

            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);

            _repositoryPath = Path.Combine(repositoryPath, "SPStorage.DB");
        }
        #endregion CTOR

        #region PRIVATE
        private void DoIndexing(LiteCollection<ApplicationsModel> collections)
        {
            collections.EnsureIndex(x => x.Id);
            collections.EnsureIndex(x => x.Name);
            collections.EnsureIndex(x => x.ConnectionString);
            collections.EnsureIndex(x => x.IsActive);
        }
        #endregion PRIVATE

        #region PUBLIC
        public List<ApplicationsModel> Gets()
        {
            List<ApplicationsModel> lApp;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lApp = db
                    .GetCollection<ApplicationsModel>(TableName)
                    .FindAll()
                    .ToList();
            }
            return lApp;
        }

        public ApplicationsModel Get(Guid id)
        {
            ApplicationsModel lApp;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lApp = db
                    .GetCollection<ApplicationsModel>(TableName)
                    .FindOne(x => x.Id == id);
            }

            return lApp;
        }

        public ApplicationsModel GetByName(string appName)
        {
            ApplicationsModel lApp;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                lApp = db
                    .GetCollection<ApplicationsModel>(TableName)
                    .FindOne(x => x.Name == appName);
            }

            return lApp;
        }

        public ApiOutputModel Insert(ApplicationsModel data)
        {
            if (data == null)
                return new ApiOutputModel {Result = EnumSpProcessorCallResult.InvalidSuppliedData};

            if (GetByName(data.Name) != null)
                return new ApiOutputModel {Result = EnumSpProcessorCallResult.DataConflict};

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lApp = db.GetCollection<ApplicationsModel>(TableName);

                data.Id = new Guid();

                lApp.Insert(data);
                DoIndexing(lApp);
            }

            return new ApiOutputModel {Result = EnumSpProcessorCallResult.Success, Supplement = data};
        }

        public ApiOutputModel Update(Guid id, ApplicationsModel data)
        {
            if (data == null)
                return new ApiOutputModel {Result = EnumSpProcessorCallResult.InvalidSuppliedData};

            data.Id = id;

            // make sure we not lost old data fields
            var oldData = Get(data.Id);
            if (oldData == null)
                return new ApiOutputModel {Result = EnumSpProcessorCallResult.SourceNotFound};

            data.Name = data.Name ?? oldData.Name;
            data.ConnectionString = data.ConnectionString ?? oldData.ConnectionString;

            using (var db = new LiteDatabase(_repositoryPath))
            {
                var lApp = db.GetCollection<ApplicationsModel>(TableName);
                lApp.Update(data);
            }

            return new ApiOutputModel {Result = EnumSpProcessorCallResult.Success, Supplement = data};
        }

        public ApiOutputModel Delete(Guid id)
        {
            using (var db = new LiteDatabase(_repositoryPath))
            {
                // remove all childs data first
                var qp = new QueryProcessor(Path.GetDirectoryName(_repositoryPath));
                qp.DeleteAll(id);

                var lApp = db.GetCollection<ApplicationsModel>(TableName);
                lApp.Delete(x => x.Id == id);

                db.Shrink();
            }

            return new ApiOutputModel{Result = EnumSpProcessorCallResult.Success};
        }
        #endregion PUBLIC
    }
}