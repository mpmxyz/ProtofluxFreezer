using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProtofluxFreezer
{
    [TestClass]
    public class UninitializedModTest
    {
        [TestMethod]
        public void TestModHasNonEmptyName()
        {
            var mod = new ProtofluxFreezerRML();
            Assert.IsNotNull(mod.Name);
            Assert.AreNotEqual("", mod.Name);
        }

        [TestMethod]
        public void TestModHasNonEmptyAuthor()
        {
            var mod = new ProtofluxFreezerRML();
            Assert.IsNotNull(mod.Author);
            Assert.AreNotEqual("", mod.Author);
        }

        [TestMethod]
        public void TestModHasNonEmptyLink()
        {
            var mod = new ProtofluxFreezerRML();
            Assert.IsNotNull(mod.Link);
            Assert.AreNotEqual("", mod.Link);
        }

        [TestMethod]
        public void TestModHasNonEmptyVersion()
        {
            var mod = new ProtofluxFreezerRML();
            Assert.IsNotNull(mod.Version);
            Assert.AreNotEqual("", mod.Version);
        }

        /// <summary>
        /// This test is only run when creating a release in github.
        /// Outside of that it just reports as being "inconclusive" and can be ignored.
        /// Remove the Assert.Inconclusive part if you want it to report as "passed" instead.
        /// </summary>
        [TestMethod]
        public void TestVersionMatchesReleaseBuild()
        {
            var mod = new ProtofluxFreezerRML();
            var expected = System.Environment.GetEnvironmentVariable("RELEASE_VERSION");
            if (expected != null && expected.Length != 0)
            {
                Assert.AreEqual(expected, mod.Version);
            }
            else
            {
                Assert.Inconclusive("Checking the version number depends on a set RELEASE_VERSION variable during deployment build.");
            }
        }
    }
}
