using Hemo.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.Data;
using System.Drawing;
using Hemo.Model;
using System.Collections.Generic;

namespace HemoTestProject
{
    
    
    /// <summary>
    ///这是 UtilityTest 的测试类，旨在
    ///包含所有 UtilityTest 单元测试
    ///</summary>
    [TestClass()]
    public class UtilityTest
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

        /// <summary>
        ///CBool 的测试
        ///</summary>
        [TestMethod()]
        public void CBoolTest()
        {
            string value = "true"; // TODO: 初始化为适当的值
            bool expected = true; // TODO: 初始化为适当的值
            bool actual;
            actual = Utility.CBool(value);
           // Assert.IsNull(value, "输入为空");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CDate 的测试
        ///</summary>
        [TestMethod()]
        public void CDateTest()
        {
            string value = "2011-04-01"; // TODO: 初始化为适当的值
            int expected = 6 ; // TODO: 初始化为适当的值
            int actual;
            actual = Utility.GetAge(value);
            Assert.AreEqual(expected, actual);
        }





        /// <summary>
        ///CDouble 的测试
        ///</summary>
        [TestMethod()]
        public void CDoubleTest()
        {
            string value = "1.23"; // TODO: 初始化为适当的值
            double expected = 1.23; // TODO: 初始化为适当的值
            double actual;
            actual = Utility.CDouble(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CDecimal 的测试
        ///</summary>
        [TestMethod()]
        public void CDecimalTest()
        {
            string value = "1223.4"; // TODO: 初始化为适当的值
            Decimal expected = 1223.4M; // TODO: 初始化为适当的值
            Decimal actual;
            actual = Utility.CDecimal(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CInt 的测试
        ///</summary>
        [TestMethod()]
        public void CIntTest()
        {
            string value = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Utility.CInt(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///IsDateTime 的测试
        ///</summary>
        [TestMethod()]
        public void IsDateTimeTest()
        {
            string source = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = Utility.IsDateTime(source);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///IsMobile 的测试
        ///</summary>
        [TestMethod()]
        public void IsMobileTest()
        {
            string source = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = Utility.IsMobile(source);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///IsTel 的测试
        ///</summary>
        [TestMethod()]
        public void IsTelTest()
        {
            string source = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = Utility.IsTel(source);
            Assert.AreEqual(expected, actual);
        }
    }
}
