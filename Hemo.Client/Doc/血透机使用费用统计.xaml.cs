/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 血透机使用费用统计
 * 创建标识:贺建操-2015年1月18日
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
using System.Data;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 血透机使用费用统计.xaml 的交互逻辑
    /// </summary>
    public partial class 血透机使用费用统计 : UserControl
    {
        #region 成员变量

        /// <summary>
        /// 设备使用费用表（字段合并）
        /// </summary>
        private DataTable _useFeeTable = null;

        /// <summary>
        /// 设备使用费用统计表
        /// </summary>
        private DataTable _useFeeStatisticsTable = null;

        #endregion

        #region 属性

        public DataTable UseFeeTable
        {
            set
            {
                this._useFeeTable = value;
            }
        }

        public DataTable UseFeeStatisticsTable
        {
            set
            {
                this._useFeeStatisticsTable = value;
            }
        }

        public string SickArea { get; set; }

        public string Bednum { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public 血透机使用费用统计()
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
            this.topGrid.DataContext = this._useFeeStatisticsTable;
            this.accessoryEquipGrid.ItemsSource = this._useFeeTable.DefaultView;
            this.txt_SickArea.Text = SickArea;
            this.txt_BedNum.Text = Bednum;

            if (this._useFeeTable != null && this._useFeeTable.Rows.Count > 0)
            {
                this.txt_Year.Text = this._useFeeTable.Rows[0]["dateyear"].ToString();
                this.txt_Month.Text = this._useFeeTable.Rows[0]["datemonth"].ToString();
            }
        }

        #endregion
    }
}
