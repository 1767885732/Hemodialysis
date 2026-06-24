/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2015年1月14日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hemo.Model;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 医疗设备登记表.xaml 的交互逻辑
    /// </summary>
    public partial class 医疗设备登记表 : UserControl
    {
        #region 字段
        /// <summary>
        /// 当前主机
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMERow _currentMainFrame;

        /// <summary>
        /// 当前主机附属设备
        /// </summary>
        private MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable _currentAccessoryEquips;
        #endregion

        #region 属性
        public MachineModel.MED_MACHINE_MAINFRAMERow CurrentMainFrame
        {
            set
            {
                this._currentMainFrame = value;
            }
        }

        public MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable CurrentAccessoryEquips
        {
            set
            {
                this._currentAccessoryEquips = value;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public 医疗设备登记表()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainframeGrid.DataContext = this._currentMainFrame;
            this.accessoryEquipGrid.ItemsSource = this._currentAccessoryEquips;
        }
        #endregion
    }
}
