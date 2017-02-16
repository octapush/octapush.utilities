using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using octapush.SPProcessor;
using octapush.SPProcessor.Models;

namespace octapush.SPEditor.api
{
    public class ApplicationController : ApiController
    {
        readonly ApplicationProcessor _ap = new ApplicationProcessor(ConfigurationManager.AppSettings["SPStoragePath"]);

        public List<ApplicationsModel> Get()
        {
            return _ap.Gets();
        }

        public ApplicationsModel Get(Guid id)
        {
            return _ap.Get(id);
        }

        public ApiOutputModel Post([FromBody]ApplicationsModel value)
        {
            return _ap.Insert(value);
        }

        public ApiOutputModel Put([FromBody]ApplicationsModel value)
        {
            return _ap.Update(value.Id, value);
        }

        public ApiOutputModel Delete(Guid id)
        {
            return _ap.Delete(id);
        }
    }
}
