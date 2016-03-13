using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CommonTool
{
    public static class HttpTool
    {
        /// <summary>
        /// GET提交
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string SendWebRequest(string url)
        {
            return SendWebRequest(url, new CookieContainer());
        }
        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SendWebRequest(string url, string data)
        {
            return SendWebRequest(url, data, new CookieContainer());
        }

        public static HttpWebResponse GetHttpWebResponse(string url)
        {
            return GetHttpWebResponse(url, new CookieContainer());
        }
        public static HttpWebResponse GetHttpWebResponse(string url, string data)
        {
            return GetHttpWebResponse(url, data, new CookieContainer());
        }
        public static Stream GetStream(string URL, CookieContainer cookieContainer)
        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "GET";

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();



            getStream = webResponse.GetResponseStream();



            return getStream;

        }
        public static HttpWebResponse GetHttpWebResponse(string URL, CookieContainer cookieContainer)
        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "text/html;charset=UTF-8";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "GET";

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();


            httpWebRequest.Abort();

            webResponse.Close();

            return webResponse;

        }
        public static HttpWebResponse GetHttpWebResponse(string URL, string postData, CookieContainer cookieContainer)
        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;


            //如果是发送HTTPS请求  
            if (URL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = WebRequest.Create(URL) as HttpWebRequest;
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
            }
            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";



            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "Post";

            byte[] byteRequest = Encoding.Default.GetBytes(postData);

            httpWebRequest.ContentLength = byteRequest.Length;

            Stream stream;

            stream = httpWebRequest.GetRequestStream();

            stream.Write(byteRequest, 0, byteRequest.Length);

            stream.Close();

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();



            httpWebRequest.Abort();

            webResponse.Close();




            return webResponse;

        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        public static string SendWebRequest(string URL, string postData, CookieContainer cookieContainer)
        {
            

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            StreamReader streamReader;

            string getString = "";
            //如果是发送HTTPS请求  
            if (URL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = WebRequest.Create(URL) as HttpWebRequest;
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
            }
            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";



            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "Post";

            byte[] byteRequest = Encoding.Default.GetBytes(postData);

            httpWebRequest.ContentLength = byteRequest.Length;

            Stream stream;

            stream = httpWebRequest.GetRequestStream();

            stream.Write(byteRequest, 0, byteRequest.Length);

            stream.Close();

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();

            getStream = webResponse.GetResponseStream();

            streamReader = new StreamReader(getStream, Encoding.UTF8);

            getString = streamReader.ReadToEnd();

            streamReader.Close();

            getStream.Close();

            httpWebRequest.Abort();

            webResponse.Close();

      
            return getString;
           
          
        }


        public static string SendWebRequest(string URL, CookieContainer cookieContainer)
        {
         
                HttpWebRequest httpWebRequest;

                HttpWebResponse webResponse;

                Stream getStream;

                StreamReader streamReader;

                string getString = "";

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

                httpWebRequest.CookieContainer = cookieContainer;

                httpWebRequest.Accept = "*/*";

                httpWebRequest.ContentType = "text/html;charset=UTF-8";

                httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

                httpWebRequest.Method = "GET";

                webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string header = webResponse.Headers.ToString();



                getStream = webResponse.GetResponseStream();
             

                streamReader = new StreamReader(getStream, Encoding.UTF8);

                getString = streamReader.ReadToEnd();



                streamReader.Close();

                getStream.Close();

                httpWebRequest.Abort();

                webResponse.Close();



                return getString;
           


        }

    }
}
