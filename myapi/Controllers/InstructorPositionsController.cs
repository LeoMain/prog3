using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace myapi.Controllers
{
    public class InstructorPositionsController : ApiController
    {
        ModelContainer db = new ModelContainer();
        
        public IEnumerable<InstructorPosition> Get()
        {
            return db.InstructorPositions;
        }

        public HttpResponseMessage Get(int id)
        {
            InstructorPosition item = db.InstructorPositions.Find(id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage Post([FromBody]InstructorPosition value)
        {
            try
            {
                db.InstructorPositions.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]InstructorPosition item)
        {
            try
            {
                InstructorPosition i = db.InstructorPositions.Find(id);

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
                var item = db.InstructorPositions.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                int check = db.Instructors.Where(i => i.InstructorPositionId == item.Id).Count();

                if (check > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.InstructorPositions.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}