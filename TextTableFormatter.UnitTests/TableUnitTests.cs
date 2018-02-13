namespace TextTableFormatter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TableUnitTests
    {
        [TestMethod]
        [TestCategory("TableTests")]
        public void TestEmpty()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(10);
            Assert.AreEqual("", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestOneCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("abcdef", cellStyle);

            Assert.AreEqual(""
                            + "+------+\n"
                            + "|abcdef|\n"
                            + "+------+", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestNullCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell(null, cellStyle);
            Assert.AreEqual(""
                            + "++\n"
                            + "||\n"
                            + "++", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestEmptyCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("", cellStyle);
            Assert.AreEqual(""
                            + "++\n"
                            + "||\n"
                            + "++", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestTwoCellsHorizontal()
        {
            var cellStyle = new CellStyle();
            var textTable = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(2);
            textTable.AddCell("abcdef", cellStyle);
            textTable.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "+------+------+\n"
                            + "|abcdef|123456|\n"
                            + "+------+------+", textTable.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestTwoCellsVertical()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("abcdef", cellStyle);
            table.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "+------+\n"
                            + "|abcdef|\n"
                            + "+------+\n"
                            + "|123456|\n"
                            + "+------+", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestMarginSpaces()
        {
            var cellStyle = new CellStyle();
            var textTable = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL, 4).AddColumn();
            textTable.AddCell("abcdef", cellStyle);
            textTable.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "    +------+\n"
                            + "    |abcdef|\n"
                            + "    +------+\n"
                            + "    |123456|\n"
                            + "    +------+", textTable.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestAutomaticWidth()
        {
            var cellStyle = new CellStyle();
            var textTable = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(2);
            textTable.AddCell("abcdef", cellStyle);
            textTable.AddCell("123456", cellStyle);
            textTable.AddCell("mno", cellStyle);
            textTable.AddCell("45689", cellStyle);
            textTable.AddCell("xyztuvw", cellStyle);
            textTable.AddCell("01234567", cellStyle);

            Assert.AreEqual(""
                            + "+-------+--------+\n"
                            + "|abcdef |123456  |\n"
                            + "+-------+--------+\n"
                            + "|mno    |45689   |\n"
                            + "+-------+--------+\n"
                            + "|xyztuvw|01234567|\n"
                            + "+-------+--------+", textTable.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestSetWidth()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(6, 10)
                .AddColumn(2, 7);

            table.AddCell("abcd", cellStyle);
            table.AddCell("123456", cellStyle);
            table.AddCell("mno", cellStyle);
            table.AddCell("45689", cellStyle);
            table.AddCell("xyztu", cellStyle);
            table.AddCell("01234567", cellStyle);

            Assert.AreEqual(""
                            + "+------+-------+\n"
                            + "|abcd  |123456 |\n"
                            + "+------+-------+\n"
                            + "|mno   |45689  |\n"
                            + "+------+-------+\n"
                            + "|xyztu |0123456|\n"
                            + "+------+-------+", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestMissingCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(6, 10)
                .AddColumn(2, 7);

            table.AddCell("abcd", cellStyle);
            table.AddCell("123456", cellStyle);
            table.AddCell("mno", cellStyle);
            table.AddCell("45689", cellStyle);
            table.AddCell("xyztu", cellStyle);

            Assert.AreEqual(""
                            + "+------+------+\n"
                            + "|abcd  |123456|\n"
                            + "+------+------+\n"
                            + "|mno   |45689 |\n"
                            + "+------+------+\n"
                            + "|xyztu |      |\n"
                            + "+------+------+", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestCellWithLineBreaks()
        {
            var csl = new CellStyle();
            var csc = new CellStyle(CellTextAlignment.Center);
            var csr = new CellStyle(CellTextAlignment.Right);
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(6, 6)
                .AddColumn(6, 6)
                .AddColumn(6, 6);

            table.AddCell("Cell\nOne", csl);
            table.AddCell("Cell\rTwo", csc);
            table.AddCell("Cell\r\nThree", csr);

            Assert.AreEqual(""
                            + "+------+------+------+\n"
                            + "|Cell  | Cell |  Cell|\n"
                            + "|One   | Two  | Three|\n"
                            + "+------+------+------+", table.Render());
        }

        [TestMethod]
        [TestCategory("TableTests")]
        public void TestCellWithTextWrapping()
        {
            var csl = new CellStyle(textWrapping: CellTextWrappingStyle.Wrap);
            var csc = new CellStyle(CellTextAlignment.Center, textWrapping: CellTextWrappingStyle.Wrap);
            var csr = new CellStyle(CellTextAlignment.Right, textWrapping: CellTextWrappingStyle.Wrap);
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(6, 6)
                .AddColumn(6, 6)
                .AddColumn(6, 6);

            table.AddCell("Cell One", csl);
            table.AddCell("Cell.Two", csc);
            table.AddCell("Cell-Three", csr);

            Assert.AreEqual(""
                            + "+------+------+------+\n"
                            + "|Cell  |Cell. | Cell-|\n"
                            + "|One   | Two  | Three|\n"
                            + "+------+------+------+", table.Render());
        }
    }
}