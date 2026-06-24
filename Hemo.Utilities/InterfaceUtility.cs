/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:系统接口类
 * 创建标识:贺建操-2013年7月5日
 * ----------------------------------------------------------------*/
using InterFaceV4;
using wsdlLib;

namespace Hemo.Utilities {
    public class InterfaceUtility
    {

        #region 使用系统集成的接口，即使用InterFaceV4公司手麻ICU等产品使用的接口方式
        /// <summary>
        /// 根据INP_NO同步门诊病人基本信息
        /// </summary>
        /// <returns></returns>
        public static string SynchronizeOutPatientByInpNO(string pInp_no) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = pInp_no;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "HIS", "HIS108", paramIn);
            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 根据INP_NO同步住院病人基本信息
        /// </summary>
        /// <returns></returns>
        public static string SynchronizeOnePatientByInpNO(string pInp_no) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = pInp_no;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "HIS", "HIS101", paramIn);
            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }
        /// <summary>
        /// 根据ID同步单个病人基本信息
        /// </summary>
        /// <returns></returns>
        public static string SynchronizeOnePatient(string pPATIENT_ID) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = pPATIENT_ID;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS101", paramIn);
            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步所有病人住院基本信息
        /// </summary>
        /// <returns></returns>
        public static string SynchronizePatient(string pPerformedcode) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.performedcode = pPerformedcode;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "HIS", "HIS102", paramIn);
            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 根据住院号号同步病人信息
        /// </summary>
        /// <param name="wardCode"></param>
        /// <param name="inpNo"></param>
        /// <returns></returns>
        public static string SynchronizePatientByID(string wardCode, string inpNo) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.performedcode = wardCode;
            paramIn.inpno = inpNo;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS104", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步病人医嘱
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public static string SynchronizeSingleOrder(string patientID, int visitID) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = patientID;
            paramIn.visitid = visitID;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "HIS", "HIS103", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步病人检查信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public static string SynchronizeSigleCheckInfo(string patientId) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = patientId;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "PACS", "PACS001", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步病人检验信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public static string SynchronizeSigleValidation(string patientID, int visitID) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = patientID;
            paramIn.visitid = visitID;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "LIS", "LIS001", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步病历病程
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public static string SynchronizePatientDocument(string patientID, int visitID) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = patientID;
            paramIn.visitid = visitID;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "EMR", "EMR001", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }

        /// <summary>
        /// 同步病理
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public static string SynchronizePatientDocumentPIS(string patientID, int visitID) {
            ParmInputData paramIn = new ParmInputData();
            paramIn.patientid = patientID;
            paramIn.visitid = visitID;

            string error = InterFaceV4.InterFaceV4.of_systeminterface("ANESMGR", "PIS", "PIS001", paramIn);

            if (!string.IsNullOrEmpty(error))
                return string.Format("新接口（3.0）报错：{0}", error);
            else
                return string.Empty;
        }
        #endregion

        #region 使用血透系统自己的接口方式，即HemoCommService的方式
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="interFaceUrl"></param>
        /// <returns></returns>
        public static string SynchronizeDictDeptment(string interFaceUrl)
        {
            var returnStr = string.Empty;
            using (var hemoService = new HemoCommService(interFaceUrl))
            {
                //hemoService.Timeout = 60000;

                returnStr = hemoService.GetHisDeptment();
            }
            return returnStr;
        }
        /// <summary>
        /// 获取HIS用户信息
        /// </summary>
        /// <param name="interFaceUrl"></param>
        /// <returns></returns>
        public static string SynchronizeHisUsers(string interFaceUrl)
        {
            var returnStr = string.Empty;
            using (var hemoService = new HemoCommService(interFaceUrl))
            {
                //hemoService.Timeout = 60000;

                returnStr = hemoService.GetHisDataUsers();
            }
            return returnStr;
        }
        /// <summary>
        /// 获取检验信息
        /// </summary>
        /// <param name="pPatientId"></param>
        /// <param name="interFaceUrl"></param>
        /// <returns></returns>
        public static string SynchronizeLabDatasByPatiets(string pPatientId, string interFaceUrl)
        {
            var returnStr = string.Empty;
            using (var hemoService = new HemoCommService(interFaceUrl))
            {
                hemoService.Timeout = 600000;
                returnStr = hemoService.GetLabDataByPatients(pPatientId);
            }
            return returnStr; ;
        }
        /// <summary>
        /// 获取病人基本信息接口
        /// </summary>
        /// <param name="pPatientId"></param>
        /// <returns></returns>
        public static string SynchronizePatientsByPatientId(string pPatientId, string interFaceUrl)
        {
            var returnStr = string.Empty;
            using (var hemoService = new HemoCommService(interFaceUrl))
            {
                //hemoService.Timeout = 60000;

                returnStr = hemoService.GetHisPatients(pPatientId);
            }
            return returnStr; ;
        }
        /// <summary>
        /// 获取检查信息
        /// </summary>
        /// <param name="pPatientId"></param>
        /// <returns></returns>
        public static string SynchronizeExamDatasByPatientId(string pPatientId, string interFaceUrl)
        {
            var returnStr = string.Empty;
            using (var hemoService = new HemoCommService(interFaceUrl))
            {
                //hemoService.Timeout = 60000;

                returnStr = hemoService.GetExamDataByPatients(pPatientId);
            }
            return returnStr; ;
        }
        #endregion

    }
}
