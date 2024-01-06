using Microsoft.VisualStudio.TestTools.UnitTesting;
using Organize;

namespace Cleaner
{
    [TestClass]
    public class Employee
    {
        [TestMethod]
        public void Clean()
        {
            Closet closet = new();
            Assert.IsFalse(string.IsNullOrEmpty(closet.StorageBin.ToString()));
        }
    }
}
