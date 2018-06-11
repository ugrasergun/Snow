using Newtonsoft.Json;
using SnowLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SnowAPI.Controllers
{
    public class SnowController : ApiController
    {

        [HttpPost]
        public IEnumerable<Bar> UploadFile()
        {
           var httpPostedFile = HttpContext.Current?.Request?.Files?["UploadedFile"];
            List<string> lines = new List<string>();
            if (httpPostedFile != null)
            {
                
                using (var reader = new StreamReader(httpPostedFile.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
             }

            IParser parser = new SnowParser();

            return parser.ParseAll(lines.ToArray());
        }
    }
}
