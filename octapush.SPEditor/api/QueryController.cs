using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using octapush.SPProcessor;
using octapush.SPProcessor.Models;

namespace octapush.SPEditor.api
{
    public class QueryController : ApiController
    {
        readonly QueryProcessor _qp = new QueryProcessor(ConfigurationManager.AppSettings["SPStoragePath"]);

        // GET api/query
        public List<QueryModel> Get(Guid appId)
        {
            return _qp.Gets(appId);
        }

        // GET api/query/5
        public string Get(Guid appId, Guid id)
        {
            return "value";
        }

        // POST api/query
        public ApiOutputModel Post(Guid appId, [FromBody]QueryModel value)
        {
            return _qp.Insert(appId, value);
        }

        // PUT api/query/5
        public ApiOutputModel Put(Guid appId, Guid id, [FromBody]QueryModel value)
        {
            return _qp.Update(appId, id, value);
        }

        // DELETE api/query/5
        public ApiOutputModel Delete(Guid appId, Guid id)
        {
            return _qp.Delete(appId, id);
        }
    }
}
