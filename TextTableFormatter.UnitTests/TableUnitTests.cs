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
                .AddColumn(new ColumnStyle(6, 10))
                .AddColumn(new ColumnStyle(2, 7));

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
                .AddColumn(new ColumnStyle(6, 10))
                .AddColumn(new ColumnStyle(2, 7));

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
                .AddColumns(3, new ColumnStyle(6));

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
                .AddColumns(3, new ColumnStyle(6));

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
                .AddColumns(2)
                .AddCells("Title 1", "Title 2")
                .AddCells("Data 1", "Data 2")
                .AddCells("Data 3", "Data 4")
                .AddCells("Footer 1", "Footer 2");

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
                .AddColumns(2)
                .AddCell("Headers", columnSpan: 2)
                .AddCells("Title 1", "Title 2")
                .AddCells("Data 1", "Data 2")
                .AddCells("Data 3", "Data 4")
                .AddCell("Footers", columnSpan: 2)
                .AddCells("Footer 1", "Footer 2");

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

        [Test]
        public void TestInheritedColumnStyle()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC,
                    TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS)
                .AddColumn(new ColumnStyle(10, 
                    cellStyle: new CellStyle(CellTextAlignment.Right), 
                    headerStyle: new CellStyle(CellTextAlignment.Center),
                    footerStyle: new CellStyle(CellTextAlignment.Left)))
                .AddCell("Title")
                .AddCell("Data 1")
                .AddCell("Data 2")
                .AddCell("Footer");

            Assert.AreEqual(""
                            + "+----------+" + Environment.NewLine
                            + "|  Title   |" + Environment.NewLine
                            + "+----------+" + Environment.NewLine
                            + "|    Data 1|" + Environment.NewLine
                            + "|    Data 2|" + Environment.NewLine
                            + "+----------+" + Environment.NewLine
                            + "|Footer    |" + Environment.NewLine
                            + "+----------+", table.Render());

        }

        [Test]
        public void TestInheritedTableStyle()
        {
            var table = new TextTable(TableBorderStyle.CLASSIC,
                    TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS,
                    cellStyle: new CellStyle(CellTextAlignment.Right),
                    headerStyle: new CellStyle(CellTextAlignment.Center),
                    footerStyle: new CellStyle(CellTextAlignment.Left))
                .AddColumn(new ColumnStyle(10))
                .AddCell("Title")
                .AddCell("Data 1")
                .AddCell("Data 2")
                .AddCell("Footer");

            Assert.AreEqual(""
                            + "+----------+" + Environment.NewLine
                            + "|  Title   |" + Environment.NewLine
                            + "+----------+" + Environment.NewLine
                            + "|    Data 1|" + Environment.NewLine
                            + "|    Data 2|" + Environment.NewLine
                            + "+----------+" + Environment.NewLine
                            + "|Footer    |" + Environment.NewLine
                            + "+----------+", table.Render());

        }
    }
}