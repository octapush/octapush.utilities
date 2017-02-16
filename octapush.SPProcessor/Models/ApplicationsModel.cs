#region Build Information
// octapush.SPProcessor : ApplicationsModel.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-18
// CratedTime  : 21:43
#endregion

#region Namespaces
using System;
using LiteDB;

#endregion

namespace octapush.SPProcessor.Models
{
    public class ApplicationsModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool IsActive { get; set; }
    }
}