using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpMvc
{
    public class DownFileResult : ActionResult
    {
        public string FilePath { get; set; }  //文件路径
        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {
            FileStream fs = File.OpenRead(FilePath);
            string fileName = string.Empty;
            int donetIndex = FilePath.LastIndexOf("\\");
            if (donetIndex >= 0)
            {
                fileName = FilePath.Substring(donetIndex + 1);
            }
            response.StatusCode = 200;
            response.ContentLength64 = fs.Length;
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Stream output = response.OutputStream;
            try
            {
                byte[] buffer = new byte[1024];

                int read = 0;
                while ((read = fs.Read(buffer, 0, 1024)) > 0)
                {
                    output.Write(buffer, 0, read);
                }

                output.Close();
            }
            catch
            {
                output.Close();
            }
           
        }
    }
}
