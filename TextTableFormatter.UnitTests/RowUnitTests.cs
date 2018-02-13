namespace TextTableFormatter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RowUnitTests
    {
        [TestMethod]
        [TestCategory("RowTests")]
        public void TestSeparators()
        {
            var cellStyle = new CellStyle();
            var row = new Row();
            row.Cells.Add(new Cell("abc", cellStyle));
            row.Cells.Add(new Cell("def", cellStyle));
            row.Cells.Add(new Cell("ghi", cellStyle, 2));
            row.Cells.Add(new Cell("jkl", cellStyle));
            row.Cells.Add(new Cell("mno", cellStyle));

            Assert.AreEqual(5, row.Cells.Count);
            Assert.AreEqual(true, row.HasCellSeparatorInPosition(0));
            Assert.AreEqual(true, row.HasCellSeparatorInPosition(1));
            Assert.AreEqual(true, row.HasCellSeparatorInPosition(2));
            Assert.AreEqual(false, row.HasCellSeparatorInPosition(3));
            Assert.AreEqual(true, row.HasCellSeparatorInPosition(4));
            Assert.AreEqual(true, row.HasCellSeparatorInPosition(5));
        }
    }
}