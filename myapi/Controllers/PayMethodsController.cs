using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace myapi.Controllers
{
    public class PayMethodsController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<PayMethod> Get()
        {
            return db.PayMethods;
        }

        public HttpResponseMessage Get(int id)
        {
            PayMethod item = db.PayMethods.Find(id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage Post([FromBody]PayMethod value)
        {
            try
            {
                db.PayMethods.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]PayMethod item)
        {
            try
            {
                PayMethod i = db.PayMethods.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.Name = item.Name;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var item = db.PayMethods.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (db.RentJournalSet.Where(i => i.PayMethodId == item.Id).Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.PayMethods.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}