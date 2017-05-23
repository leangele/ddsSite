using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ddcSite.db;

namespace ddcSite.Controllers
{
    public class AspUserApiController : ApiController
    {
        // GET: api/AspUserApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AspUserApi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/AspUserApi/userName
        public string Get(string userName)
        {
            if (userName == null)
            {
                return "Error: no data";
            }
            using (var context = new ddcEntities())
            {
                return context.AspNetUsers.FirstOrDefault(x => x.UserName == userName).idClient.ToString();
            }

        }

        // POST: api/AspUserApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AspUserApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AspUserApi/5
        public void Delete(int id)
        {
        }
    }
}
