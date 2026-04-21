/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:统计控件按钮
 * 创建标识:吕志强-2014年8月2日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemo.Client.Controls.Common
{
    public class DXSimpleButton : DevExpress.XtraEditors.SimpleButton
    {
        private DevExpress.Utils.ImageCollection btnImageList;
        private System.ComponentModel.IContainer components;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DXSimpleButton));
            this.btnImageList = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImageList
            // 
            this.btnImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("btnImageList.ImageStream")));
            this.btnImageList.Images.SetKeyName(0, "1.png");
            this.btnImageList.Images.SetKeyName(1, "2.png");
            this.btnImageList.Images.SetKeyName(2, "3.png");
            this.btnImageList.Images.SetKeyName(3, "4.png");
            this.btnImageList.Images.SetKeyName(4, "5.png");
            this.btnImageList.Images.SetKeyName(5, "6.png");
            this.btnImageList.Images.SetKeyName(6, "7.png");
            this.btnImageList.Images.SetKeyName(7, "8.png");
            this.btnImageList.Images.SetKeyName(8, "9.png");
            this.btnImageList.Images.SetKeyName(9, "10.png");
            this.btnImageList.Images.SetKeyName(10, "11.png");
            this.btnImageList.Images.SetKeyName(11, "12.png");
            this.btnImageList.Images.SetKeyName(12, "13.png");
            this.btnImageList.Images.SetKeyName(13, "14.png");
            this.btnImageList.Images.SetKeyName(14, "15.png");
            this.btnImageList.Images.SetKeyName(15, "16.png");
            this.btnImageList.Images.SetKeyName(16, "17.png");
            this.btnImageList.Images.SetKeyName(17, "18.png");
            this.btnImageList.Images.SetKeyName(18, "19.png");
            this.btnImageList.Images.SetKeyName(19, "20.png");
            this.btnImageList.Images.SetKeyName(20, "21.png");
            this.btnImageList.Images.SetKeyName(21, "22.png");
            this.btnImageList.Images.SetKeyName(22, "23.png");
            this.btnImageList.Images.SetKeyName(23, "24.png");
            this.btnImageList.Images.SetKeyName(24, "25.png");
            this.btnImageList.Images.SetKeyName(25, "26.png");
            this.btnImageList.Images.SetKeyName(26, "27.png");
            this.btnImageList.Images.SetKeyName(27, "28.png");
            this.btnImageList.Images.SetKeyName(28, "29.png");
            this.btnImageList.Images.SetKeyName(29, "30.png");
            this.btnImageList.Images.SetKeyName(30, "31.png");
            this.btnImageList.Images.SetKeyName(31, "32.png");
            this.btnImageList.Images.SetKeyName(32, "257.gif");
            this.btnImageList.Images.SetKeyName(33, "1279369174_user_business_boss.png");
            this.btnImageList.Images.SetKeyName(34, "Action_Save_32x32.png");
            this.btnImageList.Images.SetKeyName(35, "add.png");
            this.btnImageList.Images.SetKeyName(36, "base.png");
            this.btnImageList.Images.SetKeyName(37, "bg1.jpg");
            this.btnImageList.Images.SetKeyName(38, "bg2.jpg");
            this.btnImageList.Images.SetKeyName(39, "boy.png");
            this.btnImageList.Images.SetKeyName(40, "boy16.png");
            this.btnImageList.Images.SetKeyName(41, "check-64.png");
            this.btnImageList.Images.SetKeyName(42, "clouse.png");
            this.btnImageList.Images.SetKeyName(43, "clouse_hover.png");
            this.btnImageList.Images.SetKeyName(44, "CurrentActive.gif");
            this.btnImageList.Images.SetKeyName(45, "DatabaseSwitchboardManager.png");
            this.btnImageList.Images.SetKeyName(46, "del.png");
            this.btnImageList.Images.SetKeyName(47, "delete_2.png");
            this.btnImageList.Images.SetKeyName(48, "drug_long.png");
            this.btnImageList.Images.SetKeyName(49, "drug_s.png");
            this.btnImageList.Images.SetKeyName(50, "drug_temp.png");
            this.btnImageList.Images.SetKeyName(51, "edit.png");
            this.btnImageList.Images.SetKeyName(52, "end.gif");
            this.btnImageList.Images.SetKeyName(53, "ErrorActive.gif");
            this.btnImageList.Images.SetKeyName(54, "exit.png");
            this.btnImageList.Images.SetKeyName(55, "export.png");
            this.btnImageList.Images.SetKeyName(56, "favorite_love.png");
            this.btnImageList.Images.SetKeyName(57, "favorite_love_0.png");
            this.btnImageList.Images.SetKeyName(58, "FileSaveAsWord97_2003.png");
            this.btnImageList.Images.SetKeyName(59, "Fouse1.png");
            this.btnImageList.Images.SetKeyName(60, "Fouse2.png");
            this.btnImageList.Images.SetKeyName(61, "Fouse3.png");
            this.btnImageList.Images.SetKeyName(62, "Green Ball.png");
            this.btnImageList.Images.SetKeyName(63, "gril.png");
            this.btnImageList.Images.SetKeyName(64, "gril16.png");
            this.btnImageList.Images.SetKeyName(65, "guanbi.png");
            this.btnImageList.Images.SetKeyName(66, "guide_visit.png");
            this.btnImageList.Images.SetKeyName(67, "head.png");
            this.btnImageList.Images.SetKeyName(68, "head1.png");
            this.btnImageList.Images.SetKeyName(69, "head文字.png");
            this.btnImageList.Images.SetKeyName(70, "home1.jpg");
            this.btnImageList.Images.SetKeyName(71, "Image1.bmp");
            this.btnImageList.Images.SetKeyName(72, "jiantou_0.png");
            this.btnImageList.Images.SetKeyName(73, "jiantou_1.png");
            this.btnImageList.Images.SetKeyName(74, "jiantou_shu_0.png");
            this.btnImageList.Images.SetKeyName(75, "jiantou_shu_1.png");
            this.btnImageList.Images.SetKeyName(76, "jieshuziliao.png");
            this.btnImageList.Images.SetKeyName(77, "kaishizhiliao.png");
            this.btnImageList.Images.SetKeyName(78, "line.png");
            this.btnImageList.Images.SetKeyName(79, "login.png");
            this.btnImageList.Images.SetKeyName(80, "logo.png");
            this.btnImageList.Images.SetKeyName(81, "logop.png");
            this.btnImageList.Images.SetKeyName(82, "long_recipe_s.png");
            this.btnImageList.Images.SetKeyName(83, "MailSelectNames.png");
            this.btnImageList.Images.SetKeyName(84, "minimize.png");
            this.btnImageList.Images.SetKeyName(85, "minimize_hover.png");
            this.btnImageList.Images.SetKeyName(86, "minus_2.png");
            this.btnImageList.Images.SetKeyName(87, "nan.png");
            this.btnImageList.Images.SetKeyName(88, "nan_s.png");
            this.btnImageList.Images.SetKeyName(89, "next.png");
            this.btnImageList.Images.SetKeyName(90, "NonActive.gif");
            this.btnImageList.Images.SetKeyName(91, "nv.png");
            this.btnImageList.Images.SetKeyName(92, "nv_s.png");
            this.btnImageList.Images.SetKeyName(93, "offline.gif");
            this.btnImageList.Images.SetKeyName(94, "OPENSTEP_EUI Write Document.png");
            this.btnImageList.Images.SetKeyName(95, "OverActive.gif");
            this.btnImageList.Images.SetKeyName(96, "paiban_0.png");
            this.btnImageList.Images.SetKeyName(97, "paiban_0_s.png");
            this.btnImageList.Images.SetKeyName(98, "paiban_1.png");
            this.btnImageList.Images.SetKeyName(99, "paiban_1_s.png");
            this.btnImageList.Images.SetKeyName(100, "pie-chart_graph.png");
            this.btnImageList.Images.SetKeyName(101, "preview.png");
            this.btnImageList.Images.SetKeyName(102, "print.png");
            this.btnImageList.Images.SetKeyName(103, "PrintAreaMenu.png");
            this.btnImageList.Images.SetKeyName(104, "Red Ball.png");
            this.btnImageList.Images.SetKeyName(105, "right.png");
            this.btnImageList.Images.SetKeyName(106, "save.png");
            this.btnImageList.Images.SetKeyName(107, "search_16.png");
            this.btnImageList.Images.SetKeyName(108, "shuaxin.png");
            this.btnImageList.Images.SetKeyName(109, "shuqian.gif");
            this.btnImageList.Images.SetKeyName(110, "startend.gif");
            this.btnImageList.Images.SetKeyName(111, "SynchronizeData.png");
            this.btnImageList.Images.SetKeyName(112, "tshadowdown.png");
            this.btnImageList.Images.SetKeyName(113, "tshadowdownleft.png");
            this.btnImageList.Images.SetKeyName(114, "tshadowdownright.png");
            this.btnImageList.Images.SetKeyName(115, "tshadowright.png");
            this.btnImageList.Images.SetKeyName(116, "tshadowtopright.png");
            this.btnImageList.Images.SetKeyName(117, "wancheng.png");
            this.btnImageList.Images.SetKeyName(118, "white_image.png");
            this.btnImageList.Images.SetKeyName(119, "Word.png");
            this.btnImageList.Images.SetKeyName(120, "xueguan_0.png");
            this.btnImageList.Images.SetKeyName(121, "xueguan_0_s.png");
            this.btnImageList.Images.SetKeyName(122, "xueguan_1.png");
            this.btnImageList.Images.SetKeyName(123, "xueguan_1_s.png");
            this.btnImageList.Images.SetKeyName(124, "Yellow Ball.png");
            this.btnImageList.Images.SetKeyName(125, "yizhu_0.png");
            this.btnImageList.Images.SetKeyName(126, "yizhu_0_s.png");
            this.btnImageList.Images.SetKeyName(127, "yizhu_1.png");
            this.btnImageList.Images.SetKeyName(128, "yizhu_1_s.png");
            this.btnImageList.Images.SetKeyName(129, "yizhu_s.png");
            this.btnImageList.Images.SetKeyName(130, "zhiliao_0.png");
            this.btnImageList.Images.SetKeyName(131, "zhiliao_0_s.png");
            this.btnImageList.Images.SetKeyName(132, "zhiliao_1.png");
            this.btnImageList.Images.SetKeyName(133, "zhiliao_1_s.png");
            this.btnImageList.Images.SetKeyName(134, "zhiliaozhong.png");
            this.btnImageList.Images.SetKeyName(135, "zhunbei.png");
            this.btnImageList.Images.SetKeyName(136, "并发症.png");
            this.btnImageList.Images.SetKeyName(137, "并发症及其他共计.png");
            this.btnImageList.Images.SetKeyName(138, "出库信息.png");
            this.btnImageList.Images.SetKeyName(139, "处方确定.png");
            this.btnImageList.Images.SetKeyName(140, "处方未开.png");
            this.btnImageList.Images.SetKeyName(141, "处方未确定.png");
            this.btnImageList.Images.SetKeyName(142, "传染病.png");
            this.btnImageList.Images.SetKeyName(143, "打开模版.png");
            this.btnImageList.Images.SetKeyName(144, "导管手术列数.png");
            this.btnImageList.Images.SetKeyName(145, "复用记录.png");
            this.btnImageList.Images.SetKeyName(146, "感染检查统计.png");
            this.btnImageList.Images.SetKeyName(147, "工程师资料.png");
            this.btnImageList.Images.SetKeyName(148, "工作量统计.png");
            this.btnImageList.Images.SetKeyName(149, "关注1.png");
            this.btnImageList.Images.SetKeyName(150, "规律透析比例.png");
            this.btnImageList.Images.SetKeyName(151, "耗材厂商.png");
            this.btnImageList.Images.SetKeyName(152, "耗材信息.png");
            this.btnImageList.Images.SetKeyName(153, "护士资料.png");
            this.btnImageList.Images.SetKeyName(154, "患者签到.png");
            this.btnImageList.Images.SetKeyName(155, "监测指标.png");
            this.btnImageList.Images.SetKeyName(156, "检查化验.png");
            this.btnImageList.Images.SetKeyName(157, "健康宣教.png");
            this.btnImageList.Images.SetKeyName(158, "库存盘点.png");
            this.btnImageList.Images.SetKeyName(159, "临时医嘱.png");
            this.btnImageList.Images.SetKeyName(160, "模版管理.png");
            this.btnImageList.Images.SetKeyName(161, "入库信息.png");
            this.btnImageList.Images.SetKeyName(162, "数据汇总.png");
            this.btnImageList.Images.SetKeyName(163, "统计.png");
            this.btnImageList.Images.SetKeyName(164, "透析参数.png");
            this.btnImageList.Images.SetKeyName(165, "透析处方.png");
            this.btnImageList.Images.SetKeyName(166, "透析记录.png");
            this.btnImageList.Images.SetKeyName(167, "透析男女比例.png");
            this.btnImageList.Images.SetKeyName(168, "透析年龄段.png");
            this.btnImageList.Images.SetKeyName(169, "透析小结.png");
            this.btnImageList.Images.SetKeyName(170, "维持性患者监测.png");
            this.btnImageList.Images.SetKeyName(171, "维持性患者质量检测指标统计.png");
            this.btnImageList.Images.SetKeyName(172, "系统数据设定.png");
            this.btnImageList.Images.SetKeyName(173, "血透记录单.png");
            this.btnImageList.Images.SetKeyName(174, "血透数据设定.png");
            this.btnImageList.Images.SetKeyName(175, "药品厂商.png");
            this.btnImageList.Images.SetKeyName(176, "药品主档.png");
            this.btnImageList.Images.SetKeyName(177, "一般.png");
            this.btnImageList.Images.SetKeyName(178, "医生资料.png");
            this.btnImageList.Images.SetKeyName(179, "院感检查.png");
            this.btnImageList.Images.SetKeyName(180, "质量管理.png");
            this.btnImageList.Images.SetKeyName(181, "质量管理基础数据统计.png");
            this.btnImageList.Images.SetKeyName(182, "治疗数据.png");
            this.btnImageList.Images.SetKeyName(183, "重点.png");
            // 
            // DXSimpleButton
            // 
            this.ImageIndex = 0;
            this.ImageList = this.btnImageList;
            ((System.ComponentModel.ISupportInitialize)(this.btnImageList)).EndInit();
            this.ResumeLayout(false);

        }


        public DXSimpleButton()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            this.btnImageList.Clear();
            this.btnImageList.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();

            }
            base.Dispose(disposing);
        }



    }
}
