using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTest.WebHelp
{
    class WebHelper    
    {
        public static Random ran = new Random();
        public static Encoding encodingUTF8 = Encoding.UTF8;
        public static MD5 md5 = new MD5CryptoServiceProvider();
        public const String partnerCode ="1001";
        public const String partnerKey="8CFB2D86-50A9-4405-BE07-692DBA484287";
        public const String myjReceiveUrl = "http://api/myj.com.cn/WMS/DR/Receive";
        public const String myjSearchUrl = "http://api/myj.com.cn/WMS/DR/Search";
        public const String myjExcuteUrl ="http://api/myj.com.cn/WMS/DP/Execute";
        public const String testUrl = "http://192.168.100.146:8080/match/test";

        //美宜佳 数据接收 接口（详细情况请看文档）
        public static Boolean postDataToMYJ(String OrgCode, String BatchNo, String DataType,String RecCnt,String jsonData)
        {

            dynamic result = WebHelper.Post(myjReceiveUrl, string.Format("OrgCode={0}&BatchNo={1}&DataType={2}&RecCnt={3}&Extention=&Data={4}",
                           OrgCode, BatchNo, DataType, RecCnt, jsonData));

            return result.code == 1 ? true : false;
        }

        //美宜佳 数据查询 接口（详细情况请看文档）
        public static Boolean checkDataToMYJ(String OrgCode, String BatchNo, String DataType, String RecCnt, String jsonData)
        {

            dynamic result = WebHelper.Post(myjSearchUrl, string.Format("OrgCode={0}&BatchNo={1}&DataType={2}&RecCnt={3}&Extention=&Data={4}",
                           OrgCode, BatchNo, DataType, RecCnt, jsonData));

            return result.code == 1 ? true : false;
        }

        //美宜佳 业务处理 接口（详细情况请看文档）
        public static Boolean excuteDataToMYJ(String OrgCode, String BatchNo, String DataType, String RecCnt, String jsonData)
        {

            dynamic result = WebHelper.Post(myjExcuteUrl, string.Format("OrgCode={0}&BatchNo={1}&DataType={2}&RecCnt={3}&Extention=&Data={4}",
                           OrgCode, BatchNo, DataType, RecCnt, jsonData));

            return result.code == 1 ? true : false;
        }
       
        public static string Get(string Url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }

            return retString;
        }

        public static JObject Post(string Url, string Data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            //request.Referer = Referer;
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json;charset=UTF-8";
            //request.ContentType = "application/x-www-form-urlencoded";

            //header填写校验数据
            String partnerCode = WebHelper.partnerCode;
            String timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            String nonce = getRandom();
            String signature = getMD5(CatString(partnerCode, timestamp, nonce));
            request.Headers.Add("partnerCode", partnerCode);
            request.Headers.Add("timestamp", timestamp);
            request.Headers.Add("nonce", nonce);
            request.Headers.Add("signature", signature);

            //body填写    
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myRequestStream.Close();
            if (response != null)
                response.Close();
            if (request != null)
                request.Abort();         
           JObject result = JObject.Parse(retString);
           return result;
        }
       
        static void Main1(string[] args)
        {



            // string getjson = WebHelper.Post(myjSearchUrl, string.Format("ss={0}&offset={1}&limit={2}&type={3}", key, "0", "10", "1"));

            //JObject jo = JObject.Parse(getjson);
            //JArray  ja = JArray.Parse(getjson);
            //   string result = jo["data"].ToString();   //选择要插入的键


        }

        public static String getMD5(string input)
        {           
            //String input1="10012019-01-31 14:39:17047373808CFB2D86-50A9-4405-BE07-692DBA484287";
            byte[] t = md5.ComputeHash(encodingUTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0').ToUpper());
            return sb.ToString();          
        }

        public static String getRandom()
        {
            int RandKey = ran.Next(0, 99999999);
            return Convert.ToString(RandKey).PadLeft(8, '0');     
        }

        public static String CatString(String partnerCode, String timeStamp, String nonce)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(partnerCode).Append(timeStamp).Append(nonce).Append(partnerKey);
            return sb.ToString();
          
        }

 
    }
}
