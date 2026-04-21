/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.HQCWebClient.Models
 * 创建标识:吕志强-2014年8月24日
 * ----------------------------------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// 客户端信息实体类
    /// </summary>
    public class ResultMsg<T> where T : class
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        [JsonIgnore]
        public T Result
        {
            get
            {
                if (StatusCode == (int)StatusCodeEnum.Success)
                {
                    return JsonConvert.DeserializeObject<T>(Data.ToString());
                }
                return null;
            }
        }
    }

    /// <summary>
    /// 状态码枚举类
    /// </summary>
    public enum StatusCodeEnum
    {
        [Description("成功")]
        Success = 200,

        [Description("参数错误")]
        ParameterError = 201,

        [Description("请求超时")]
        UrlExpireTime = 202,

        [Description("认证无效")]
        TokenInvalid = 203,

        [Description("请求方法错误")]
        HttpMethodError = 204,

        [Description("请求错误")]
        HttpRequestError = 205,

        [Description("返回为空")]
        ReturnIsNull = 206,

        [Description("上传失败")]
        UploadFail = 207,

        [Description("记录已存在")]
        RecordExist = 208
    }

    /// <summary>
    /// 状态码枚举扩展类
    /// </summary>
    public static class StatusCodeEnumExtension
    {
        public static string GetEnumText(this StatusCodeEnum statusCode)
        {
            string text = string.Empty;
            switch (statusCode)
            {
                case StatusCodeEnum.Success:
                    text = "成功";
                    break;
                case StatusCodeEnum.ParameterError:
                    text = "参数错误";
                    break;
                case StatusCodeEnum.UrlExpireTime:
                    text = "请求超时";
                    break;
                case StatusCodeEnum.TokenInvalid:
                    text = "认证无效";
                    break;
                case StatusCodeEnum.HttpMethodError:
                    text = "请求方法错误";
                    break;
                case StatusCodeEnum.HttpRequestError:
                    text = "请求错误";
                    break;
                case StatusCodeEnum.ReturnIsNull:
                    text = "返回为空";
                    break;
                case StatusCodeEnum.UploadFail:
                    text = "上传失败";
                    break;
                case StatusCodeEnum.RecordExist:
                    text = "记录已存在";
                    break;
            }
            return text;
        }
    }
}
