using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void ReplaceAllTest1()
    {
        const string original = @"using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
#if LICENSING
        [TestMethod]
        public void ReadGetRequestDataAsyncTest()
        {
            const string original = @"""";
            const string expected = """";

            Assert.AreEqual(expected, original.ReplaceAll(string.Empty, string.Empty, string.Empty));
        }
#endif

#if LICENSING
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

        Assert.AreEqual(expected, original.ReplaceAll("#if LICENSING", "#endif", string.Empty));
    }

    [TestMethod]
    public void ReplaceAllTest2()
    {
        const string original = @"
#if LICENSING
            DaysLeftLabel = new Label
            {
                AutoSize = true,
                Text = @""(Trial Version)"",
            };
            PurchaseLinkLabel = new LinkLabel
            {
                AutoSize = true,
                Text = @""Purchase Developer License"",
#endif
#if LICENSING && !NET5_0
                Font = new Font(DaysLeftLabel.Font.FontFamily, 6),
#endif
#if LICENSING
            };
            PurchaseLinkLabel.LinkClicked += linkLblPurchase_LinkClicked;

            WelcomeBoardPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Size = new Size(0, 30),
                FlowDirection = FlowDirection.LeftToRight,
                Controls =
                {
                    PurchaseLinkLabel,
                    DaysLeftLabel,
                },
            };

            Controls.Add(WelcomeBoardPanel);

            WelcomeBoardPanel.ResumeLayout(false);
            WelcomeBoardPanel.PerformLayout();
#endif
";
        const string expected = @"



";

        Assert.AreEqual(expected, original.ReplaceAll("#if LICENSING", "#endif", string.Empty));
    }

    [TestMethod]
    public void ReplaceAllTest3()
    {
        const string original = @"
#if LICENSING
            DaysLeftLabel = new Label
            {
                AutoSize = true,
                Text = @""(Trial Version)"",
            };
            PurchaseLinkLabel = new LinkLabel
            {
                AutoSize = true,
                Text = @""Purchase Developer License"",
#endif
#if LICENSING && !NET5_0
                Font = new Font(DaysLeftLabel.Font.FontFamily, 6),
#endif
#if LICENSING
            };
            PurchaseLinkLabel.LinkClicked += linkLblPurchase_LinkClicked;

            WelcomeBoardPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Size = new Size(0, 30),
                FlowDirection = FlowDirection.LeftToRight,
                Controls =
                {
                    PurchaseLinkLabel,
                    DaysLeftLabel,
                },
            };

            Controls.Add(WelcomeBoardPanel);

            WelcomeBoardPanel.ResumeLayout(false);
            WelcomeBoardPanel.PerformLayout();
#endif
";
        const string expected = @"
#if LICENSING#endif
#if LICENSING#endif
#if LICENSING#endif
";

        Assert.AreEqual(expected, original.ReplaceAll("#if LICENSING", "#endif", string.Empty, false));
    }
}
