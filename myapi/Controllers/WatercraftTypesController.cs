using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace myapi.Controllers
{
    public class WatercraftTypesController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<WatercraftType> Get()
        {
            return db.WatercraftTypes;
        }

        public HttpResponseMessage Get(int id)
        {
            WatercraftType item = db.WatercraftTypes.Find(id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage Post([FromBody]WatercraftType value)
        {
            try
            {
                db.WatercraftTypes.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]WatercraftType item)
        {
            try
            {
                WatercraftType i = db.WatercraftTypes.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.Type = item.Type;
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
                var item = db.WatercraftTypes.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (db.WaterCrafts.Where(i => i.WatercraftTypeId == item.Id).Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.WatercraftTypes.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}