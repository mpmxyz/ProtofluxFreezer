using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProtofluxFreezer
{
    [TestClass]
    public class UninitializedModTest
    {
        [TestMethod]
        public void TestModHasNonEmptyName()
        {
            var mod = ProtofluxFreezerMonkey.Instance;
            Assert.IsNotNull(mod.Name);
            Assert.AreNotEqual("", mod.Name);
        }
    }
}
