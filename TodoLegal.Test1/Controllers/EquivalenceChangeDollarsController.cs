using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoLegal.Test1.Helper;
using TodoLegal.Test1.Models;

namespace TodoLegal.Test1.Controllers
{
    public class EquivalenceChangeDollarsController : ApiController
    {
        private TodoLegalTestEntities db = new TodoLegalTestEntities();

        // GET: api/EquivalenceChangeDollars
        public IQueryable<TrackingResults> GetEquivalenceChangeDollar()
        {
            return db.TrackingResults;
        }

        // GET: api/EquivalenceChangeDollars/5
        [ResponseType(typeof(EquivalenceChangeDollar))]
        public async Task<IHttpActionResult> GetEquivalenceChangeDollar(string code, decimal valor)
        {
            try
            {
                EquivalenceChangeDollar equivalenceChangeDollar = await db.EquivalenceChangeDollar.FirstOrDefaultAsync(s => s.Code.Contains(code));

                if (equivalenceChangeDollar == null)
                    return NotFound();

                var operation = Math.Round(valor * equivalenceChangeDollar.ValueExchangeRate, 4);

                ResultTest1 result = new ResultTest1 { Result = operation, Message = "OK", Aditional = equivalenceChangeDollar.Description };

                #region PARAMETERS
                var objectBodyResult = JsonConvert.SerializeObject(result);

                //BODY
                Dictionary<string, string> bodyParameter = new Dictionary<string, string>(){
                    { "application/json", objectBodyResult }
                };
                #endregion

                CallAPIGetType call = new CallAPIGetType();
                var response = await call.SetRequestAPI(Tools.UrlSendTest, Method.POST, null, bodyParameter);

                var trackingResponse = new TrackingResults { Result = operation, Code = response.StatusCode.ToString() };
                db.TrackingResults.Add(trackingResponse);
                await db.SaveChangesAsync();

                if ((int)response.StatusCode == 200)
                    return Ok(result);
                else
                    return Ok(new ResultTest1 { Result = operation, Message = response.StatusCode.ToString(), Aditional = "Failed send request to webhook.site. Try again later." });

            }
            catch (Exception ex)
            {
                Tools.RegisterException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}