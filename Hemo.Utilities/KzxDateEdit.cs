/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:DateEdit、VistaDateEditCalendar控件类
 * 创建标识:吕志强-2013年7月27日
 * 
 * 修改时间:2013年11月4日
 * 修改人:顾伟伟
 * 修改描述:新增方法CustomVistaDateEditCalendar
 * ----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Calendar;

namespace Hemo.Utilities
{
    /// <summary>
    /// DateEdit日期控件
    /// </summary>
    public partial class KzxDateEdit : DateEdit
    {
        public KzxDateEdit()
        {
            InitializeComponent();
            Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            Properties.DisplayFormat.FormatString = "yyyy-MM";
            Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Properties.Mask.EditMask = "yyyy-MM";
            Properties.ShowToday = false;
        }

        public KzxDateEdit(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override PopupBaseForm CreatePopupForm()
        {
            if (Properties.VistaDisplayMode == DevExpress.Utils.DefaultBoolean.True)
                return new CustomVistaPopupDateEditForm(this);
            return new PopupDateEditForm(this);
        }

    }

    /// <summary>
    /// VistaPopupDateEditForm表单
    /// </summary>
    public class CustomVistaPopupDateEditForm : VistaPopupDateEditForm
    {
        public CustomVistaPopupDateEditForm(DateEdit ownerEdit) : base(ownerEdit) { }
        protected override DateEditCalendar CreateCalendar()
        {
            return new CustomVistaDateEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue);
        }
    }

    /// <summary>
    /// VistaDateEditCalendar日期修改
    /// </summary>
    public class CustomVistaDateEditCalendar : VistaDateEditCalendar
    {
        public CustomVistaDateEditCalendar(RepositoryItemDateEdit item, object editDate) : base(item, editDate) { }

        protected override void Init()
        {
            base.Init();
            this.View = DateEditCalendarViewType.YearInfo;
        }

        protected override void OnItemClick(DevExpress.XtraEditors.Calendar.CalendarHitInfo hitInfo)
        {
            DayNumberCellInfo cell = hitInfo.HitObject as DayNumberCellInfo;
            if (View == DateEditCalendarViewType.YearInfo)
            {
                DateTime dt = new DateTime(DateTime.Year, cell.Date.Month, 1, 0, 0, 0);

                DateTime tempDate = dt.AddMonths(1).AddDays(-1);
                tempDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 23, 59, 59);
                OnDateTimeCommit(tempDate, false);
            }
            else
                base.OnItemClick(hitInfo);
        }
    }
}
