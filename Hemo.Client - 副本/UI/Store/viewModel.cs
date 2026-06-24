/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:视图实体类
 * 创建标识:贺建操-2017年4月5日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit.Model;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Store
{
    public partial class viewModel : ViewBase
    {
        #region 构造函数

        public viewModel()
        {
            InitializeComponent();
        }

        #endregion

        public void IniControl(string contolTypeName)
        {

        }

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            //Save();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            //New();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            //Edit();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            SetEditMode(true);

            //Delete();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(true);

            //Cancle();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #region 重写事件


        //public override void New()
        //{
        //    base.New();
        //}
        //public override void Edit()
        //{
        //    base.Edit();
        //}
        //public override void Cancle()
        //{
        //    base.Cancle();
        //}
        //public override void Delete()
        //{
        //    base.Delete();
        //}

        //public override void Save()
        //{
        //    base.Save();
        //}

        #endregion

        #endregion

        #region 方法

        /// <summary>        
        ///设置为编辑模式
        ///数据操作两种状态.1：数据修改状态 2：查看数据状态 
        /// </summary>
        protected virtual void SetEditMode(bool isVisit)
        {
            this.btnAdd.Visible = isVisit;
            this.btnEdit.Visible = isVisit;
            this.btnDelete.Visible = isVisit;
            this.btnSave.Visible = !isVisit;
            this.btnCancel.Visible = !Visible;
        }

        #endregion
    }

    /// <summary>
    /// ControlTypeName枚举类
    /// </summary>
    public enum ControlTypeName
    {
        supplier = 0,
        material = 1
    }
}
