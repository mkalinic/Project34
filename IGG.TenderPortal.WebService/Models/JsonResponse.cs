using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IGG.TenderPortal.WebService.Models
{
    /// <summary>
    /// When I use this kind of architecture, I like to standardise json protocol between server and client
    /// because I can add additional fields if needed (for example the time of request to measure speed... or anything)
    /// and can send more errors than http protocol provides
    /// plus I can fastly implement basic autentification or security
    /// I call it "ajax simple protocol"
    /// </summary>
    public class JsonResponse
    {
        /// <summary>
        /// if response contains error
        /// </summary>
        public static readonly string ERROR_RESPONSE = "ERROR";

        /// <summary>
        /// if response is normal, and there is no data present
        /// </summary>
        public static readonly string OK_NO_DATA_RESPONSE = "OK_NO_DATA";

        /// <summary>
        /// if response is normal, and there is data present
        /// </summary>
        public static readonly string OK_DATA_RESPONSE = "OK";


        /// <summary>
        /// Status of request
        /// </summary>
        public string Status;

        /// <summary>
        /// Status of request
        /// </summary>
        public object Data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">One of values: JsonResponse.ERROR_RESPONSE , JsonResponse.OK_NO_DATA_RESPONSE, JsonResponse.OK_DATA_RESPONSE</param>
        /// <param name="data"></param>
        /// /// <param name="data">jsonRequestBehavior One of values from JsonRequestBehavior (like  JsonRequestBehavior.AllowGet)</param>
        public JsonResponse(string status, object data)
        {
            this.Status = status;
            this.Data = data;

        }


        public static JsonResult GetJsonResult(string status, object data, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            JsonResponse jsonResponse = new JsonResponse(status, data);
            return new JsonResult() { Data = jsonResponse, JsonRequestBehavior = jsonRequestBehavior };
        }


    }
}