using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace myapi.Controllers
{
    public class InstructionTypesController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<InstructionType> Get()
        {
            return db.InstructionTypes;
        }

        public HttpResponseMessage Get(int id)
        {
            var item = db.InstructionTypes.Find(id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage Post([FromBody]InstructionType value)
        {
            try
            {
                db.InstructionTypes.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]InstructionType item)
        {
            try
            {
                var i = db.InstructionTypes.Find(id);

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
                var item = db.InstructionTypes.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (db.RentJournalSet.Where(i => i.InstructionTypeId == item.Id).Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.InstructionTypes.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}