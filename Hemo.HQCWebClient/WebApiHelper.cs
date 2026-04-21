/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:WebApiHelper
 * 创建标识:吕志强-2017年9月18日
 * 
 * 修改时间:2017年9月18日
 * 修改人:吕志强
 * 修改描述:新增方法GetWebApi
 * ----------------------------------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hemo.HQCWebClient.Models;
using Hemo.Utilities;

namespace Hemo.HQCWebClient
{
    /// <summary>
    /// 提供WebApi服务访问类
    /// </summary>
    public class WebApiHelper
    {
        /// <summary>
        /// GET请求WebApi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webApi"></param>
        /// <param name="tokenApi"></param>
        /// <param name="query"></param>
        /// <param name="queryStr"></param>
        /// <param name="staffId"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static ResultMsg<T> GetWebApi<T>(string webApi, string tokenApi, string query, string queryStr, int staffId, bool sign = true) where T : class
        {
            System.GC.Collect();
            Stream streamReceive = null;
            StreamReader streamReader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string strResult = string.Empty;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(webApi + "?" + queryStr);
                string timeStamp = GetTimeStamp();
                string nonce = GetRandom();

                //加入头信息
                request.Headers.Add("staffId", staffId.ToString());//当前请求用户的Staffid
                request.Headers.Add("timeStamp", timeStamp);//发起请求的时间戳（单位：毫秒）
                request.Headers.Add("nonce", nonce);//发起请求的随机数
                if (sign)
                    request.Headers.Add("signature", GetSignature(timeStamp, nonce, staffId, query, tokenApi));//当前请求内容的数字签名

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Timeout = 30000;
                request.KeepAlive = false;
                request.Headers.Set("Pragma", "no-cache");

                response = (HttpWebResponse)request.GetResponse();
                streamReceive = response.GetResponseStream();
                streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                strResult = streamReader.ReadToEnd();
            }
            catch (WebException exp)
            {
                MessageLog.Instance().Log(new LogEntity() { Type = webApi, Content = exp.Message });
            }
            finally
            {
                if (streamReader != null) { streamReader.Close(); }
                if (streamReceive != null) { streamReceive.Close(); }
                if (response != null) { response.Close(); }
                if (request != null) { request.Abort(); }
            }

            return JsonConvert.DeserializeObject<ResultMsg<T>>(strResult);
        }

        /// <summary>
        /// POST请求WebApi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webApi"></param>
        /// <param name="tokenApi"></param>
        /// <param name="query"></param>
        /// <param name="staffId"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static ResultMsg<T> PostWebApi<T>(string webApi, string tokenApi, string query, int staffId, bool sign = true) where T : class
        {
            System.GC.Collect();
            Stream streamReceive = null;
            StreamReader streamReader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string strResult = string.Empty;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(webApi);
                string timeStamp = GetTimeStamp();
                string nonce = GetRandom();

                //加入头信息
                request.Headers.Add("staffId", staffId.ToString());//当前请求用户的Staffid
                request.Headers.Add("timeStamp", timeStamp);//发起请求的时间戳（单位：毫秒）
                request.Headers.Add("nonce", nonce);//发起请求的随机数
                if (sign)
                    request.Headers.Add("signature", GetSignature(timeStamp, nonce, staffId, query, tokenApi));//当前请求内容的数字签名

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 30000;
                request.KeepAlive = false;
                request.Headers.Set("Pragma", "no-cache");
                byte[] data = Encoding.UTF8.GetBytes(query);
                request.ContentLength = data.Length;

                Stream reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                response = (HttpWebResponse)request.GetResponse();

                streamReceive = response.GetResponseStream();
                streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                strResult = streamReader.ReadToEnd();
            }
            catch (WebException exp)
            {
                MessageLog.Instance().Log(new LogEntity() { Type = webApi, Content = exp.Message });
            }
            finally
            {
                if (streamReader != null) { streamReader.Close(); }
                if (streamReceive != null) { streamReceive.Close(); }
                if (response != null) { response.Close(); }
                if (request != null) { request.Abort(); }
            }

            return JsonConvert.DeserializeObject<ResultMsg<T>>(strResult);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        private static string GetRandom()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            return i.ToString();
        }

        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="nonce"></param>
        /// <param name="staffId"></param>
        /// <param name="data"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        private static string GetSignature(string timeStamp, string nonce, int staffId, string data, string tokenApi)
        {
            Token token = null;
            var resultMsg = GetSignToken(staffId, tokenApi);
            if (resultMsg != null)
            {
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    token = resultMsg.Result;
                }
                else
                {
                    throw new Exception(resultMsg.Data.ToString());
                }
            }
            else
            {
                throw new Exception("token为null，编号为：" + staffId);
            }

            var hash = System.Security.Cryptography.MD5.Create();
            //拼接签名数据
            var signStr = timeStamp + nonce + staffId + token.SignToken.ToString() + data;
            //将字符串中字符按升序排序
            var sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            //使用MD5加密
            var md5Val = hash.ComputeHash(bytes);
            //把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper();
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        private static ResultMsg<Token> GetSignToken(int staffId, string tokenApi)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            Tuple<string, string> parameters = GetQueryString(parames);
            ResultMsg<Token> token = WebApiHelper.GetWebApi<Token>(tokenApi, tokenApi, parameters.Item1, parameters.Item2, staffId, false);
            return token;
        }

        /// <summary>
        /// 获取参数拼接字符串
        /// </summary>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetQueryString(Dictionary<string, string> parames)
        {
            //第一步：把字典按key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            //第二部 把所有的名字和参数值串在一起
            StringBuilder query = new StringBuilder("");//签名字符串
            StringBuilder queryStr = new StringBuilder("");//url参数
            if (parames == null || parames.Count == 0)
            {
                return new Tuple<string, string>("", "");
            }

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    query.Append(key).Append(value);
                    queryStr.Append("&").Append(key).Append("=").Append(value);
                }
            }
            return new Tuple<string, string>(query.ToString(), queryStr.ToString().Substring(1, queryStr.Length - 1));
        }
    }
}
