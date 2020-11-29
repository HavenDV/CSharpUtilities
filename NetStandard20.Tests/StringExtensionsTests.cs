using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ReplaceAllTest()
        {
            const string original = @"using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
#if LICENCING
        [TestMethod]
        public void ReadGetRequestDataAsyncTest()
        {
            const string original = @"""";
            const string expected = """";

            Assert.AreEqual(expected, original.ReplaceAll(string.Empty, string.Empty, string.Empty));
        }
#endif

#if LICENCING
        [TestMethod]
        public void ReadGetRequestDataAsyncTest()
        {
            const string original = @"""";
            const string expected = """";

            Assert.AreEqual(expected, original.ReplaceAll(string.Empty, string.Empty, string.Empty));
        }
#endif
    }
}
";
            const string expected = @"using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {



    }
}
";

            Assert.AreEqual(expected, original.ReplaceAll("#if LICENCING", "#endif", string.Empty));
        }
    }
}