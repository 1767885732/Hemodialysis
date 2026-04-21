using Hemo.IService.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using Hemo.Model;

namespace HemoTestProject
{
    
    
    /// <summary>
    ///这是 IHemodialysisTest 的测试类，旨在
    ///包含所有 IHemodialysisTest 单元测试
    ///</summary>
    [TestClass()]
    public class IHemodialysisTest
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


        internal virtual IHemodialysis CreateIHemodialysis()
        {
            // TODO: 实例化相应的具体类。
            IHemodialysis target = null;
            return target;
        }

        /// <summary>
        ///创建处方 的测试
        ///</summary>
        [TestMethod()]
        public void CreatePatientRecipeBydateTest()
        {
            IHemodialysis target = CreateIHemodialysis(); // TODO: 初始化为适当的值
            DateTime recipeDate = new DateTime(); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
            //actual = target.CreatePatientRecipeBydate(recipeDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///删除处方 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteAssessmentByIdTest()
        {
            IHemodialysis target = CreateIHemodialysis(); // TODO: 初始化为适当的值
            string id = string.Empty; // TODO: 初始化为适当的值
            string p = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
           // actual = target.DeleteAssessmentById(id, p);
            Assert.AreEqual(expected, actual);
   
        }

        /// <summary>
        ///创建长期处方 的测试
        ///</summary>
        [TestMethod()]
        public void DeleteCureORLongDrugByIDTest()
        {
            IHemodialysis target = CreateIHemodialysis(); // TODO: 初始化为适当的值
            string drugType = string.Empty; // TODO: 初始化为适当的值
            string cureID = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual=0;
           // actual = target.DeleteCureORLongDrugByID(drugType, cureID);
            Assert.AreEqual(expected, actual);
 
        }

        
    }
}
