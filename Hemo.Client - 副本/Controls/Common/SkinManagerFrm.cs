/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:贺建操
 * 创建标识:吕志强-2014年8月10日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using System.Collections;
using System.Xml;
using Hemo.Model;
using Hemo.IService.Permission;
using Hemo.Service;
using Hemo.Client.Core;

namespace Hemo.Client.Controls.Common
{
    /// <summary>
    /// 皮肤管理
    /// </summary>
    public partial class SkinManagerFrm : HemoBaseFrm
    {
        public string SkinName { get; set; }
        public SkinManagerFrm()
        {
            InitializeComponent();
        }

        private DictModel.MED_USERS_SKINDataTable _userSkin = null;

        private IUser _userService = ServiceManager.Instance.UserService;
        /// <summary>
        /// Dev皮肤列表 
        /// </summary>
        private ArrayList SkinList = new ArrayList(){ "DevExpress Dark Style","Office 2010 Black","Visual Studio 2013 Dark","Pumpkin","Dark Side","High Contrast",
        "Sharp","Sharp Plus","Black","Office 2007","Darkroom","Blueprint","Metropolis Dark"};
        /// <summary>
        /// 登录时加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinManager_Load(object sender, EventArgs e)
        {

            bool isAdd = false;
            foreach (SkinContainer item in SkinManager.Default.Skins)
            {
                isAdd = true;
                if (item.SkinName.Contains("2007"))
                    isAdd = false;
                else
                {
                    foreach (string name in SkinList)
                    {
                        if (name == item.SkinName)
                            isAdd = false;
                    }
                }
                if (isAdd)
                {
                    this.listBoxControl1.Items.Add(item.SkinName);
                }
            }

            _userSkin = _userService.GetUserSkinDt();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            SkinName = this.listBoxControl1.SelectedItem.ToString();
            if (_userSkin != null)
            {
                var skinRow = _userSkin.FindByUSER_ID(HemoApplicationContext.Current.CurrentUser.USER_ID);
                if (skinRow != null)
                {
                    skinRow.SKINSTRING = SkinName;
                }
                else
                {
                    var row = _userSkin.NewMED_USERS_SKINRow();
                    row.USER_ID = HemoApplicationContext.Current.CurrentUser.USER_ID;
                    row.SKINSTRING = SkinName;
                    _userSkin.AddMED_USERS_SKINRow(row);
                }
            }
            else
            {
                _userSkin = new DictModel.MED_USERS_SKINDataTable();
                var row = _userSkin.NewMED_USERS_SKINRow();
                row.USER_ID = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.SKINSTRING = SkinName;
                _userSkin.AddMED_USERS_SKINRow(row);

            }
            _userService.SaveUserSkin(_userSkin);

            #region //存本地的XML文件去掉

            //XmlDocument doc = new XmlDocument();
            //doc.Load("SkinInfo.xml");
            //XmlNodeList nodelist = doc.SelectSingleNode("SetSkin").ChildNodes;
            //foreach (XmlNode node in nodelist)
            //{
            //    XmlElement xe = (XmlElement)node;//将子节点类型转换为XmlElement类型
            //    if (xe.Name == "Skinstring")
            //    {
            //        xe.InnerText = SkinName;
            //    }
            //}
            //doc.Save("SkinInfo.xml");

            #endregion


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        /// <summary>
        /// 双击时改变皮肤样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = this.listBoxControl1.SelectedItem.ToString();
        }
    }

}
