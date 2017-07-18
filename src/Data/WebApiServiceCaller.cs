using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using OfficeLocationMicroservice.Core.Services.SharedContext;

namespace OfficeLocationMicroservice.Data
{
    public sealed class WebApiServiceCaller
    {
        private readonly ISystemLog _systemLog;
        private readonly JavaScriptSerializer _javaScriptSerializer;

        public WebApiServiceCaller(
            ISystemLog systemLog)
        {
            _systemLog = systemLog;

            _javaScriptSerializer = new JavaScriptSerializer();
        }

        public TType GetDataFromJsonUrl<TType>(
            string targetUrl)
        {
            const int fiveMinutes = 300000;

            _systemLog.Info("Calling web service: " + targetUrl);

            var request = WebRequest.Create(targetUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = fiveMinutes;

            request.Method = "GET";

            var jsonData = "";

            using (var s = request.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    jsonData = sr.ReadToEnd();
                }
            }

            _javaScriptSerializer.MaxJsonLength = Int32.MaxValue;

            var models = _javaScriptSerializer.Deserialize<TType>(
                jsonData);

            return models;
        }
    }
}