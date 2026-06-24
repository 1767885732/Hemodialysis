/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者制卡管理类
// 创建时间：2016-05-17
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Patient 
{
    public partial class EditPatientCard : HemoBaseFrm
    {
        #region 构造函数

        public EditPatientCard()
        {
            InitializeComponent();
            InizationData();
        }

        #endregion
        
        #region 变量属性
        public DrugModel.MED_PATIENTS_CARDDataTable _patientCard = new DrugModel.MED_PATIENTS_CARDDataTable();
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        public int icDev; // 通讯设备标识符--初始化串口通讯接口
        public Int16 currentState;//设备的当前状态
        public int secNumber; //扇区号
        public string HemoId { get; set; }

        private string Sec = "1";
        private string Key = "ffffffffffff";
        private string Data = "00112233445566778899aabbccddeeff";
        private string Value = "10000";
        #endregion

        #region 方法
        /// <summary>
        /// 连串口
        /// </summary>
        private void MingTechConnect()
        {
            currentState = 0;
            byte[] ver = new byte[30];
            //设备当前状态
            currentState = MingTechComm.lib_ver(ver);
            //接口版本号
            string sver = System.Text.Encoding.ASCII.GetString(ver);
            lbSoftVer.Text = sver;//显示接口的版本号
            Int16 port = 0;//定义串口一
            int baud = 9600;//写义波特率
            icDev = MingTechComm.rf_init(port, baud);//初始化串口
            if (icDev > 0)
            {
                lbResult.Text = "打开串口成功！";
                byte[] status = new byte[30];
                //打开串口成功后获取状态
                currentState = MingTechComm.rf_get_status(icDev, status);
                lbHardVer.Text = System.Text.Encoding.ASCII.GetString(status);
                MingTechComm.rf_beep(icDev, 5);//蜂鸣一下
            }
            else
            {
               
                lbResult.Text = "打开串口失败！";
                XtraMessageBox.Show(lbResult.Text);
                MingTechDisConnect();

                this.btnWriteData.Enabled = false;

                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            } 

        }
        /// <summary>
        /// 断开串口
        /// </summary>
        private void MingTechDisConnect()
        {
            //根据串口状态，去断开连接
            currentState = MingTechComm.rf_exit(icDev);
            if (currentState == 0)
                lbResult.Text = "断开连接成功！";
            else
                lbResult.Text = "断开连接失败！";
        }
        /// <summary>
        /// 进行一次蜂鸣
        /// </summary>
        private void MingTechBeep()
        {
            //根据串口状态，去断开连接
            currentState = MingTechComm.rf_beep(icDev, 10);
            if (currentState == 0)
                lbResult.Text = "蜂鸣成功！";
            else
                lbResult.Text = "蜂鸣失败！";
        }
        /// <summary>
        /// 寻卡操作 
        /// 1：重置串口
        /// 2：rf_anticoll
        /// 3:rf_select
        /// </summary>
        private void MingTechSeekCard()
        {
            UInt16 tagtype = 0;
            byte size = 0;
            uint snr = 0;

            MingTechMiFareOne.rf_reset(icDev, 3);//重置串口
            //请求串口是否成功
            currentState = MingTechMiFareOne.rf_request(icDev, 1, out tagtype);
            if (currentState != 0)
            {
                //重置失败
                lbResult.Text = "请求失败，请重新放卡！";
                btnSeekCard.Visible = true;
                this.btnWriteData.Enabled = false;
                return;
            }

            currentState = MingTechMiFareOne.rf_anticoll(icDev, 0, out snr);
            if (currentState != 0)
            {
                lbResult.Text = "anticoll error!";
                btnSeekCard.Visible = true;
                this.btnWriteData.Enabled = false;

                return;
            }
            lbSnr.Text = snr.ToString();
            //string snrstr = "";
            //snrstr = snr.ToString("X");
            //lbSnr.Text = Convert.ToInt32(snrstr, 16).ToString();

            currentState = MingTechMiFareOne.rf_select(icDev, snr, out size);
            if (currentState != 0)
            {
                lbResult.Text = "select error!";
                btnSeekCard.Visible = true;
                this.btnWriteData.Enabled = false;
                return;
            }
            lbResult.Text = "寻卡成功！";
            btnSeekCard.Visible = false;  
        }
        /// <summary>
        /// 卡认证
        /// </summary>
        private void MingTechAuth()
        {

            byte[] key1 = new byte[17];
            byte[] key2 = new byte[7];
            int i = 0;
            string skey = textKey.Text;
            int keylen = textKey.TextLength;
            //密码校验
            if (keylen != 12)
            {
                lbResult.Text = "请正确输入密码，密码长度不对！";
                return;
            }
            //扇区号长度检验
            if (textSec.TextLength < 1)
            {
                lbResult.Text = "请输入扇区号！";
                return;
            }
            //获取扇区号
            secNumber = Convert.ToInt32(textSec.Text, 10);
            //扇区号为1到14
            if (secNumber < 1 || secNumber > 15)
            {
                lbResult.Text = "扇区号不正确！";
                return;
            }

            for (i = 0; i < keylen; i++)
            {
                if (skey[i] >= '0' && skey[i] <= '9')
                    continue;
                if (skey[i] <= 'a' && skey[i] <= 'f')
                    continue;
                if (skey[i] <= 'A' && skey[i] <= 'F')
                    continue;
            }
            if (i != keylen)
            {
                lbResult.Text = "密码必须为十六进制数！";
                return;

            }
            key1 = Encoding.ASCII.GetBytes(skey);
            MingTechComm.a_hex(key1, key2, 12);
            //装载密码
            currentState = MingTechComm.rf_load_key(icDev, 0, secNumber, key2);
            if (currentState != 0)
            {
                lbResult.Text = "装载密码失败！";
                return;
            }
            //认证
            currentState = MingTechMiFareOne.rf_authentication(icDev, 0, secNumber);
            if (currentState != 0)
            {
                lbResult.Text = "认证失败！";
                //this.btnAuth.Visible = true;
                this.btnWriteData.Enabled = false;
            }
            else
            {
                lbResult.Text = "认证成功！";
                //this.btnAuth.Visible = false;
                this.btnWriteData.Enabled = true;

            }

        }
        /// <summary>
        /// 读取卡数据
        /// </summary>
        private void MingTechReadData()
        {

            int i = 0;
            byte[] data = new byte[16];
            byte[] buff = new byte[32];

            for (i = 0; i < 16; i++)
                data[i] = 0;
            for (i = 0; i < 32; i++)
                buff[i] = 0;
            currentState = MingTechMiFareOne.rf_read(icDev, secNumber * 4 + 1, data);
            if (currentState == 0)
            {
                MingTechComm.hex_a(data, buff, 16);
                textData.Text = System.Text.Encoding.ASCII.GetString(buff);
               
                lbResult.Text = "读数据成功！";
            }
            else
                lbResult.Text = "读数据失败！";
        }
        /// <summary>
        /// 对卡进行数据的写操作
        /// </summary>
        private void MingTechWriteData()
        {
            int i = 0;
            byte[] databuff = new byte[16];
            byte[] buff = new byte[32];

            if (textData.TextLength < 32)
            {
                lbResult.Text = "请正确输入数据，数据长度不对！";
                return;
            }
            ///获取写的数据
            string data = textData.Text;
            for (i = 0; i < data.Length; i++)
            {
                if (data[i] >= '0' && data[i] <= '9')
                    continue;
                if (data[i] <= 'a' && data[i] <= 'f')
                    continue;
                if (data[i] <= 'A' && data[i] <= 'F')
                    continue;
            }
            if (i != data.Length)
            {
                lbResult.Text = "数据必须为十六进制数！";
                return;
            }
            //数据写入前进行ASCII码的转换
            buff = Encoding.ASCII.GetBytes(data);
            MingTechComm.a_hex(buff, databuff, 32);
            currentState = MingTechMiFareOne.rf_write(icDev, secNumber * 4 + 1, databuff);
            if (currentState == 0)
                lbResult.Text = "写数据成功！";
            else
                lbResult.Text = "写数据失败！";
        }
        /// <summary>
        /// 对卡的值进行操作..
        /// 发现每进行一次值操作会自动增加900
        /// </summary>
        private void MingTechValueOperator()
        {
            uint cvalue = 0;
            uint val = 0;
            int i = 0;
            if (textValue.TextLength < 1)
            {
                lbResult.Text = "请输入值！";
                return;
            }

            cvalue = Convert.ToUInt32(textValue.Text, 10);
            if (cvalue < 1 || cvalue > 4294966000)
            {
                lbResult.Text = "输入的值不正确！";
                return;
            }

            currentState = MingTechMiFareOne.rf_initval(icDev, secNumber * 4 + 2, cvalue);
            if (currentState != 0)
            {
                lbResult.Text = "初始化值操作失败！";
                return;
            }
            currentState = MingTechMiFareOne.rf_increment(icDev, secNumber * 4 + 2, 1000);
            if (currentState != 0)
            {
                lbResult.Text = "加值操作失败！";
                return;
            }
            currentState = MingTechMiFareOne.rf_decrement(icDev, secNumber * 4 + 2, 100);
            if (currentState != 0)
            {
                lbResult.Text = "减值操作失败！";
                return;
            }
            currentState = MingTechMiFareOne.rf_readval(icDev, secNumber * 4 + 2, out val);
            if (currentState != 0)
            {
                lbResult.Text = "读当前值操作失败！";
                return;
            }
            textValue.Text = val.ToString();
            lbResult.Text = "值操作成功！";
        }

        private void InizationData()
        {
            textSec.Text = Sec;
            textKey.Text = Key;
            textData.Text = Data;
            textValue.Text = Value;
        }

        private bool InvaildReadAndWrite(string readStr, string WriteStr)
        {
            if (readStr.Equals(WriteStr))
                return true;
            else
                return false;
        }

        private string InVaildCardInfo()
        {
            //根据卡内的数据和卡的序列号去判断是此卡已被写入..
            var data = objPatient.GetCardInfoByCardInfo(this.lbSnr.Text.Trim(), this.textData.Text.Trim());
            if (data != null && data.Rows.Count > 0)
            {
                return data[0].STATE;
            }
            else
                return "-1";
        }
        private void SaveCardInfo()
        { }
        #endregion

        #region 事件

        private void EditPatientCard_Load(object sender, EventArgs e)
        {
            //去打开串口
            MingTechConnect();
            //去寻卡
            MingTechSeekCard();
            //去验证
            MingTechAuth();
            //赋值透析组成的密码
            Data = string.Format("{0}{1}",HemoId,Data.Substring(HemoId.Length)).ToUpper();
            this.textData.Text = Data;
        }

        private void btnSeekCard_Click(object sender, EventArgs e)
        {

            MingTechSeekCard();
            InizationData();
            MingTechAuth();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            InizationData();
            MingTechAuth();
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            MingTechReadData();
        }

        private void btnWriteData_Click(object sender, EventArgs e)
        {
            MingTechReadData();
            if (InVaildCardInfo()=="0")
            {
                AutoClosedMsgBox.ShowForm("此卡已关联患者,请更换新卡!",this.Text,3000,MessageBoxIcon.Error);
                this.btnSeekCard.Visible = true;
                //this.btnAuth.Visible = true;
                this.btnWriteData.Enabled = false;
                return;
            }
            else if (InVaildCardInfo() == "1")
            {
                AutoClosedMsgBox.ShowForm("此卡已被挂失\r\n请使用未写入过的卡进行制卡", this.Text, 3000, MessageBoxIcon.Error);
                this.btnSeekCard.Visible = true;
                //this.btnAuth.Visible = true;
                this.btnWriteData.Enabled = false;
                return;
            }
            else if (InVaildCardInfo() == "2")
            {
                if (XtraMessageBox.Show("此卡已作废，是否继续进行制卡？","提示",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    this.btnSeekCard.Visible = true;
                    //this.btnAuth.Visible = true;
                    this.btnWriteData.Enabled = false;
                    return;
                }
            }

            this.textData.Text = Data;
            //写
            MingTechWriteData();
            //读
            MingTechReadData();
            if (InvaildReadAndWrite(this.textData.Text.Trim(),Data.Trim()))
            {
                MingTechDisConnect();
                _patientCard = new DrugModel.MED_PATIENTS_CARDDataTable();
                var row = _patientCard.NewMED_PATIENTS_CARDRow();
                row.ID = Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = this.HemoId;
                row.SERIALNUMBER =this.lbSnr.Text.Trim();// Convert.ToInt32(, 16).ToString();
                row.SEC = this.textSec.Text.Trim();
                row.CARDKEY = this.textKey.Text.Trim();
                row.CARDNO = this.textData.Text.Trim();
                row.CARDVALUE = this.textValue.Text.Trim();
                row.STATE = "0";
                row.EXTEND1 = string.Empty;
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = System.DateTime.Now;
                _patientCard.AddMED_PATIENTS_CARDRow(row);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                AutoClosedMsgBox.ShowForm("写入失败", this.Text, 1000, MessageBoxIcon.Error);
                MingTechDisConnect();
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void btnValueOp_Click(object sender, EventArgs e)
        {
            MingTechValueOperator();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MingTechDisConnect();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void EditPatientCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            MingTechDisConnect();
        }

        #endregion
    }
}
