namespace Hemo.Client.UI.Patient
{
    partial class IntegratedPatientInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegratedPatientInfo));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.treePatientArea = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtCardNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPATIENT_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbxTIME_TYPE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtHEMODIALYSIS_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl70 = new DevExpress.XtraEditors.LabelControl();
            this.txtAge = new DevExpress.XtraEditors.TextEdit();
            this.txtBIRTHDAY = new DevExpress.XtraEditors.DateEdit();
            this.labelControl76 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl78 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl86 = new DevExpress.XtraEditors.LabelControl();
            this.txtNAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl87 = new DevExpress.XtraEditors.LabelControl();
            this.txtSEX = new DevExpress.XtraEditors.TextEdit();
            this.txtDIAGNOSE = new DevExpress.XtraEditors.TextEdit();
            this.txtMARITAL = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treePatientArea)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPATIENT_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTIME_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHEMODIALYSIS_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBIRTHDAY.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBIRTHDAY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIAGNOSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMARITAL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(736, 432);
            this.splitContainerControl1.SplitterPosition = 208;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.busyIndicator1);
            this.groupControl2.Controls.Add(this.treePatientArea);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(208, 432);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "类别列表";
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(38, 224);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 0;
            // 
            // treePatientArea
            // 
            this.treePatientArea.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treePatientArea.Appearance.FocusedCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.treePatientArea.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treePatientArea.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treePatientArea.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treePatientArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePatientArea.KeyFieldName = "ITEM_ID";
            this.treePatientArea.Location = new System.Drawing.Point(2, 23);
            this.treePatientArea.Name = "treePatientArea";
            this.treePatientArea.OptionsBehavior.Editable = false;
            this.treePatientArea.OptionsSelection.InvertSelection = true;
            this.treePatientArea.OptionsView.ShowColumns = false;
            this.treePatientArea.OptionsView.ShowIndicator = false;
            this.treePatientArea.ParentFieldName = "PARENT";
            this.treePatientArea.Size = new System.Drawing.Size(204, 407);
            this.treePatientArea.TabIndex = 1;
            this.treePatientArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treePatientArea_MouseDown);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "名称";
            this.treeListColumn1.FieldName = "ITEM_NAME";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "值";
            this.treeListColumn2.FieldName = "ITEM_ID";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.18518F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.81481F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 432);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 154);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(516, 275);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnOk);
            this.groupControl1.Controls.Add(this.txtCardNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtPATIENT_ID);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cbxTIME_TYPE);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtHEMODIALYSIS_ID);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl70);
            this.groupControl1.Controls.Add(this.txtAge);
            this.groupControl1.Controls.Add(this.txtBIRTHDAY);
            this.groupControl1.Controls.Add(this.labelControl76);
            this.groupControl1.Controls.Add(this.labelControl78);
            this.groupControl1.Controls.Add(this.labelControl86);
            this.groupControl1.Controls.Add(this.txtNAME);
            this.groupControl1.Controls.Add(this.labelControl87);
            this.groupControl1.Controls.Add(this.txtSEX);
            this.groupControl1.Controls.Add(this.txtDIAGNOSE);
            this.groupControl1.Controls.Add(this.txtMARITAL);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(516, 145);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "患者信息";
            // 
            // btnOk
            // 
            this.btnOk.ImageIndex = 6;
            this.btnOk.ImageList = this.imageList1;
            this.btnOk.Location = new System.Drawing.Point(418, 118);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 23);
            this.btnOk.TabIndex = 316;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "女患者.ico");
            this.imageList1.Images.SetKeyName(1, "男患者.ico");
            this.imageList1.Images.SetKeyName(2, "boy.png");
            this.imageList1.Images.SetKeyName(3, "boy16.png");
            this.imageList1.Images.SetKeyName(4, "gril.png");
            this.imageList1.Images.SetKeyName(5, "gril16.png");
            this.imageList1.Images.SetKeyName(6, "处方确定.png");
            // 
            // txtCardNo
            // 
            this.txtCardNo.Enabled = false;
            this.txtCardNo.EnterMoveNextControl = true;
            this.txtCardNo.Location = new System.Drawing.Point(367, 30);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCardNo.Properties.MaxLength = 20;
            this.txtCardNo.Size = new System.Drawing.Size(136, 21);
            this.txtCardNo.TabIndex = 314;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(332, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 315;
            this.labelControl3.Text = "卡号";
            // 
            // txtPATIENT_ID
            // 
            this.txtPATIENT_ID.Enabled = false;
            this.txtPATIENT_ID.EnterMoveNextControl = true;
            this.txtPATIENT_ID.Location = new System.Drawing.Point(184, 30);
            this.txtPATIENT_ID.Name = "txtPATIENT_ID";
            this.txtPATIENT_ID.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtPATIENT_ID.Properties.MaxLength = 20;
            this.txtPATIENT_ID.Size = new System.Drawing.Size(119, 21);
            this.txtPATIENT_ID.TabIndex = 314;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(154, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 315;
            this.labelControl1.Text = "ID号";
            // 
            // cbxTIME_TYPE
            // 
            this.cbxTIME_TYPE.EditValue = "";
            this.cbxTIME_TYPE.Enabled = false;
            this.cbxTIME_TYPE.Location = new System.Drawing.Point(367, 91);
            this.cbxTIME_TYPE.Name = "cbxTIME_TYPE";
            this.cbxTIME_TYPE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cbxTIME_TYPE.Properties.Appearance.Options.UseForeColor = true;
            this.cbxTIME_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTIME_TYPE.Properties.Items.AddRange(new object[] {
            "",
            "门诊",
            "住院",
            "医保"});
            this.cbxTIME_TYPE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTIME_TYPE.Size = new System.Drawing.Size(136, 21);
            this.cbxTIME_TYPE.TabIndex = 312;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(308, 94);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 313;
            this.labelControl2.Text = "病人类型";
            // 
            // txtHEMODIALYSIS_ID
            // 
            this.txtHEMODIALYSIS_ID.EditValue = "";
            this.txtHEMODIALYSIS_ID.Location = new System.Drawing.Point(47, 30);
            this.txtHEMODIALYSIS_ID.Name = "txtHEMODIALYSIS_ID";
            this.txtHEMODIALYSIS_ID.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtHEMODIALYSIS_ID.Properties.Appearance.Options.UseForeColor = true;
            this.txtHEMODIALYSIS_ID.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtHEMODIALYSIS_ID.Size = new System.Drawing.Size(101, 21);
            this.txtHEMODIALYSIS_ID.TabIndex = 310;
            this.txtHEMODIALYSIS_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHEMODIALYSIS_ID_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 33);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 311;
            this.labelControl4.Text = "透析号";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 122);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 309;
            this.labelControl5.Text = "诊断";
            // 
            // labelControl70
            // 
            this.labelControl70.Location = new System.Drawing.Point(154, 92);
            this.labelControl70.Name = "labelControl70";
            this.labelControl70.Size = new System.Drawing.Size(24, 14);
            this.labelControl70.TabIndex = 309;
            this.labelControl70.Text = "婚姻";
            // 
            // txtAge
            // 
            this.txtAge.EditValue = "";
            this.txtAge.Enabled = false;
            this.txtAge.Location = new System.Drawing.Point(47, 89);
            this.txtAge.Name = "txtAge";
            this.txtAge.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtAge.Properties.Appearance.Options.UseForeColor = true;
            this.txtAge.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtAge.Size = new System.Drawing.Size(101, 21);
            this.txtAge.TabIndex = 303;
            // 
            // txtBIRTHDAY
            // 
            this.txtBIRTHDAY.EditValue = null;
            this.txtBIRTHDAY.Enabled = false;
            this.txtBIRTHDAY.Location = new System.Drawing.Point(367, 58);
            this.txtBIRTHDAY.Name = "txtBIRTHDAY";
            this.txtBIRTHDAY.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtBIRTHDAY.Properties.Appearance.Options.UseForeColor = true;
            this.txtBIRTHDAY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBIRTHDAY.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtBIRTHDAY.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBIRTHDAY.Size = new System.Drawing.Size(136, 21);
            this.txtBIRTHDAY.TabIndex = 302;
            // 
            // labelControl76
            // 
            this.labelControl76.Location = new System.Drawing.Point(308, 65);
            this.labelControl76.Name = "labelControl76";
            this.labelControl76.Size = new System.Drawing.Size(48, 14);
            this.labelControl76.TabIndex = 308;
            this.labelControl76.Text = "出生日期";
            // 
            // labelControl78
            // 
            this.labelControl78.Location = new System.Drawing.Point(17, 92);
            this.labelControl78.Name = "labelControl78";
            this.labelControl78.Size = new System.Drawing.Size(24, 14);
            this.labelControl78.TabIndex = 307;
            this.labelControl78.Text = "年龄";
            // 
            // labelControl86
            // 
            this.labelControl86.Location = new System.Drawing.Point(154, 65);
            this.labelControl86.Name = "labelControl86";
            this.labelControl86.Size = new System.Drawing.Size(24, 14);
            this.labelControl86.TabIndex = 306;
            this.labelControl86.Text = "性别";
            // 
            // txtNAME
            // 
            this.txtNAME.EditValue = "";
            this.txtNAME.Location = new System.Drawing.Point(47, 62);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtNAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtNAME.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtNAME.Size = new System.Drawing.Size(101, 21);
            this.txtNAME.TabIndex = 301;
            this.txtNAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNAME_KeyDown);
            // 
            // labelControl87
            // 
            this.labelControl87.Location = new System.Drawing.Point(17, 65);
            this.labelControl87.Name = "labelControl87";
            this.labelControl87.Size = new System.Drawing.Size(24, 14);
            this.labelControl87.TabIndex = 305;
            this.labelControl87.Text = "姓名";
            // 
            // txtSEX
            // 
            this.txtSEX.EditValue = "";
            this.txtSEX.Enabled = false;
            this.txtSEX.Location = new System.Drawing.Point(184, 62);
            this.txtSEX.Name = "txtSEX";
            this.txtSEX.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtSEX.Properties.Appearance.Options.UseForeColor = true;
            this.txtSEX.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtSEX.Size = new System.Drawing.Size(119, 21);
            this.txtSEX.TabIndex = 300;
            // 
            // txtDIAGNOSE
            // 
            this.txtDIAGNOSE.EditValue = "";
            this.txtDIAGNOSE.Enabled = false;
            this.txtDIAGNOSE.Location = new System.Drawing.Point(47, 119);
            this.txtDIAGNOSE.Name = "txtDIAGNOSE";
            this.txtDIAGNOSE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDIAGNOSE.Properties.Appearance.Options.UseForeColor = true;
            this.txtDIAGNOSE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDIAGNOSE.Size = new System.Drawing.Size(256, 21);
            this.txtDIAGNOSE.TabIndex = 304;
            // 
            // txtMARITAL
            // 
            this.txtMARITAL.EditValue = "";
            this.txtMARITAL.Enabled = false;
            this.txtMARITAL.Location = new System.Drawing.Point(184, 89);
            this.txtMARITAL.Name = "txtMARITAL";
            this.txtMARITAL.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtMARITAL.Properties.Appearance.Options.UseForeColor = true;
            this.txtMARITAL.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtMARITAL.Size = new System.Drawing.Size(119, 21);
            this.txtMARITAL.TabIndex = 304;
            // 
            // IntegratedPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 432);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "IntegratedPatientInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "透析患者综合信息";
            this.Load += new System.EventHandler(this.IntegratedPatientInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treePatientArea)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPATIENT_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTIME_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHEMODIALYSIS_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBIRTHDAY.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBIRTHDAY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIAGNOSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMARITAL.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtPATIENT_ID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbxTIME_TYPE;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHEMODIALYSIS_ID;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl70;
        private DevExpress.XtraEditors.TextEdit txtAge;
        private DevExpress.XtraEditors.DateEdit txtBIRTHDAY;
        private DevExpress.XtraEditors.LabelControl labelControl76;
        private DevExpress.XtraEditors.LabelControl labelControl78;
        private DevExpress.XtraEditors.LabelControl labelControl86;
        private DevExpress.XtraEditors.TextEdit txtNAME;
        private DevExpress.XtraEditors.LabelControl labelControl87;
        private DevExpress.XtraEditors.TextEdit txtSEX;
        private DevExpress.XtraEditors.TextEdit txtMARITAL;
        private DevExpress.XtraEditors.TextEdit txtCardNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDIAGNOSE;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraTreeList.TreeList treePatientArea;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    }
}
