#region Build Information
// octapush.SPProcessor : QueriesModel.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-18
// CratedTime  : 21:46
#endregion

#region Namespaces
using System;
using LiteDB;

#endregion

namespace octapush.SPProcessor.Models
{
    public class QueryModel
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonId]
        public Guid ApplicationId { get; set; }

        public string Name { get; set; }
        public string Query { get; set; }
        public bool IsActive { get; set; }
    }
}