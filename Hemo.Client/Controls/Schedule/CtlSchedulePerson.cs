/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:自定义控件
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
    public partial class CtlSchedulePerson : UserControl
    {
        #region 变量

        private const string CAPTION = "病患排班";
        private DateTime _dialysisDate;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private static string BanChi = string.Empty;
        #endregion

        #region 属性

        public int DayOfWeek
        {
            private set;
            get;
        }

        public ConfigModel.MED_COMMON_ITEMLISTRow AreaRow
        {
            private set;
            get;
        }

        public ConfigModel.MED_COMMON_ITEMLISTRow BedRow
        {
            private set;
            get;
        }

        public MachineModel.MED_DIALYSIS_MACHINERow MachineRow
        {
            private set;
            get;
        }

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

        public ConfigModel.MED_COMMON_ITEMLISTDataTable PurifierModelDataTable
        {
            private set;
            get;
        }

        public HemodialysisModel.MED_HEMO_RECIPEDataTable RecipeDataTable
        {
            private set;
            get;
        }

        public string Comments
        {
            //set
            //{
            //    this.labComments.Text = value;
            //}
            //get
            //{
            //    return this.labComments.Text;
            //}

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

        /// <summary>
        /// 当前排班类型
        /// </summary>
        private string _scheduleType = string.Empty;
        public string ScheduleType
        {
            set
            {
                _scheduleType = value;
            }
            get
            {
                return _scheduleType;
            }
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
        #endregion

        #region 构造函数

        public CtlSchedulePerson(int dayOfWeek, ConfigModel.MED_COMMON_ITEMLISTRow areaRow, ConfigModel.MED_COMMON_ITEMLISTRow bedRow, MachineModel.MED_DIALYSIS_MACHINERow machineRow)
        {
            this.InitializeComponent();

            this.DayOfWeek = dayOfWeek;
            this.AreaRow = areaRow;
            this.BedRow = bedRow;
            this.MachineRow = machineRow;

            this.labMachineName.Text = machineRow.MACHINE_NAME;
        }

        #endregion

        #region 方法

        public void SetBaseInfo(DateTime dialysisDate, ConfigModel.MED_COMMON_ITEMLISTDataTable purifierModelDataTable, HemodialysisModel.MED_HEMO_RECIPEDataTable recipeDataTable)
        {
            this._dialysisDate = dialysisDate;
            this.PurifierModelDataTable = purifierModelDataTable;
            this.RecipeDataTable = recipeDataTable;
        }

        public void SetPatientInfo(PatientModel.MED_PATIENTSRow patientRow, PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow)
        {
            if (patientRow == null)
                return;
            this.PatientRow = patientRow;
            this.PatientScheduleRow = patientScheduleRow;
            if (patientScheduleRow != null)
                BanChi = patientScheduleRow.BANCI_ID.ToString();

            SchedulePersonDragManager.Instance.RemoveHemoID(this.DayOfWeek, this.PatientRow.HEMODIALYSIS_ID);
            SchedulePersonDragManager.Instance.AddHemoID(this.DayOfWeek, this.PatientRow.HEMODIALYSIS_ID);

            this.labPatientName.ForeColor = this.IsPatientTreated ? Color.Red : Color.Black;
            this.labPatientName.Text = this.PatientRow.NAME;

            //if (!this.PatientScheduleRow.IsCOMMENTSNull())
            //    this.labComments.Text = this.PatientScheduleRow.COMMENTS;

            //    if (!this.PatientScheduleRow.IsRECIPE_IDNull())
            //    {
            //        this.RECIPE_ID = this.PatientScheduleRow.RECIPE_ID;

            //        this.labTreatInfo.Text = this.RecipeDataTable.FindByRECIPE_ID(this.RECIPE_ID)["PURIFICATION_MODE_NAME"].ToString();
            //    }

            //    if (!this.PatientScheduleRow.IsPURIFIER_MODEL_IDNull())
            //    {
            //        this.PURIFIER_MODEL_ID = this.PatientScheduleRow.PURIFIER_MODEL_ID;

            //        this.labTreatInfo.Text = string.Format("{0} {1}", this.labTreatInfo.Text, this.PurifierModelDataTable.FindByITEM_ID(this.PURIFIER_MODEL_ID).ITEM_NAME);
            //    }
        }

        public void SetOtherInfo(string comments, string recipeID, string purifierModelID)
        {
            if (!string.IsNullOrEmpty(comments))
                this.Comments = this.labComments.Text = comments;

            if (!string.IsNullOrEmpty(recipeID) && !string.IsNullOrEmpty(purifierModelID))
            {
                this.RECIPE_ID = recipeID;
                this.PURIFIER_MODEL_ID = purifierModelID;

                if (this.RecipeDataTable.FindByRECIPE_ID(recipeID)["PURIFICATION_MODE_NAME"].ToString() != null)
                {
                    this.labTreatInfo.Text = this.RecipeDataTable.FindByRECIPE_ID(recipeID)["PURIFICATION_MODE_NAME"].ToString();
                }

                if (this.PurifierModelDataTable.FindByITEM_ID(purifierModelID) != null)
                    this.labTreatInfo.Text = string.Format("{0} {1}", this.labTreatInfo.Text, this.PurifierModelDataTable.FindByITEM_ID(purifierModelID).ITEM_NAME);
            }
        }

        public void ClearInfo()
        {
            if (this.PatientRow != null)
                SchedulePersonDragManager.Instance.RemoveHemoID(this.DayOfWeek, this.PatientRow.HEMODIALYSIS_ID);

            this.RECIPE_ID = this.PURIFIER_MODEL_ID = string.Empty;
            this.Comments = this.labComments.Text = string.Empty;
            this.labPatientName.Text = this.labComments.Text = this.labTreatInfo.Text = string.Empty;

            this.PatientRow = null;
            this.PatientScheduleRow = null;
        }

        #endregion

        #region 事件

        private void CtlSchedulePerson_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(typeof(SchedulePersonDragInfo))))
                e.Effect = DragDropEffects.Copy;
        }

        private void CtlSchedulePerson_DragDrop(object sender, DragEventArgs e)
        {
            //接受拖放的数据
            SchedulePersonDragInfo dragInfo = e.Data.GetData(e.Data.GetFormats()[0]) as SchedulePersonDragInfo;

            if (dragInfo == null)
                return;
            else if (dragInfo.SourceCtlSchedulePerson != null && this == dragInfo.SourceCtlSchedulePerson)
                return;
            else
            {
                //HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(dragInfo.PatientRow.HEMODIALYSIS_ID);

                //recipeTable = Utility.GetSubTable(recipeTable, "status = 1") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

                //if (recipeTable.Rows.Count == 0)
                //{
                //    XtraMessageBox.Show("该病人尚未确认透析处方，不能进行排班。", "病患排班");
                //    return;
                //}

                //HemodialysisModel.MED_HEMO_RECIPERow recipeRow = recipeTable.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                #region MyRegion


                DateTime dialysisdate = DateTime.Now.Date.AddDays(1 - Convert.ToInt32(DateTime.Now.Date.DayOfWeek.ToString("d")) + this.DayOfWeek);
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable currentDT = _patientScheduleService.GetPatientScheduleSignle(dialysisdate, dragInfo.PatientRow.HEMODIALYSIS_ID);

                if (currentDT.Rows.Count > 0)
                {
                    var currentDtRow = currentDT.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;

                    //排班模板不验证数据
                    if (ScheduleType.Length == 0)
                    {
                        if (BanChi != currentDtRow.BANCI_ID || string.IsNullOrEmpty(BanChi))
                        {
                            string BanCiName = currentDtRow.BANCI_ID == "1" ? "上午" : (currentDtRow.BANCI_ID == "2" ? "下午" : "晚");
                            string WaringStr = string.Format("同一天内不能有相同的患者！\r\n患者已存在于{0}班{3},{1}的{2}床。", BanCiName, currentDtRow.AREANAME, currentDtRow.BEDNAME, dialysisdate.Date.ToString("yyyy-MM-dd"));

                            XtraMessageBox.Show(WaringStr, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                #endregion
                //是否从患者树状控件中拖放的患者
                bool isFromTreeList = dragInfo.SourceCtlSchedulePerson == null;
                //该区块是否存在相同的患者
                bool isContainsHemoID = SchedulePersonDragManager.Instance.ContainsHemoID(this.DayOfWeek, dragInfo.PatientRow.HEMODIALYSIS_ID);

                if ((isFromTreeList && isContainsHemoID)
                    //2个区块之间拖放
                    || (!isFromTreeList && isContainsHemoID && this.DayOfWeek != dragInfo.SourceCtlSchedulePerson.DayOfWeek)
                    //交换
                    || this.PatientRow != null && !isFromTreeList && this.DayOfWeek != dragInfo.SourceCtlSchedulePerson.DayOfWeek && SchedulePersonDragManager.Instance.ContainsHemoID(dragInfo.SourceCtlSchedulePerson.DayOfWeek, this.PatientRow.HEMODIALYSIS_ID)
                    )
                {
                    if (ScheduleType.Length == 0)
                    {
                        XtraMessageBox.Show("同一班次内不能有相同的患者！", CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (this.PatientRow == null) //没有患者使用
                    {
                        if (isFromTreeList)
                        {
                            this.SetPatientInfo(dragInfo.PatientRow, null);

                            //this.SetOtherInfo(string.Empty, recipeRow.RECIPE_ID, recipeRow.FIRST_PURIFIER_MODEL);
                            this.SetOtherInfo(string.Empty, string.Empty, string.Empty);
                        }
                        else
                        {
                            PatientModel.MED_PATIENTSRow tempPatientRow = dragInfo.PatientRow;
                            PatientScheduleModel.MED_PATIENT_SCHEDULERow tempPatientScheduleRow = dragInfo.SourceCtlSchedulePerson.PatientScheduleRow;
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

        private void 检验记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PatientRow != null)
            {
                LabFrm labFrm = new LabFrm(this.PatientRow);
                labFrm.ShowDialog();
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //此处没有必要进行再次数据加载，而且他只加载排班的，如果是模板就完蛋 2013-12-25 horace.jc
            //if (this.PatientRow != null)
            //{
            //    PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = this._patientScheduleService.GetPatientScheduleSignle(this._dialysisDate, this.PatientRow.HEMODIALYSIS_ID);

            //    if (patientScheduleDataTable.Rows.Count > 0)
            //    {
            //        PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow = patientScheduleDataTable.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;

            //        this.SetPatientInfo(this.PatientRow, patientScheduleRow);

            //        this.SetOtherInfo(
            //            patientScheduleRow.IsREMARKNull() ? string.Empty : patientScheduleRow.REMARK,
            //            patientScheduleRow.IsRECIPE_IDNull() ? string.Empty : patientScheduleRow.RECIPE_ID,
            //            patientScheduleRow.IsPURIFIER_MODEL_IDNull() ? string.Empty : patientScheduleRow.PURIFIER_MODEL_ID);
            //    }
            //}

            if (this.IsPatientTreated)
                this.tsmiSetTreatInfo.Enabled = this.tsmiEditComments.Enabled = this.tsmiDeletePatient.Enabled = false;
            else
                this.tsmiSetTreatInfo.Enabled = this.tsmiEditComments.Enabled = this.tsmiDeletePatient.Enabled = this.PatientRow != null;

            if (this.IsSureReciped)
                this.tsmiSetTreatInfo.Enabled = this.tsmiEditComments.Enabled = this.tsmiDeletePatient.Enabled = false;
            else
                this.tsmiSetTreatInfo.Enabled = this.tsmiEditComments.Enabled = this.tsmiDeletePatient.Enabled = this.PatientRow != null;
        }

        /// <summary>
        /// 设置治疗方式和透析器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSetTreatInfo_Click(object sender, EventArgs e)
        {
            SetTreatInfo setTreatInfo = new SetTreatInfo(this.PatientRow.HEMODIALYSIS_ID, this.RECIPE_ID, this.PURIFIER_MODEL_ID);

            if (setTreatInfo.ShowDialog() == DialogResult.Yes)
            {
                this.RECIPE_ID = setTreatInfo.RECIPE_ID;
                this.PURIFIER_MODEL_ID = setTreatInfo.PURIFIER_MODEL_ID;

                this.labTreatInfo.Text = string.Format("{0} {1}", setTreatInfo.PURIFICATION_MODE_NAME, setTreatInfo.PURIFIER_MODEL_NAME);
            }
        }

        /// <summary>
        /// 维护备注信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditComments_Click(object sender, EventArgs e)
        {
            EditPatientSchedule editPatientSchedule = new EditPatientSchedule(this.labComments.Text);

            if (editPatientSchedule.ShowDialog() == DialogResult.Yes && editPatientSchedule.Tag != null)
                this.Comments = this.labComments.Text = editPatientSchedule.Tag.ToString();
        }

        /// <summary>
        /// 删除患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeletePatient_Click(object sender, System.EventArgs e)
        {
            this.ClearInfo();
        }

        private void labPatientName_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.IsPatientTreated)
                return;
            if (this.IsSureReciped)
                return;

            if (e.Button == MouseButtons.Left)
            {
                this.labPatientName.DoDragDrop(
                    new SchedulePersonDragInfo()
                    {
                        SourceCtlSchedulePerson = this,
                        PatientRow = this.PatientRow
                    }, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        #endregion

    }
}
