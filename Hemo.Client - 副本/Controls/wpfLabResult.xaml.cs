/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统加载时数据缓存问题
 * 创建标识:顾伟伟-2016年12月25日
 * 
 * 修改时间:2017年5月12日
 * 修改人:刘超
 * 修改描述:修复系统加载时数据缓存问题
 * 
 * 修改时间:2017年6月13日
 * 修改人:贺建操
 * 修改描述:用户控件
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
using System.Linq;
using Hemo.Model;
using Hemo.Utilities;
namespace Hemo.Client.Controls
{
    /// <summary>
    /// wpfLabResult.xaml 的交互逻辑
    /// </summary>
    public partial class wpfLabResult : WPF_DocumentBase
    {
        #region 变量

        public wpfLabResult1 NextPage
        {
            get
            {
                return new wpfLabResult1();
            }
        }

        #endregion
        #region 构造函数
        
        /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="ds"></param>
       /// <param name="ds1"></param>
       /// <param name="patientRow"></param>
        public wpfLabResult(DataSet ds, DataSet ds1, PatientModel.MED_PATIENTSRow patientRow)
        {
           
            InitializeComponent();

            wpfLabResult1 tb1 = new wpfLabResult1();
            tb1.Name = "myTextBox1";
            tb1.Width = 780;
            tb1.Height = 420;

            tb1.HorizontalAlignment = HorizontalAlignment.Left;
            tb1.VerticalAlignment = VerticalAlignment.Top;
            tb1.Margin = new Thickness(0, 0, 0, 0);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    tb1.grdList.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1] != null)
                {
                    tb1.grdList1.ItemsSource = ds.Tables[1].DefaultView;
                }
            }
            tb1.label4.Content = patientRow.WHAT_DEPARTMENT_IN;
            tb1.label6.Content = patientRow.PATIENT_ID;
            tb1.label8.Content = patientRow.ADMISSION_NUMBER;
            tb1.label18.Content = patientRow.DIAGNOSE;
            tb1.label10.Content = patientRow.NAME;
            tb1.label21.Content = patientRow.SEX;
            tb1.label22.Content = patientRow.AGE;

            grid1.Children.Add(tb1);

            wpfLabResult1 tb2 = new wpfLabResult1();
            tb2.Name = "myTextBox2";
            tb2.Width = 780;
            tb2.Height = 420;

            tb2.HorizontalAlignment = HorizontalAlignment.Left;
            tb2.VerticalAlignment = VerticalAlignment.Top;
            tb2.Margin = new Thickness(0, 420, 0, 0);
            if (ds1!=null && ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0] != null)
                {
                    tb2.grdList.ItemsSource = ds1.Tables[0].DefaultView;
                }
            }
            if (ds1 != null && ds1.Tables.Count > 1)
            {
                if (ds1.Tables[1] != null)
                {
                    tb2.grdList1.ItemsSource = ds1.Tables[1].DefaultView;
                }
            }
            tb2.label4.Content = patientRow.WHAT_DEPARTMENT_IN;
            tb2.label6.Content = patientRow.PATIENT_ID;
            tb2.label8.Content = patientRow.ADMISSION_NUMBER;
            tb2.label18.Content = patientRow.DIAGNOSE;
            tb2.label10.Content = patientRow.NAME;
            tb2.label21.Content = patientRow.SEX;
            tb2.label22.Content = patientRow.AGE;
            grid1.Children.Add(tb2);   
        }

        #endregion
        #region 方法
        
        /// <summary>
        /// 是否显示治疗单grid信息 
        /// </summary>
        /// <param name="pValue">是否显示</param>
        public void IsShowGrid(bool pValue)
        {
             
        }

        #endregion
        #region 事件
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            
        }

        #endregion
       
    }
}
