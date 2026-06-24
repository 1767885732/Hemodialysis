/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:排班控件人员
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Client.UI.PatientSchedule;
using Hemo.IService.Config;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.Controls.Schedule
{
    public partial class CtlSchedulePersonNew : GroupControl
    {
        #region 变量属性
        private const string CAPTION = "病患排班";
        /// <summary>
        /// 父容器
        /// </summary>
        public SchedulePanelNew ParentFrame { get; set; }
        public PatientModel.MED_PATIENTSRow PatientRow
        {
            private set;
            get;
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULERow PatientScheduleRow
        {
            private set;
            get;
        }

        /// <summary>
        /// 该病人是否已经被治疗过
        /// </summary>
        public bool IsPatientTreated
        {
            get
            {
                if (this.PatientScheduleRow == null)
                    return false;
                else
                    return !this.PatientScheduleRow.IsSTART_TIMENull() || !this.PatientScheduleRow.IsEND_TIMENull();
            }
        }
        /// <summary>
        /// 该病人是否已经被确认过处方
        /// </summary>
        public bool IsSureReciped
        {
            get
            {
                if (this.PatientScheduleRow == null)
                    return false;
                else
                    return !this.PatientScheduleRow.IsRECIPE_IDNull() || !this.PatientScheduleRow.IsPURIFIER_MODEL_IDNull();
            }
        }
        public string Comments
        {
            private set;
            get;
        }

        public string RECIPE_ID
        {
            private set;
            get;
        }

        public string PURIFIER_MODEL_ID
        {
            private set;
            get;
        }

        public HemodialysisModel.MED_HEMO_RECIPEDataTable RecipeDataTable
        {
            private set;
            get;
        }

        public ConfigModel.MED_COMMON_ITEMLISTDataTable PurifierModelDataTable
        {
            private set;
            get;
        }
        #endregion

        public CtlSchedulePersonNew()
        {
            this.InitializeComponent();
            this.ShowCaption = false;
            
        }


        #region 方法

        public void ClearInfo()
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.AliceBlue);
            this.PatientRow = null;
            this.PatientScheduleRow = null;
        }
        public void SetPatientInfo(PatientModel.MED_PATIENTSRow patientRow, PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow)
        {
            if (patientRow == null)
                return;
            this.PatientRow = patientRow;
            this.PatientScheduleRow = patientScheduleRow;
            //if (patientScheduleRow != null)
            //BanChi = patientScheduleRow.BANCI_ID.ToString();
            var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
            //姓名
            var cardLoc = new PointF(10, 5);
            System.Drawing.Graphics g = this.CreateGraphics();
            DrawString(g, string.Format("{0}", PatientRow.NAME), font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
        }

        public void SetOtherInfo(string comments, string recipeID, string purifierModelID)
        {
            if (!string.IsNullOrEmpty(comments))
            {
                this.Comments = comments;
                var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                //Commnets
                var cardLoc = new PointF(30, 5);
                System.Drawing.Graphics g = this.CreateGraphics();
                DrawString(g, comments, font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
            }

            if (!string.IsNullOrEmpty(recipeID) && !string.IsNullOrEmpty(purifierModelID))
            {
                this.RECIPE_ID = recipeID;
                this.PURIFIER_MODEL_ID = purifierModelID;
                string threantInfo = string.Empty;
                if (this.RecipeDataTable.FindByRECIPE_ID(recipeID)["PURIFICATION_MODE_NAME"].ToString() != null)
                {
                    threantInfo = this.RecipeDataTable.FindByRECIPE_ID(recipeID)["PURIFICATION_MODE_NAME"].ToString();
                    var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                    //threantInfo
                    var cardLoc = new PointF(20, 5);
                    System.Drawing.Graphics g = this.CreateGraphics();
                    DrawString(g, threantInfo, font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
                }
                if (this.PurifierModelDataTable.FindByITEM_ID(purifierModelID) != null)
                {
                    string pruifierModeName = string.Format("{0} {1}", threantInfo, this.PurifierModelDataTable.FindByITEM_ID(purifierModelID).ITEM_NAME);
                    var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                    //threantInfo
                    var cardLoc = new PointF(20, 5);
                    System.Drawing.Graphics g = this.CreateGraphics();
                    DrawString(g, pruifierModeName, font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
                }
            }
        }
           /// <summary>
        /// 设置治疗方式和透析器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSetTreatInfo_Click(object sender, EventArgs e) {
            SetTreatInfo setTreatInfo = new SetTreatInfo(this.PatientRow.HEMODIALYSIS_ID, this.RECIPE_ID, this.PURIFIER_MODEL_ID);

            if (setTreatInfo.ShowDialog() == DialogResult.Yes) {
                this.RECIPE_ID = setTreatInfo.RECIPE_ID;
                this.PURIFIER_MODEL_ID = setTreatInfo.PURIFIER_MODEL_ID;

                string threadInfo = string.Format("{0} {1}", setTreatInfo.PURIFICATION_MODE_NAME, setTreatInfo.PURIFIER_MODEL_NAME);

                var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                //threantInfo
                var cardLoc = new PointF(20, 5);
                System.Drawing.Graphics g = this.CreateGraphics();
                DrawString(g, threadInfo, font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
            }
        }

        /// <summary>
        /// 维护备注信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditComments_Click(object sender, EventArgs e) {
            EditPatientSchedule editPatientSchedule = new EditPatientSchedule(string.Empty);

            if (editPatientSchedule.ShowDialog() == DialogResult.Yes && editPatientSchedule.Tag != null)
            {
                this.Comments  = editPatientSchedule.Tag.ToString();
                var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                //Commnets
                var cardLoc = new PointF(30, 5);
                System.Drawing.Graphics g = this.CreateGraphics();
                DrawString(g, editPatientSchedule.Tag.ToString(), font, this.IsPatientTreated ? Brushes.Red : Brushes.Black, cardLoc);
            }
        }

        /// <summary>
        /// 删除患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeletePatient_Click(object sender, System.EventArgs e) {
            this.ClearInfo();
        }

        private void CtlSchedulePersonNew_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.IsPatientTreated)
                return;
            if (this.IsSureReciped)
                return;

            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(
                    new SchedulePersonDragInfo()
                    {
                        SourceCtlSchedulePersonNew = this,
                        PatientRow = this.PatientRow
                    }, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        private void 检验记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PatientRow != null)
            {
                LabFrm labFrm = new LabFrm(this.PatientRow);
                labFrm.ShowDialog();
            }
        }
        #endregion

      

        #region 事件

        private void CtlSchedulePersonNew_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(typeof(SchedulePersonDragInfo))))
                e.Effect = DragDropEffects.Copy;
        }

        private void CtlSchedulePersonNew_DragDrop(object sender, DragEventArgs e)
        {
            //接受拖放的数据
            SchedulePersonDragInfo dragInfo = e.Data.GetData(e.Data.GetFormats()[0]) as SchedulePersonDragInfo;

            if (dragInfo == null && dragInfo.SourceCtlSchedulePersonNew != null && dragInfo.SourceCtlSchedulePersonNew == this)
                return;
            else
            {

                #region 验证同一天内不能有相同的患者。。。

                #endregion

                //是否从患者树状控件中拖放的患者
                bool isFromTreeList = dragInfo.SourceCtlSchedulePersonNew == null;
                //该区块是否存在相同的患者
                bool isContainsHemoID = false;

                if ((isFromTreeList && isContainsHemoID))
                {
                    #region 判断同一班次内不能有相同的患者

                    #endregion
                }
                else
                {
                    if (this.PatientRow == null) //没有患者使用
                    {
                        if (isFromTreeList)
                        {
                            this.SetPatientInfo(dragInfo.PatientRow, null);

                            this.SetOtherInfo(string.Empty, string.Empty, string.Empty);
                        }
                        else
                        {
                            
                            PatientModel.MED_PATIENTSRow tempPatientRow = dragInfo.PatientRow;
                            if (tempPatientRow == null || dragInfo.SourceCtlSchedulePersonNew == null)
                                return;
                            PatientScheduleModel.MED_PATIENT_SCHEDULERow tempPatientScheduleRow = dragInfo.SourceCtlSchedulePersonNew.PatientScheduleRow;
                            string tempComments = dragInfo.SourceCtlSchedulePerson.Comments;
                            string tempRecipeID = dragInfo.SourceCtlSchedulePerson.RECIPE_ID;
                            string tempPurifierModelID = dragInfo.SourceCtlSchedulePerson.PURIFIER_MODEL_ID;

                            dragInfo.SourceCtlSchedulePerson.ClearInfo();

                            this.SetPatientInfo(tempPatientRow, tempPatientScheduleRow);

                            this.SetOtherInfo(tempComments, tempRecipeID, tempPurifierModelID);
                        }
                    }
                    else
                    {
                        if (isFromTreeList) //从患者树状控件中拖放的患者
                        {
                            if (this.IsPatientTreated)
                            {
                                XtraMessageBox.Show("已经开始治疗或者结束治疗的患者不能替换！", CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }
                            if (this.IsSureReciped)
                            {
                                XtraMessageBox.Show("已经确认处方的患者不能替换，请取消处方！", CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }
                            if (DialogResult.Yes == XtraMessageBox.Show("该血透机已被其他患者使用，是否替换成当前患者？", CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                this.ClearInfo();

                                this.SetPatientInfo(dragInfo.PatientRow, null);

                                //this.SetOtherInfo(string.Empty, recipeRow.RECIPE_ID, recipeRow.FIRST_PURIFIER_MODEL);
                                this.SetOtherInfo(string.Empty, string.Empty, string.Empty);
                            }
                        }
                        else
                        {
                            if (this.IsPatientTreated)
                            {
                                XtraMessageBox.Show("已经开始治疗或者结束治疗的患者不能交换！", CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }
                            if (this.IsSureReciped)
                            {
                                XtraMessageBox.Show("已经确认处方的患者不能替换，请取消处方！", CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }
                            if (DialogResult.Yes == XtraMessageBox.Show("该血透机已被其他患者使用，是否交换患者？", CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                PatientModel.MED_PATIENTSRow tempPatientRow = this.PatientRow;
                                PatientScheduleModel.MED_PATIENT_SCHEDULERow tempPatientScheduleRow = this.PatientScheduleRow;
                                string tempComments = this.Comments;
                                string tempRecipeID = this.RECIPE_ID;
                                string tempPurifierModelID = this.PURIFIER_MODEL_ID;

                                this.ClearInfo();

                                this.SetPatientInfo(dragInfo.PatientRow, dragInfo.SourceCtlSchedulePerson.PatientScheduleRow);

                                this.SetOtherInfo(dragInfo.SourceCtlSchedulePerson.Comments, dragInfo.SourceCtlSchedulePerson.RECIPE_ID, dragInfo.SourceCtlSchedulePerson.PURIFIER_MODEL_ID);

                                dragInfo.SourceCtlSchedulePerson.ClearInfo();

                                dragInfo.SourceCtlSchedulePerson.SetPatientInfo(tempPatientRow, tempPatientScheduleRow);

                                dragInfo.SourceCtlSchedulePerson.SetOtherInfo(tempComments, tempRecipeID, tempPurifierModelID);
                            }
                        }
                    }
                }
            }
        }

        private void DrawString(Graphics g, object str, Font font, Brush brush, PointF pf)
        {
            g.DrawString(string.Format("{0}", str), font, brush, pf);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void CtlSchedulePersonNew_Paint(object sender, PaintEventArgs e)
        {
            if (ParentFrame == null)
            {
                return;
            }
            var font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
            //姓名
            var cardLoc = new PointF(10, 5);
            DrawString(e.Graphics, string.Format("{0}", PatientRow.NAME), font, Brushes.Black, cardLoc);
        }

        #endregion


     
    }
}
