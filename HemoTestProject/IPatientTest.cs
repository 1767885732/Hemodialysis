using Hemo.IService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hemo.Model;
using System.Data;

namespace HemoTestProject
{
    
    
    /// <summary>
    ///这是 IPatientTest 的测试类，旨在
    ///包含所有 IPatientTest 单元测试
    ///</summary>
    [TestClass()]
    public class IPatientTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual IPatient CreateIPatient()
        {
            // TODO: 实例化相应的具体类。
            IPatient target = null;
            return target;
        }

        /// <summary>
        ///取消病例记录 的测试
        ///</summary>
        [TestMethod()]
        public void CancelMaterialRecordOutTest()
        {
            IPatient target = CreateIPatient(); // TODO: 初始化为适当的值
            MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
            //actual = target.CancelMaterialRecordOut(dt);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///删除工作记录 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteChangeWorkByIdTest()
        {
            IPatient target = CreateIPatient(); // TODO: 初始化为适当的值
            string _changeId = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
           // actual = target.DeleteChangeWorkById(_changeId);
            Assert.AreEqual(expected, actual);
        }

      
    }
}
