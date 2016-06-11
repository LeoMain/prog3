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
    public class WatercraftsController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<WatercraftDTO> Get()
        {
            return db.WaterCrafts
                .Select(i =>
                    new WatercraftDTO()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        WaterCraftType = db.WatercraftTypes.FirstOrDefault(p => p.Id == i.WatercraftTypeId).Type,
                        WaterCraftCondition = db.WatercraftConditions.FirstOrDefault(p => p.Id == i.WatercraftConditionId).Condition,
                        CostRate = i.CostRate
                    })
                .ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            var item = db.WaterCrafts.Select(i =>
                    new WatercraftDTO()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        WaterCraftType = db.WatercraftTypes.FirstOrDefault(p => p.Id == i.WatercraftTypeId).Type,
                        WaterCraftCondition = db.WatercraftConditions.FirstOrDefault(p => p.Id == i.WatercraftConditionId).Condition,
                        CostRate = i.CostRate
                    }).SingleOrDefault(i => i.Id == id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);//instructor;
        }

        public HttpResponseMessage Post([FromBody]WaterCraft value)
        {            
            try
            {
                db.WaterCrafts.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]WaterCraft item)
        {
            try
            {
                var i = db.WaterCrafts.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.Name = item.Name;
                i.WatercraftTypeId = item.WatercraftTypeId;
                i.WatercraftConditionId = item.WatercraftConditionId;
                i.CostRate = item.CostRate;
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
                var item = db.WaterCrafts.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (db.RentJournalSet.Where(i => i.WaterCraftId == item.Id).Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.WaterCrafts.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}