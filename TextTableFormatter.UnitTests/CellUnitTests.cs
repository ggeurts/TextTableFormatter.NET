namespace TextTableFormatter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CellUnitTests
    {
        [TestMethod]
        [TestCategory("CellTests")]
        public void TestAlignLeft()
        {
            var cellStyle = new CellStyle();
            var cell = new Cell("abcdef", cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual("a", cell.Render(1));
            Assert.AreEqual("ab", cell.Render(2));
            Assert.AreEqual("abc", cell.Render(3));
            Assert.AreEqual("abcd", cell.Render(4));
            Assert.AreEqual("abcde", cell.Render(5));
            Assert.AreEqual("abcdef", cell.Render(6));
            Assert.AreEqual("abcdef ", cell.Render(7));
            Assert.AreEqual("abcdef  ", cell.Render(8));
            Assert.AreEqual("abcdef   ", cell.Render(9));
            Assert.AreEqual("abcdef    ", cell.Render(10));
            Assert.AreEqual("abcdef     ", cell.Render(11));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestAlignCenter()
        {
            var cellStyle = new CellStyle(CellTextAlignment.Center);
            var cell = new Cell("abcdef", cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual("a", cell.Render(1));
            Assert.AreEqual("ab", cell.Render(2));
            Assert.AreEqual("abc", cell.Render(3));
            Assert.AreEqual("abcd", cell.Render(4));
            Assert.AreEqual("abcde", cell.Render(5));
            Assert.AreEqual("abcdef", cell.Render(6));
            Assert.AreEqual("abcdef ", cell.Render(7));
            Assert.AreEqual(" abcdef ", cell.Render(8));
            Assert.AreEqual(" abcdef  ", cell.Render(9));
            Assert.AreEqual("  abcdef  ", cell.Render(10));
            Assert.AreEqual("  abcdef   ", cell.Render(11));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestAlignRight()
        {
            var cellStyle = new CellStyle(CellTextAlignment.Right);
            var cell = new Cell("abcdef", cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual("a", cell.Render(1));
            Assert.AreEqual("ab", cell.Render(2));
            Assert.AreEqual("abc", cell.Render(3));
            Assert.AreEqual("abcd", cell.Render(4));
            Assert.AreEqual("abcde", cell.Render(5));
            Assert.AreEqual("abcdef", cell.Render(6));
            Assert.AreEqual(" abcdef", cell.Render(7));
            Assert.AreEqual("  abcdef", cell.Render(8));
            Assert.AreEqual("   abcdef", cell.Render(9));
            Assert.AreEqual("    abcdef", cell.Render(10));
            Assert.AreEqual("     abcdef", cell.Render(11));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestAbbreviateCrop()
        {
            var cellStyle = new CellStyle();
            var cell = new Cell("abcdef", cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual("a", cell.Render(1));
            Assert.AreEqual("ab", cell.Render(2));
            Assert.AreEqual("abc", cell.Render(3));
            Assert.AreEqual("abcd", cell.Render(4));
            Assert.AreEqual("abcde", cell.Render(5));
            Assert.AreEqual("abcdef", cell.Render(6));
            Assert.AreEqual("abcdef ", cell.Render(7));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestAbbreviateDots()
        {
            var cellStyle = new CellStyle(textTrimming: CellTextTrimmingStyle.Dots);
            var cell = new Cell("abcdef", cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual(".", cell.Render(1));
            Assert.AreEqual("..", cell.Render(2));
            Assert.AreEqual("...", cell.Render(3));
            Assert.AreEqual("a...", cell.Render(4));
            Assert.AreEqual("ab...", cell.Render(5));
            Assert.AreEqual("abcdef", cell.Render(6));
            Assert.AreEqual("abcdef ", cell.Render(7));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestNullEmpty()
        {
            var cellStyle = new CellStyle();
            var cell = new Cell(null, cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual(" ", cell.Render(1));
            Assert.AreEqual("  ", cell.Render(2));
            Assert.AreEqual("   ", cell.Render(3));
            Assert.AreEqual("    ", cell.Render(4));
            Assert.AreEqual("          ", cell.Render(10));
            Assert.AreEqual("           ", cell.Render(11));
        }

        [TestMethod]
        [TestCategory("CellTests")]
        public void TestNullText()
        {
            var cellStyle = new CellStyle(nullText: "<null>");
            var cell = new Cell(null, cellStyle);
            Assert.AreEqual("", cell.Render(0));
            Assert.AreEqual("<", cell.Render(1));
            Assert.AreEqual("<n", cell.Render(2));
            Assert.AreEqual("<nu", cell.Render(3));
            Assert.AreEqual("<nul", cell.Render(4));
            Assert.AreEqual("<null", cell.Render(5));
            Assert.AreEqual("<null>", cell.Render(6));
            Assert.AreEqual("<null> ", cell.Render(7));
            Assert.AreEqual("<null>     ", cell.Render(11));
        }
    }
}