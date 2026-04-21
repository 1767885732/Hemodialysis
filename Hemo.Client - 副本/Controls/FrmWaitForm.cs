/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：等待框
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace Hemo.Client.Controls
{
    public partial class FrmWaitForm : WaitForm
    {
        public FrmWaitForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides
        /// <summary>
        /// 设置进度条内容
        /// </summary>
        /// <param name="caption"></param>
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        /// <summary>
        /// 设置描述
        /// </summary>
        /// <param name="description"></param>
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        /// <summary>
        /// 进度条命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            if (cmd.ToString() == WaitFormCommand.SetText.ToString())
            {
                SetCaption(arg.ToString());
            }
            base.ProcessCommand(cmd, arg);
        }
        /// <summary>
        /// 设置显示位置
        /// </summary>
        /// <param name="control"></param>
        public void ShowLoadingScreenFor(Control control)
        {
            this.Left = (control.Width - this.Width) / 2 + control.Left;
            this.Top = (control.Height - this.Height) / 2 + control.Top;
            this.Height = 40;
            this.Width = 100;
            this.Visible = true;
        }
        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideLoadingScreen()
        {
            this.Visible = false;
        }
        #endregion

        /// <summary>
        /// 命令内容
        /// </summary>
        public enum WaitFormCommand
        {
            SetProgress,
            SetText
        }
    }
}