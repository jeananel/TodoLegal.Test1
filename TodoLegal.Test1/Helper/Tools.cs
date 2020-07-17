using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace TodoLegal.Test1.Helper
{
    public static class Tools
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static string UrlSendTest = "https://webhook.site/6f7c6822-4237-4e18-899b-87aaedf728a3";

        public static string UrlDweet = "https://dweet.io/get/dweets/for/thecore";

        public static void RegisterException(Exception ex, string nameMethod)
        {
            Log.Error(ex, string.Format("An error has ocurred {0}", nameMethod));
        }

        public static void RegisterInfo(string message)
        {
            Log.Info(message);
        }

        //Web Scrapping
        public static async Task<string> GetHtml() {

            var url = "https://dweet.io/follow/thecore";

            var httpCliente = new HttpClient();

            var html = await httpCliente.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var temperatura = htmlDocument.GetElementbyId("data-row-temperature");
            var humedad = htmlDocument.GetElementbyId("data-row-humidity");

            return string.Empty; //Checking get parameters html
        }
    }
}