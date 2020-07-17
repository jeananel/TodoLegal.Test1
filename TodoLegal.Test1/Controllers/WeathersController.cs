using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoLegal.Test1.Helper;
using TodoLegal.Test1.Models;

namespace TodoLegal.Test1.Controllers
{
    public class WeathersController : ApiController
    {
        private TodoLegalTestEntities db = new TodoLegalTestEntities();

        // GET: api/Weathers
        public IQueryable<Weather> GetWeather()
        {
            return db.Weather;
        }

        // GET: api/Weathers/5
        [ResponseType(typeof(Weather))]
        public async Task<IHttpActionResult> GetWeather(bool onlyConsult)
        {
            try
            {
                CallAPIGetType call = new CallAPIGetType();
                var response = await call.SetRequestAPI(Tools.UrlDweet, Method.GET, null, null);

                //valid issues with web service dweet.io
                if ((int)response.StatusCode != 200)
                    return Ok(new ResultTest2 { Message = response.StatusCode.ToString(), Aditional = "Failed send request to dweet.io. Try again later." + response.ErrorMessage, });

                var request = JsonConvert.DeserializeObject<Dweet>(response.Content);

                var data = request.with.OrderByDescending(s => s.created).ToList();

                var lastRegister = data.FirstOrDefault();

                ResultTest2 result = new ResultTest2 { Message = response.StatusCode.ToString(), Humedity = lastRegister.content.humidity, Temperature = lastRegister.content.temperature, Last = DateTime.Now, Aditional = "Datime Created Dweet.io " + lastRegister.created.ToString() };

                //save data response tracking
                db.Weather.Add(new Weather { Humidity = lastRegister.content.humidity, Temperature = Convert.ToDecimal(lastRegister.content.temperature), RequestDate = DateTime.Now });
                await db.SaveChangesAsync();

                #region PARAMETERS
                var objectBodyResult = JsonConvert.SerializeObject(result);

                //BODY
                Dictionary<string, string> bodyParameter = new Dictionary<string, string>(){
                    { "application/json", objectBodyResult }
                };
                #endregion

                CallAPIGetType call2 = new CallAPIGetType();
                var response2 = await call.SetRequestAPI(Tools.UrlSendTest, Method.POST, null, bodyParameter);

                if ((int)response2.StatusCode == 200)
                    return Ok(result);
                else
                    return Ok(new ResultTest2 { Message = response2.StatusCode.ToString(), Aditional = "Failed send request to webhook.site. Try again later.", Humedity = lastRegister.content.humidity, Temperature = lastRegister.content.temperature, Last = DateTime.Now });

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