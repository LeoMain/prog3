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
    public class JournalController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<JournalDTO> Get()
        {
            return db.RentJournalSet
                .ToList()
                .Select(i =>
                    new JournalDTO()
                    {
                        Id = i.Id,
                        Client = db.Clients.FirstOrDefault(p => p.Id == i.ClientId).FIO,
                        Water = db.Waters.FirstOrDefault(p => p.Id == i.WaterId).Name,
                        PayMethod = db.PayMethods.FirstOrDefault(p => p.Id == i.PayMethodId).Name,
                        WaterCraftName = db.WaterCrafts.FirstOrDefault(p => p.Id == i.WaterCraftId).Name,
                        WaterCraftType = db.WatercraftTypes.FirstOrDefault(x => x.Id == i.WaterCraftId).Type,
                        InstructionType = db.InstructionTypes.FirstOrDefault(p => p.Id == i.InstructionTypeId).Name,
                        Instructor = db.Instructors.FirstOrDefault(p => p.Id == i.InstructorId).FIO,
                        Date = i.Date,
                        Cost = i.Cost
                    });
        }

        public HttpResponseMessage Get(int id)
        {
            var item = db.RentJournalSet
                .ToList()
                .Select(i =>
                    new JournalDTO()
                    {
                        Id = i.Id,
                        Date = i.Date,
                        Cost = i.Cost,
                        Client = db.Clients.FirstOrDefault(p => p.Id == i.ClientId).FIO,
                        Water = db.Waters.FirstOrDefault(p => p.Id == i.WaterId).Name,
                        PayMethod = db.PayMethods.FirstOrDefault(p => p.Id == i.PayMethodId).Name,
                        WaterCraftName = db.WaterCrafts.FirstOrDefault(p => p.Id == i.WaterCraftId).Name,
                        WaterCraftType = db.WatercraftTypes.FirstOrDefault(x => x.Id == i.WaterCraftId).Type,
                        InstructionType = db.InstructionTypes.FirstOrDefault(p => p.Id == i.InstructionTypeId).Name,
                        Instructor = db.Instructors.FirstOrDefault(p => p.Id == i.InstructorId).FIO
                    }).SingleOrDefault(i => i.Id == id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);//instructor;
        }

        public HttpResponseMessage Post([FromBody]RentJournal value)
        {            
            try
            {
                db.RentJournalSet.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]RentJournal item)
        {
            try
            {                
                var i = db.RentJournalSet.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.ClientId = item.ClientId;
                i.WaterId = item.WaterId;
                i.PayMethodId = item.PayMethodId;
                i.WaterCraftId = item.WaterCraftId;
                i.InstructionTypeId = item.InstructionTypeId;
                i.InstructorId = item.InstructorId;
                i.Date = item.Date;
                i.Cost = item.Cost;
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
                var item = db.RentJournalSet.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);


                db.RentJournalSet.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}