using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Uitl
{
    public class HttpRequest
    {
        #region 声明及构造函数
        /// <summary>
        /// 私有实例
        /// </summary>
        private static HttpRequest _instance = new HttpRequest();

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HttpRequest Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private HttpRequest()
        {
        }
        #endregion

        /// <summary>
        /// Cookie容器
        /// </summary>
        private static CookieContainer _cookies = new CookieContainer();

        /// <summary>
        /// Post数据到指定的URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="isSetNewSessionID">是否生成新的SessionID</param>
        /// <returns>数据Post的返回结果</returns>
        public string PostRequest(string url, Dictionary<string, string> parameters, bool isSetNewSessionID)
        {
            string responseData = string.Empty;

            //Init http发送
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.UserAgent = string.Empty;
            request.Timeout = 15000;
            request.ServicePoint.Expect100Continue = false;
            if (isSetNewSessionID)
            {
                request.CookieContainer = new CookieContainer();
            }
            else
            {
                request.CookieContainer = _cookies;
            }
            Encoding encoding = Encoding.GetEncoding("utf-8");

            StreamReader responseReader = null;
            Stream responseStream = null;

            //POST数据
            string buffer = string.Empty;
            if (parameters != null)
            {
                buffer = parameters.Aggregate(buffer, (current, keyValuePair) => current + ( HttpUtility.UrlEncode(keyValuePair.Key, encoding) + "=" + HttpUtility.UrlEncode(keyValuePair.Value, encoding) + "&"));
            }

            buffer = buffer.TrimEnd('&');

            byte[] data = encoding.GetBytes(buffer);

            request.ContentLength = data.Length;

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                responseStream = request.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
             


                //throw new Exception("URL :" + url + "，请检查是否可以正常访问!" + ex.ToString());
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (responseReader != null)
                {
                    responseReader.Close();
                }
                if (isSetNewSessionID)
                {
                    _cookies = request.CookieContainer;
                }
            }

            return responseData;
        }



        /// <summary>
        /// 以GET方式请求URL，并获取结果
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="isSetNewSessionID">是否生成新的SessionID</param>
        /// <returns>返回结果</returns>
        public string GetRequest(string url, Dictionary<string, string> parameters, bool isSetNewSessionID)
        {
            bool succesFlag = true;
            return GetRequest(url, parameters, isSetNewSessionID, ref succesFlag);
        }


        /// <summary>
        /// 以GET方式请求URL，并获取结果
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="isSetNewSessionID">是否生成新的SessionID</param>
        /// <param name="succesFlag">是否请求成功</param>
        /// <returns>返回结果</returns>
        public string GetRequest(string url, Dictionary<string, string> parameters, bool isSetNewSessionID, ref bool succesFlag)
        {
            succesFlag = true;
            string responseData = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                //GET参数
                string getParams = string.Empty;
                if (parameters != null)
                {
                    getParams = parameters.Aggregate(getParams, (current, keyValuePair) => current + (keyValuePair.Key + "=" + keyValuePair.Value + "&"));
                }
                getParams = getParams.TrimEnd('&');

                //Init http发送
                HttpWebRequest request = null;
                request = WebRequest.Create(url + "?" + getParams) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = string.Empty;
                request.Timeout = 15000;
                request.ServicePoint.Expect100Continue = false;
                if (isSetNewSessionID)
                {
                    request.CookieContainer = new CookieContainer();
                }
                else
                {
                    request.CookieContainer = _cookies;
                }

                StreamReader responseReader = null;
                Stream responseStream = null;

                try
                {
                    WebResponse response = request.GetResponse();
                    responseStream = response.GetResponseStream();
                    responseReader = new StreamReader(responseStream);
                    responseData = responseReader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    succesFlag = false;
                 
                }
                finally
                {
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    if (responseReader != null)
                    {
                        responseReader.Close();
                    }
                    if (isSetNewSessionID)
                    {
                        _cookies = request.CookieContainer;
                    }
                }
            }
            else
            {
                succesFlag = false;

            }
            return responseData;
        }


        /// <summary>
        /// json复杂对象,只是区分方法,测试复杂对象,没有特意按泛型来写
        /// </summary>
        /// <returns></returns>
        public string PostRequestComplex(string url, string json)
        {
            string result = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //request.ContentType = "application/json;charset=utf-8";
            request.ContentType = "application/json";
            request.Timeout = 15000;
            request.ServicePoint.Expect100Continue = false; 
            byte[] payload = System.Text.Encoding.GetEncoding("utf-8").GetBytes(json);
            request.ContentLength = payload.Length;
            System.IO.Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception)
            {
                writer = null;

            }
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            HttpWebResponse response = null;
            try
            {

                response = (HttpWebResponse)request.GetResponse();
                using (Stream s = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(s, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
              

            }
            return result;
        }



        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求的url/param>
        /// <param name="saveFullName">保存的路径</param>
        /// <returns></returns>
        public bool DownloadFile(string url, string saveFullName)
        {
            bool flag = false;
            HttpWebRequest request = null;
            try
            {
                request = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 10000;
                HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)request.GetResponse();
                Stream sr = httpWebResponse.GetResponseStream();
                Stream sw = new System.IO.FileStream(saveFullName, System.IO.FileMode.Create);

                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = sr.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    sw.Write(by, 0, osize);
                    osize = sr.Read(by, 0, (int)by.Length);
                }
                System.Threading.Thread.Sleep(100);
                flag = true;
                sw.Close();
                sr.Close();
            }
            catch (Exception ex)
            {
                if (request != null)
                {
                    request.Abort();
                }
              
            }
            finally
            {

            }
            return flag;
        }



        public bool DownImage(string url, string path)
        {
            bool flag = true;
            try
            {
                WebClient client = new WebClient();
                using (Stream str = client.OpenRead(url))
                {
                    StreamReader reader = new StreamReader(str);
                    byte[] mbyte = new byte[1000000];
                    int allmybyte = (int)mbyte.Length;
                    int startmbyte = 0;

                    while (allmybyte > 0)
                    {

                        int m = str.Read(mbyte, startmbyte, allmybyte);
                        if (m == 0)
                            break;

                        startmbyte += m;
                        allmybyte -= m;
                    }

                    reader.Dispose();
                    str.Dispose();
                    FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    fstr.Write(mbyte, 0, startmbyte);
                    fstr.Flush();
                    fstr.Close();
                }
            }
            catch (Exception ex)
            {
                flag = false;
             
            }

            return flag;
        }
    }
}
