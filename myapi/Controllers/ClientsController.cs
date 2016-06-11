using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.IO;
using OfficeOpenXml;
using System.Net.Http.Headers;
using myapi.Models;

namespace myapi.Controllers
{
    public class ClientsController : ApiController
    {
        ModelContainer db = new ModelContainer();

        public IEnumerable<ClientDTO> Get()
        {
            return db.Clients
                .Select(i =>
                    new ClientDTO()
                    {
                        Id = i.Id,
                        FIO = i.FIO,
                        Passport = i.Passport
                    });
        }

        public HttpResponseMessage Get(int id)
        {
            var item = db.Clients.Select(i =>
                    new ClientDTO()
                    {
                        Id = i.Id,
                        FIO = i.FIO,
                        Passport = i.Passport
                    }).SingleOrDefault(i => i.Id == id);

            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage Excel()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(GetExcelSheet());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "clients.xlsx";
            return response;
        }

        private MemoryStream GetExcelSheet()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Test");
                worksheet.Cells["A1"].LoadFromCollection<ClientDTO>(Get());
                //package.Save();
                return new MemoryStream(package.GetAsByteArray());
            }
        }

        public HttpResponseMessage Post([FromBody]Client value)
        {
            try
            {
                db.Clients.Add(value);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Client item)
        {
            try
            {
                Client i = db.Clients.Find(id);

                if (i == null)
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Item is not found");

                i.FIO = item.FIO;
                i.Passport = item.Passport;
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
                var item = db.Clients.Find(id);

                if (item == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (db.RentJournalSet.Where(i => i.ClientId == item.Id).Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete. There are referenced items.");

                db.Clients.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}