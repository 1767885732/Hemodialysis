using Hemo.IService.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hemo.Model;
using System.Data;

namespace HemoTestProject
{
    
    
    /// <summary>
    ///这是 IMachineTest 的测试类，旨在
    ///包含所有 IMachineTest 单元测试
    ///</summary>
    [TestClass()]
    public class IMachineTest
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


        internal virtual IMachine CreateIMachine()
        {
            // TODO: 实例化相应的具体类。
            IMachine target = null;
            return target;
        }

        /// <summary>
        ///删除设备 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteAirPurgeDataTest()
        {
            IMachine target = CreateIMachine(); // TODO: 初始化为适当的值
            string arrPurgeId = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual =0;
           // actual = target.DeleteAirPurgeData(arrPurgeId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///删除水处理机 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteMED_EQUIPMENT_MAINTENANCETest()
        {
            IMachine target = CreateIMachine(); // TODO: 初始化为适当的值
            string id = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
           // actual = target.DeleteMED_EQUIPMENT_MAINTENANCE(id);
            Assert.AreEqual(expected, actual);

        }

      
    }
}
