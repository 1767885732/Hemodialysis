/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：主页WPF左右选择
// 创建时间：2015-08-21
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
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
using System.Windows.Media.Animation;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// HomePageToolBar.xaml 的交互逻辑
    /// </summary>
    public partial class HomePageToolBar : UserControl
    {
        public HomePageToolBar()
        {
            InitializeComponent();
        }

        private void WNewBtn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private DoubleAnimation _doubleleftAnimation = null;

        private void WNewBtn_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (_doubleleftAnimation == null)
            {
                _doubleleftAnimation = new DoubleAnimation();
                _doubleleftAnimation.From = 0;
                _doubleleftAnimation.To = 1;
                _doubleleftAnimation.Duration = TimeSpan.FromMilliseconds(500);
                _doubleleftAnimation.FillBehavior = FillBehavior.Stop;
                _doubleleftAnimation.Completed += delegate
                {
                    this.WNewBtn.Opacity = 1;
                };
            }

            this.WNewBtn.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleleftAnimation, HandoffBehavior.SnapshotAndReplace);
        }
        private DoubleAnimation _doubleLeftAnimation2 = null;

        private void WNewBtn_OnMouseLeave(object sender, MouseEventArgs e)
        {

            if (_doubleLeftAnimation2 == null)
            {
                _doubleLeftAnimation2 = new DoubleAnimation();
                _doubleLeftAnimation2.From = 1;
                _doubleLeftAnimation2.To = 0;
                _doubleLeftAnimation2.Duration = TimeSpan.FromMilliseconds(500);
                _doubleLeftAnimation2.FillBehavior = FillBehavior.Stop;
                _doubleLeftAnimation2.Completed += delegate
                {
                    this.WNewBtn.Opacity = 0;
                };
            }

            this.WNewBtn.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleLeftAnimation2, HandoffBehavior.SnapshotAndReplace);
        }

        private void WEditBtn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private DoubleAnimation _doubleRightAnimation = null;

        private void WEditBtn_OnMouseEnter(object sender, MouseEventArgs e)
        {

            if (_doubleRightAnimation == null)
            {
                _doubleRightAnimation = new DoubleAnimation();
                _doubleRightAnimation.From = 0;
                _doubleRightAnimation.To = 1;
                _doubleRightAnimation.Duration = TimeSpan.FromMilliseconds(500);
                _doubleRightAnimation.FillBehavior = FillBehavior.Stop;
                _doubleRightAnimation.Completed += delegate
                {
                    this.WEditBtn.Opacity = 1;
                };
            }

            this.WEditBtn.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleRightAnimation, HandoffBehavior.SnapshotAndReplace);
        }
        private DoubleAnimation _doubleRightAnimation2 = null;

        private void WEditBtn_OnMouseLeave(object sender, MouseEventArgs e)
        {

            if (_doubleRightAnimation2 == null)
            {
                _doubleRightAnimation2 = new DoubleAnimation();
                _doubleRightAnimation2.From = 1;
                _doubleRightAnimation2.To = 0;
                _doubleRightAnimation2.Duration = TimeSpan.FromMilliseconds(500);
                _doubleRightAnimation2.FillBehavior = FillBehavior.Stop;
                _doubleRightAnimation2.Completed += delegate
                {
                    this.WEditBtn.Opacity = 0;
                };
            }

            this.WEditBtn.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleRightAnimation2, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
