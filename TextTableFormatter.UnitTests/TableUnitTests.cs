namespace TextTableFormatter.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class TableUnitTests
    {
        [Test]
        [Category("TableTests")]
        public void TestEmpty()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(10);
            Assert.AreEqual("", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestOneCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("abcdef", cellStyle);

            Assert.AreEqual(""
                            + "+------+" + Environment.NewLine
                            + "|abcdef|" + Environment.NewLine
                            + "+------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestNullCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell(null, cellStyle);
            Assert.AreEqual(""
                            + "++" + Environment.NewLine
                            + "||" + Environment.NewLine
                            + "++", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestEmptyCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("", cellStyle);
            Assert.AreEqual(""
                            + "++" + Environment.NewLine
                            + "||" + Environment.NewLine
                            + "++", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestTwoCellsHorizontal()
        {
            var cellStyle = new CellStyle();
            var textTable = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(2);
            textTable.AddCell("abcdef", cellStyle);
            textTable.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "+------+------+" + Environment.NewLine
                            + "|abcdef|123456|" + Environment.NewLine
                            + "+------+------+", textTable.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestTwoCellsVertical()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumn();
            table.AddCell("abcdef", cellStyle);
            table.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "+------+" + Environment.NewLine
                            + "|abcdef|" + Environment.NewLine
                            + "+------+" + Environment.NewLine
                            + "|123456|" + Environment.NewLine
                            + "+------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestMarginSpaces()
        {
            var cellStyle = new CellStyle();
            var textTable = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL, 4).AddColumn();
            textTable.AddCell("abcdef", cellStyle);
            textTable.AddCell("123456", cellStyle);

            Assert.AreEqual(""
                            + "    +------+" + Environment.NewLine
                            + "    |abcdef|" + Environment.NewLine
                            + "    +------+" + Environment.NewLine
                            + "    |123456|" + Environment.NewLine
                            + "    +------+", textTable.Render());
        }

        [Test]
        [Category("TableTests")]
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
                            + "+-------+--------+" + Environment.NewLine
                            + "|abcdef |123456  |" + Environment.NewLine
                            + "+-------+--------+" + Environment.NewLine
                            + "|mno    |45689   |" + Environment.NewLine
                            + "+-------+--------+" + Environment.NewLine
                            + "|xyztuvw|01234567|" + Environment.NewLine
                            + "+-------+--------+", textTable.Render());
        }

        [Test]
        [Category("TableTests")]
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
                            + "+------+-------+" + Environment.NewLine
                            + "|abcd  |123456 |" + Environment.NewLine
                            + "+------+-------+" + Environment.NewLine
                            + "|mno   |45689  |" + Environment.NewLine
                            + "+------+-------+" + Environment.NewLine
                            + "|xyztu |0123456|" + Environment.NewLine
                            + "+------+-------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
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
                            + "+------+------+" + Environment.NewLine
                            + "|abcd  |123456|" + Environment.NewLine
                            + "+------+------+" + Environment.NewLine
                            + "|mno   |45689 |" + Environment.NewLine
                            + "+------+------+" + Environment.NewLine
                            + "|xyztu |      |" + Environment.NewLine
                            + "+------+------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
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
                            + "+------+------+------+" + Environment.NewLine
                            + "|Cell  | Cell |  Cell|" + Environment.NewLine
                            + "|One   | Two  | Three|" + Environment.NewLine
                            + "+------+------+------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestCellWithTextWrapping()
        {
            var csl = new CellStyle(textWrapping: CellTextWrapping.Wrap);
            var csc = new CellStyle(CellTextAlignment.Center, textWrapping: CellTextWrapping.Wrap);
            var csr = new CellStyle(CellTextAlignment.Right, textWrapping: CellTextWrapping.Wrap);
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(6, 6)
                .AddColumn(6, 6)
                .AddColumn(6, 6);

            table.AddCell("Cell One", csl);
            table.AddCell("Cell.Two", csc);
            table.AddCell("Cell-Three", csr);

            Assert.AreEqual(""
                            + "+------+------+------+" + Environment.NewLine
                            + "|Cell  |Cell. | Cell-|" + Environment.NewLine
                            + "|One   | Two  | Three|" + Environment.NewLine
                            + "+------+------+------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestSingleRowHeaderAndFooterSeparators()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC,
                    TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS)
                .AddColumn()
                .AddColumn();
            table.AddCell("Title 1");
            table.AddCell("Title 2");
            table.AddCell("Data 1");
            table.AddCell("Data 2");
            table.AddCell("Data 3");
            table.AddCell("Data 4");
            table.AddCell("Footer 1");
            table.AddCell("Footer 2");

            Assert.AreEqual(""
                            + "+--------+--------+" + Environment.NewLine
                            + "|Title 1 |Title 2 |" + Environment.NewLine
                            + "+--------+--------+" + Environment.NewLine
                            + "|Data 1  |Data 2  |" + Environment.NewLine
                            + "|Data 3  |Data 4  |" + Environment.NewLine
                            + "+--------+--------+" + Environment.NewLine
                            + "|Footer 1|Footer 2|" + Environment.NewLine
                            + "+--------+--------+", table.Render());
        }

        [Test]
        [Category("TableTests")]
        public void TestMultipleRowHeaderAndFooterSeparators()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC,
                    TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS, headerRows: 2, footerRows: 2)
                .AddColumn()
                .AddColumn();
            table.AddCell("Headers", columnSpan: 2);
            table.AddCell("Title 1");
            table.AddCell("Title 2");
            table.AddCell("Data 1");
            table.AddCell("Data 2");
            table.AddCell("Data 3");
            table.AddCell("Data 4");
            table.AddCell("Footers", columnSpan: 2);
            table.AddCell("Footer 1");
            table.AddCell("Footer 2");

            Assert.AreEqual(""
                            + "+-----------------+" + Environment.NewLine
                            + "|Headers          |" + Environment.NewLine
                            + "+-----------------+" + Environment.NewLine
                            + "|Title 1 |Title 2 |" + Environment.NewLine
                            + "+--------+--------+" + Environment.NewLine
                            + "|Data 1  |Data 2  |" + Environment.NewLine
                            + "|Data 3  |Data 4  |" + Environment.NewLine
                            + "+-----------------+" + Environment.NewLine
                            + "|Footers          |" + Environment.NewLine
                            + "+-----------------+" + Environment.NewLine
                            + "|Footer 1|Footer 2|" + Environment.NewLine
                            + "+--------+--------+", table.Render());
        }
    }
}