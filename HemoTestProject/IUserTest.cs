using Hemo.IService.Permission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace HemoTestProject
{
    
    
    /// <summary>
    ///这是 IUserTest 的测试类，旨在
    ///包含所有 IUserTest 单元测试
    ///</summary>
    [TestClass()]
    public class IUserTest
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


        internal virtual IUser CreateIUser()
        {
            // TODO: 实例化相应的具体类。
            IUser target = ServiceManager.Instance.UserService; ;
            return target;
        }

        ///<summary>
        ///登录测试的测试
        ///</summary>
        [TestMethod()]
        public void VerifyUserLoginTest()
        {
            IUser target = CreateIUser(); // TODO: 初始化为适当的值
            string userName = string.Empty; // TODO: 初始化为适当的值
            string passWord = string.Empty; // TODO: 初始化为适当的值
            userName = "admin";
            passWord = Utility.Encrypto("medhemo");
            PermissionModel.MED_USERSDataTable expected = null; // TODO: 初始化为适当的值
            PermissionModel.MED_USERSDataTable actual = null;
           //actual = target.VerifyUserLogin(userName, passWord);
            Assert.AreEqual(expected, actual);
        }
    }
}
