namespace Hemo.Client.UI.Machine
{
    partial class UserDetialFrmNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_MachineNum = new DevExpress.XtraEditors.LabelControl();
            this.panel_Bottom = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            this.simpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.label_year_mon = new DevExpress.XtraEditors.LabelControl();
            this.txtCREATE_DATE = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEdit_machine = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.DEALWITH = new DevExpress.XtraEditors.MemoEdit();
            this.MACHINE_CHECK = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit_BanCi = new DevExpress.XtraEditors.LookUpEdit();
            this.user_time = new DevExpress.XtraEditors.SpinEdit();
            this.DEGASSING = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.MACHINE_ALARM = new DevExpress.XtraEditors.MemoEdit();
            this.WORKING = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lopOperator = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Bottom)).BeginInit();
            this.panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEALWITH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MACHINE_CHECK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_BanCi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEGASSING.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MACHINE_ALARM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WORKING.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_MachineNum
            // 
            this.label_MachineNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_MachineNum.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label_MachineNum.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MachineNum.Appearance.Options.UseBackColor = true;
            this.label_MachineNum.Appearance.Options.UseFont = true;
            this.label_MachineNum.Location = new System.Drawing.Point(6, 32);
            this.label_MachineNum.Name = "label_MachineNum";
            this.label_MachineNum.Size = new System.Drawing.Size(56, 20);
            this.label_MachineNum.TabIndex = 2;
            this.label_MachineNum.Text = "机器编号";
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panel_Bottom.Controls.Add(this.simpleButton2);
            this.panel_Bottom.Controls.Add(this.simpleButton1);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 176);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel_Bottom.Size = new System.Drawing.Size(723, 46);
            this.panel_Bottom.TabIndex = 3;
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageIndex = 3;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton2.Location = new System.Drawing.Point(622, 10);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(85, 27);
            this.simpleButton2.TabIndex = 0;
            this.simpleButton2.Text = "取  消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageIndex = 7;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(531, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 27);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label_year_mon
            // 
            this.label_year_mon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_year_mon.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label_year_mon.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_year_mon.Appearance.Options.UseBackColor = true;
            this.label_year_mon.Appearance.Options.UseFont = true;
            this.label_year_mon.Location = new System.Drawing.Point(395, 32);
            this.label_year_mon.Name = "label_year_mon";
            this.label_year_mon.Size = new System.Drawing.Size(56, 20);
            this.label_year_mon.TabIndex = 2;
            this.label_year_mon.Text = "创建日期";
            // 
            // txtCREATE_DATE
            // 
            this.txtCREATE_DATE.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCREATE_DATE.EditValue = null;
            this.txtCREATE_DATE.EnterMoveNextControl = true;
            this.txtCREATE_DATE.Location = new System.Drawing.Point(460, 32);
            this.txtCREATE_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCREATE_DATE.Name = "txtCREATE_DATE";
            this.txtCREATE_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCREATE_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCREATE_DATE.Properties.Appearance.Options.UseFont = true;
            this.txtCREATE_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.txtCREATE_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCREATE_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCREATE_DATE.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCREATE_DATE.Size = new System.Drawing.Size(248, 23);
            this.txtCREATE_DATE.TabIndex = 42;
            this.txtCREATE_DATE.EditValueChanged += new System.EventHandler(this.txtCREATE_DATE_EditValueChanged);
            // 
            // lookUpEdit_machine
            // 
            this.lookUpEdit_machine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lookUpEdit_machine.EditValue = "";
            this.lookUpEdit_machine.EnterMoveNextControl = true;
            this.lookUpEdit_machine.Location = new System.Drawing.Point(83, 32);
            this.lookUpEdit_machine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEdit_machine.Name = "lookUpEdit_machine";
            this.lookUpEdit_machine.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lookUpEdit_machine.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lookUpEdit_machine.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit_machine.Properties.Appearance.Options.UseForeColor = true;
            this.lookUpEdit_machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_machine.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_NAME", "名称", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FLNAME", "分类"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_MODEL", "型号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("THERAPEUTIC_PROPERTIES", "治疗方式"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("QYNAME", "区域"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CWNAME", "床位")});
            this.lookUpEdit_machine.Properties.DisplayMember = "MACHINE_MODEL";
            this.lookUpEdit_machine.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lookUpEdit_machine.Properties.NullText = "";
            this.lookUpEdit_machine.Properties.ValueMember = "MACHINE_ID";
            this.lookUpEdit_machine.Size = new System.Drawing.Size(248, 23);
            this.lookUpEdit_machine.TabIndex = 15;
            this.lookUpEdit_machine.EditValueChanged += new System.EventHandler(this.lookUpEdit_machine_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl8.Location = new System.Drawing.Point(6, 139);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(76, 30);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "使用时间";
            // 
            // DEALWITH
            // 
            this.DEALWITH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DEALWITH.Location = new System.Drawing.Point(460, 119);
            this.DEALWITH.Margin = new System.Windows.Forms.Padding(0);
            this.DEALWITH.Name = "DEALWITH";
            this.DEALWITH.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.DEALWITH.Properties.Appearance.Options.UseForeColor = true;
            this.DEALWITH.Size = new System.Drawing.Size(247, 17);
            this.DEALWITH.TabIndex = 8;
            // 
            // MACHINE_CHECK
            // 
            this.MACHINE_CHECK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MACHINE_CHECK.Location = new System.Drawing.Point(83, 89);
            this.MACHINE_CHECK.Margin = new System.Windows.Forms.Padding(0);
            this.MACHINE_CHECK.Name = "MACHINE_CHECK";
            this.MACHINE_CHECK.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.MACHINE_CHECK.Properties.Appearance.Options.UseForeColor = true;
            this.MACHINE_CHECK.Size = new System.Drawing.Size(248, 17);
            this.MACHINE_CHECK.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl4.Location = new System.Drawing.Point(5, 84);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 24);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "自检情况";
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl6.Location = new System.Drawing.Point(6, 113);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 24);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "消毒功能";
            // 
            // lookUpEdit_BanCi
            // 
            this.lookUpEdit_BanCi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lookUpEdit_BanCi.EditValue = "";
            this.lookUpEdit_BanCi.EnterMoveNextControl = true;
            this.lookUpEdit_BanCi.Location = new System.Drawing.Point(83, 60);
            this.lookUpEdit_BanCi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEdit_BanCi.Name = "lookUpEdit_BanCi";
            this.lookUpEdit_BanCi.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lookUpEdit_BanCi.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lookUpEdit_BanCi.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit_BanCi.Properties.Appearance.Options.UseForeColor = true;
            this.lookUpEdit_BanCi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_BanCi.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lookUpEdit_BanCi.Properties.NullText = "";
            this.lookUpEdit_BanCi.Size = new System.Drawing.Size(247, 23);
            this.lookUpEdit_BanCi.TabIndex = 44;
            this.lookUpEdit_BanCi.EditValueChanged += new System.EventHandler(this.lookUpEdit_BanCi_EditValueChanged);
            // 
            // user_time
            // 
            this.user_time.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.user_time.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.user_time.Location = new System.Drawing.Point(83, 148);
            this.user_time.Name = "user_time";
            this.user_time.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.user_time.Properties.Appearance.Options.UseForeColor = true;
            this.user_time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.user_time.Properties.Mask.EditMask = "T";
            this.user_time.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.user_time.Size = new System.Drawing.Size(248, 21);
            this.user_time.TabIndex = 43;
            // 
            // DEGASSING
            // 
            this.DEGASSING.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DEGASSING.Location = new System.Drawing.Point(83, 121);
            this.DEGASSING.Margin = new System.Windows.Forms.Padding(0);
            this.DEGASSING.Name = "DEGASSING";
            this.DEGASSING.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.DEGASSING.Properties.Appearance.Options.UseForeColor = true;
            this.DEGASSING.Size = new System.Drawing.Size(248, 17);
            this.DEGASSING.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(339, 151);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(7, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "H";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl2.Location = new System.Drawing.Point(9, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(73, 24);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "班次";
            // 
            // labelControl11
            // 
            this.labelControl11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl11.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl11.Location = new System.Drawing.Point(384, 113);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(60, 24);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "   处理";
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl9.Location = new System.Drawing.Point(388, 144);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(72, 26);
            this.labelControl9.TabIndex = 309;
            this.labelControl9.Text = "  操作者";
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl7.Location = new System.Drawing.Point(388, 79);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(67, 25);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "  运转情况";
            // 
            // MACHINE_ALARM
            // 
            this.MACHINE_ALARM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MACHINE_ALARM.Location = new System.Drawing.Point(460, 63);
            this.MACHINE_ALARM.Margin = new System.Windows.Forms.Padding(0);
            this.MACHINE_ALARM.Name = "MACHINE_ALARM";
            this.MACHINE_ALARM.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.MACHINE_ALARM.Properties.Appearance.Options.UseForeColor = true;
            this.MACHINE_ALARM.Size = new System.Drawing.Size(247, 17);
            this.MACHINE_ALARM.TabIndex = 311;
            // 
            // WORKING
            // 
            this.WORKING.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WORKING.Location = new System.Drawing.Point(461, 88);
            this.WORKING.Margin = new System.Windows.Forms.Padding(0);
            this.WORKING.Name = "WORKING";
            this.WORKING.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.WORKING.Properties.Appearance.Options.UseForeColor = true;
            this.WORKING.Size = new System.Drawing.Size(247, 17);
            this.WORKING.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl5.Location = new System.Drawing.Point(395, 50);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 29);
            this.labelControl5.TabIndex = 310;
            this.labelControl5.Text = "报警情况";
            // 
            // lopOperator
            // 
            this.lopOperator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lopOperator.EditValue = "";
            this.lopOperator.EnterMoveNextControl = true;
            this.lopOperator.Location = new System.Drawing.Point(461, 148);
            this.lopOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lopOperator.Name = "lopOperator";
            this.lopOperator.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lopOperator.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lopOperator.Properties.Appearance.Options.UseFont = true;
            this.lopOperator.Properties.Appearance.Options.UseForeColor = true;
            this.lopOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lopOperator.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lopOperator.Properties.NullText = "";
            this.lopOperator.Size = new System.Drawing.Size(247, 23);
            this.lopOperator.TabIndex = 14;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtCREATE_DATE);
            this.panelControl1.Controls.Add(this.lookUpEdit_machine);
            this.panelControl1.Controls.Add(this.label_MachineNum);
            this.panelControl1.Controls.Add(this.lopOperator);
            this.panelControl1.Controls.Add(this.label_year_mon);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.WORKING);
            this.panelControl1.Controls.Add(this.MACHINE_ALARM);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl11);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.DEGASSING);
            this.panelControl1.Controls.Add(this.user_time);
            this.panelControl1.Controls.Add(this.lookUpEdit_BanCi);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.MACHINE_CHECK);
            this.panelControl1.Controls.Add(this.DEALWITH);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.panelControl1.Size = new System.Drawing.Size(723, 222);
            this.panelControl1.TabIndex = 312;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // UserDetialFrmNew
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 222);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panelControl1);
            this.Name = "UserDetialFrmNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "血透机使用情况记录";
            this.Load += new System.EventHandler(this.UserDetialFrmNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel_Bottom)).EndInit();
            this.panel_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEALWITH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MACHINE_CHECK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_BanCi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEGASSING.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MACHINE_ALARM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WORKING.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl label_MachineNum;
        private DevExpress.XtraEditors.PanelControl panel_Bottom;
        private Hemo.Client.Controls.DXSimpleButton simpleButton2;
        private Hemo.Client.Controls.DXSimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl label_year_mon;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_machine;
        private DevExpress.XtraEditors.DateEdit txtCREATE_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.MemoEdit DEALWITH;
        private DevExpress.XtraEditors.MemoEdit MACHINE_CHECK;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_BanCi;
        private DevExpress.XtraEditors.SpinEdit user_time;
        private DevExpress.XtraEditors.MemoEdit DEGASSING;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MemoEdit MACHINE_ALARM;
        private DevExpress.XtraEditors.MemoEdit WORKING;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lopOperator;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}