/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:定义释放非托管资源类
 * 创建标识:刘超-2017年1月17日
 * ----------------------------------------------------------------*/

using System;
using System.Collections;
namespace Hemo.Client.Print.PrintClass
{
    [Serializable]
	public class MCObject : IDisposable
    {
        #region 类变量

        private string strID;
        private string strName;
        private string spellCode;
        private string wubiCode;
        private string userCode;
        private string strMemo;

        public string User01 = string.Empty;
        public string User02 = string.Empty;
        public string User03 = string.Empty;

        private bool alreadyDisposed = false;

        #endregion

        #region 属性

        /// <summary>
        /// ID
        /// </summary>
        public virtual string ID
        {
            get
            {
                return strID + "";
            }
            set
            {
                strID = value;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return strName + "";
            }
            set
            {
                strName = value;
            }
        }

        /// <summary>
        /// SpellCode
        /// </summary>
        public string SpellCode
        {
            get { return spellCode; }
            set { spellCode = value; }
        }

        /// <summary>
        /// WBCode
        /// </summary>
        public string WBCode
        {
            get { return wubiCode; }
            set { wubiCode = value; }
        }

        /// <summary>
        /// UserCode
        /// </summary>
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        /// <summary>
        /// Memo
        /// </summary>
        public virtual string Memo
        {
            get
            {
                return strMemo + "";
            }
            set
            {
                strMemo = value;
            }
        }

        #endregion

        #region 构造函数
        
        public MCObject()
		{
			strID="";
			strName="";
            spellCode = "";
            wubiCode = "";
            userCode = "";
			strMemo="";
		}

        public MCObject(string id, string name,string spell_Code,string wubi_Code,string user_Code, string memo)
        {
            strID = id;
            strName = name;
            spellCode = spell_Code;
            wubiCode = wubi_Code;
            userCode = user_Code;
            strMemo = memo;
        }

        #endregion

        #region 方法

        /// <summary>
		/// 重写ToString
		/// </summary>
		/// <returns>Name</returns>
		public override string ToString()
		{
			return this.Name;
        }

        /// <summary>
        /// 创建对象的浅表副本
        /// </summary>
        /// <returns></returns>
        public MCObject Clone()
        {
            return this.MemberwiseClone() as MCObject;
        }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (alreadyDisposed)
                return;
            if (isDisposing)
            {
                // TODO: free managed resources here
            }
            //TODO: free unmanaged resources here
            alreadyDisposed = true;
        }

        #endregion

		#region IDisposable 成员

        /// <summary>
        /// 释放非托管资源
        /// </summary>
		public void Dispose()
		{
			Dispose( true );			
			GC.SuppressFinalize(true);
		}

		#endregion
	}
}
