using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ddcApi.Controllers
{
    [EnableCors("*","*","*")]
    public class usersController : ApiController
    {
        // GET: api/users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/users/5
        public string Get(string id)
        {
            if (id == null)
            {
                return "Error: no data";
            }
            using (var context = new ddcEntities())
            {
                var userNameReturn = context.AspNetUsers.FirstOrDefault(x => x.Id == id);
                return userNameReturn != null ? userNameReturn.idClient.ToString() : "no data";
            }
        }

        // GET: api/users/5
        //public string Get(string userName)
        //{
        //    if (userName == null)
        //    {
        //        return "Error: no data";
        //    }
        //    using (var context = new ddcEntities())
        //    {
        //        var userNameReturn = context.AspNetUsers.FirstOrDefault(x => x.Id == userName);
        //        return userNameReturn != null ? userNameReturn.idClient.ToString() : "no data";
        //    }
        //}
        // POST: api/users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/users/5
        public void Delete(int id)
        {
        }
    }
}
