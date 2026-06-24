namespace Hemo.Client.UI.Patient
{
    partial class TemplateOfDealthFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateOfDealthFrm));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.iReName = new System.Windows.Forms.ToolStripMenuItem();
            this.iNew = new System.Windows.Forms.ToolStripMenuItem();
            this.txtEVENTANALYSIS = new DevExpress.XtraEditors.MemoEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCORRECTIVEACTIONS = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.iNewCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnInsert = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCancle = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTANALYSIS.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCORRECTIVEACTIONS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.treeList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panelControl2.Size = new System.Drawing.Size(194, 388);
            this.panelControl2.TabIndex = 3;
            // 
            // treeList
            // 
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.ImageIndexFieldName = "EXTEND1";
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.AutoChangeParent = false;
            this.treeList.OptionsBehavior.AutoPopulateColumns = false;
            this.treeList.OptionsBehavior.Editable = false;
            this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeList.OptionsView.ShowColumns = false;
            this.treeList.OptionsView.ShowHorzLines = false;
            this.treeList.OptionsView.ShowIndicator = false;
            this.treeList.OptionsView.ShowVertLines = false;
            this.treeList.ParentFieldName = "HEMODIALYSIS_ID";
            this.treeList.RowHeight = 20;
            this.treeList.SelectImageList = this.imageList1;
            this.treeList.Size = new System.Drawing.Size(193, 388);
            this.treeList.TabIndex = 8;
            this.treeList.UseDisabledStatePainter = false;
            this.treeList.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeList_BeforeFocusNode);
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
            this.treeList.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.treeList_CustomDrawNodeImages);
            this.treeList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseClick);
            this.treeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "EXTEND2";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "RecentlyUsedFolder.png");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "FileSaveAsWord97_2003.png");
            this.imageList1.Images.SetKeyName(3, "FunctionsTextInsertGallesry.png");
            // 
            // iReName
            // 
            this.iReName.Name = "iReName";
            this.iReName.Size = new System.Drawing.Size(140, 22);
            this.iReName.Text = "重命名";
            this.iReName.Click += new System.EventHandler(this.iReName_Click);
            // 
            // iNew
            // 
            this.iNew.Image = ((System.Drawing.Image)(resources.GetObject("iNew.Image")));
            this.iNew.Name = "iNew";
            this.iNew.Size = new System.Drawing.Size(140, 22);
            this.iNew.Text = "新建(N)";
            this.iNew.Click += new System.EventHandler(this.iNew_Click);
            // 
            // txtEVENTANALYSIS
            // 
            this.txtEVENTANALYSIS.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEVENTANALYSIS.Location = new System.Drawing.Point(0, 47);
            this.txtEVENTANALYSIS.Name = "txtEVENTANALYSIS";
            this.txtEVENTANALYSIS.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEVENTANALYSIS.Properties.Appearance.Options.UseBackColor = true;
            this.txtEVENTANALYSIS.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.txtEVENTANALYSIS.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtEVENTANALYSIS.Properties.MaxLength = 1990;
            this.txtEVENTANALYSIS.Size = new System.Drawing.Size(578, 150);
            this.txtEVENTANALYSIS.TabIndex = 0;
            this.txtEVENTANALYSIS.TextChanged += new System.EventHandler(this.txtEVENTANALYSIS_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.panel1.Size = new System.Drawing.Size(774, 390);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(194, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(580, 388);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtCORRECTIVEACTIONS);
            this.panel3.Controls.Add(this.panelControl3);
            this.panel3.Controls.Add(this.txtEVENTANALYSIS);
            this.panel3.Controls.Add(this.panelControl4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panel3.Size = new System.Drawing.Size(578, 388);
            this.panel3.TabIndex = 22;
            // 
            // txtCORRECTIVEACTIONS
            // 
            this.txtCORRECTIVEACTIONS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCORRECTIVEACTIONS.Location = new System.Drawing.Point(0, 236);
            this.txtCORRECTIVEACTIONS.Name = "txtCORRECTIVEACTIONS";
            this.txtCORRECTIVEACTIONS.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCORRECTIVEACTIONS.Properties.Appearance.Options.UseBackColor = true;
            this.txtCORRECTIVEACTIONS.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.txtCORRECTIVEACTIONS.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtCORRECTIVEACTIONS.Properties.MaxLength = 1990;
            this.txtCORRECTIVEACTIONS.Size = new System.Drawing.Size(578, 152);
            this.txtCORRECTIVEACTIONS.TabIndex = 0;
            this.txtCORRECTIVEACTIONS.EditValueChanged += new System.EventHandler(this.txtEVENTANALYSIS_TextChanged);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 197);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(578, 39);
            this.panelControl3.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(132, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "针对死亡事件的改进措施";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.labelControl2);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(0, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(578, 45);
            this.panelControl4.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "事件及原因分析";
            // 
            // iNewCategory
            // 
            this.iNewCategory.Image = ((System.Drawing.Image)(resources.GetObject("iNewCategory.Image")));
            this.iNewCategory.Name = "iNewCategory";
            this.iNewCategory.Size = new System.Drawing.Size(140, 22);
            this.iNewCategory.Text = "添加分类(A)";
            this.iNewCategory.Click += new System.EventHandler(this.iNewCategory_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNewCategory,
            this.iNew,
            this.iDelete,
            this.iReName});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(141, 92);
            // 
            // iDelete
            // 
            this.iDelete.Image = ((System.Drawing.Image)(resources.GetObject("iDelete.Image")));
            this.iDelete.Name = "iDelete";
            this.iDelete.Size = new System.Drawing.Size(140, 22);
            this.iDelete.Text = "删除(D)";
            this.iDelete.Click += new System.EventHandler(this.iDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(606, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.iSaveButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnInsert);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 390);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(774, 40);
            this.panelControl1.TabIndex = 8;
            // 
            // btnInsert
            // 
            this.btnInsert.ImageIndex = 5;
            this.btnInsert.Location = new System.Drawing.Point(525, 10);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 25);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "插入";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.ImageIndex = 3;
            this.btnCancle.Location = new System.Drawing.Point(687, 10);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 25);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "关闭";
            this.btnCancle.Click += new System.EventHandler(this.iCloseButton_Click);
            // 
            // TemplateOfDealthFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 430);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.MaximumSize = new System.Drawing.Size(790, 468);
            this.MinimumSize = new System.Drawing.Size(790, 468);
            this.Name = "TemplateOfDealthFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者终止透析登记模板";
            this.Load += new System.EventHandler(this.TemplateOfDealthFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTANALYSIS.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCORRECTIVEACTIONS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem iReName;
        private System.Windows.Forms.ToolStripMenuItem iNew;
        private DevExpress.XtraEditors.MemoEdit txtEVENTANALYSIS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem iNewCategory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem iDelete;
        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnCancle;
        private Controls.DXSimpleButton btnInsert;
        private DevExpress.XtraEditors.MemoEdit txtCORRECTIVEACTIONS;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}