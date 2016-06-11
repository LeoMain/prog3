using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using myapi.Models;
using Newtonsoft.Json;

namespace myapi.Controllers
{
    public class InstructorsController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public InstructorsController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<InstructorDTO> Get()
        {            
            return db.Instructors
                .Select(i =>
                    new InstructorDTO()
                    {
                        Id = i.Id,
                        FIO = i.FIO,
                        InstructorPosition = db.InstructorPositions.FirstOrDefault(p => p.Id == i.InstructorPositionId).Name
                    })
                .ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            InstructorDTO item = db.Instructors.Select(i =>
                    new InstructorDTO()
                    {
                        Id = i.Id,
                        FIO = i.FIO,
                        InstructorPosition = db.InstructorPositions.FirstOrDefault(p => p.Id == i.InstructorPositionId).Name
                    }).SingleOrDefault(i => i.Id == id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);//instructor;
        }

        public HttpResponseMessage Post([FromBody]Instructor value)
        {            
            try
            {
                db.Instructors.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Instructor item)
        {
            try
            {                
                Instructor i = db.Instructors.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.FIO = item.FIO;
                i.InstructorPositionId = item.InstructorPositionId;
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
                var item = db.Instructors.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                //if (item.Enrollments.Count > 0)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete course, students has enrollments in course.");
                //}

                db.Instructors.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}