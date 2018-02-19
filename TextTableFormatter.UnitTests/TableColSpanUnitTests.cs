namespace TextTableFormatter.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class TableColSpanUnitTests
    {
        [Test]
        [Category("TableColSpanTests")]
        public void TestWideNullCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(5);
            table.AddCell(null, cellStyle, 5);

            Assert.AreEqual(""
                            + "+----+" + Environment.NewLine
                            + "|    |" + Environment.NewLine
                            + "+----+", table.Render());
        }

        [Test]
        [Category("TableColSpanTests")]
        public void TestWideIncompleteNullCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(7);
            table.AddCell(null, cellStyle, 3);

            Assert.AreEqual(""
                            + "+--+++++" + Environment.NewLine
                            + "|  |||||" + Environment.NewLine
                            + "+--+++++", table.Render());
        }

        [Test]
        [Category("TableColSpanTests")]
        public void TestSetColSpan()
        {
            var cs = new CellStyle();
            var csr = new CellStyle(CellTextAlignment.Right);
            var csc = new CellStyle(CellTextAlignment.Center);
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(new ColumnStyle(6, 10))
                .AddColumn(new ColumnStyle(2, 7));

            table.AddCell("abcd", cs);
            table.AddCell("123456", cs);
            table.AddCell("mno", cs, 2);
            table.AddCell("xyztu", csr, 2);
            table.AddCell("efgh", csc, 2);

            Assert.AreEqual(""
                            + "+------+------+" + Environment.NewLine
                            + "|abcd  |123456|" + Environment.NewLine
                            + "+-------------+" + Environment.NewLine
                            + "|mno          |" + Environment.NewLine
                            + "+-------------+" + Environment.NewLine
                            + "|        xyztu|" + Environment.NewLine
                            + "+-------------+" + Environment.NewLine
                            + "|    efgh     |" + Environment.NewLine
                            + "+-------------+", table.Render());
        }

        [Test]
        [Category("TableColSpanTests")]
        public void TestCenteredColSpan()
        {
            var cellStyle1 = new CellStyle();

            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL)
                .AddColumn(new ColumnStyle(3, 10))
                .AddColumn(new ColumnStyle(2, 7))
                .AddColumn(new ColumnStyle(3, 10))
                .AddColumn();

            table.AddCell("abcd", cellStyle1);
            table.AddCell("123456", cellStyle1);
            table.AddCell("efgh", cellStyle1);
            table.AddCell("789012", cellStyle1);

            table.AddCell("ijkl", cellStyle1);
            table.AddCell("mno", cellStyle1, 2);
            table.AddCell("345678", cellStyle1);

            table.AddCell("mnop", cellStyle1);
            table.AddCell("901234", cellStyle1);
            table.AddCell("qrst", cellStyle1);
            table.AddCell("567890", cellStyle1);

            Assert.AreEqual(""
                            + "+----+------+----+------+" + Environment.NewLine
                            + "|abcd|123456|efgh|789012|" + Environment.NewLine
                            + "+----+-----------+------+" + Environment.NewLine
                            + "|ijkl|mno        |345678|" + Environment.NewLine
                            + "+----+-----------+------+" + Environment.NewLine
                            + "|mnop|901234|qrst|567890|" + Environment.NewLine
                            + "+----+------+----+------+", table.Render());
        }

        [Test]
        [Category("TableColSpanTests")]
        public void TestSetColSpanWide()
        {
            var cellStyle1 = new CellStyle();
            var cellStyle2 = new CellStyle(CellTextAlignment.Right);
            var textTable = new TextTable(TableBorderStyle.CLASSIC_WIDE, TableBorderVisibility.ALL)
                .AddColumn(new ColumnStyle(6, 10))
                .AddColumn(new ColumnStyle(2, 7));

            textTable.AddCell("abcd", cellStyle1);
            textTable.AddCell("123456", cellStyle1);
            textTable.AddCell("mno", cellStyle1, 2);
            textTable.AddCell("xyztu", cellStyle2, 2);

            Assert.AreEqual(""
                            + "+--------+--------+" + Environment.NewLine
                            + "| abcd   | 123456 |" + Environment.NewLine
                            + "+-----------------+" + Environment.NewLine
                            + "| mno             |" + Environment.NewLine
                            + "+-----------------+" + Environment.NewLine
                            + "|           xyztu |" + Environment.NewLine
                            + "+-----------------+", textTable.Render());
        }

        [Test]
        [Category("TableColSpanTests")]
        public void TestTooWideCell()
        {
            var cellStyle = new CellStyle();
            var table = new TextTable(TableBorderStyle.CLASSIC, TableBorderVisibility.ALL).AddColumns(3);
            table.AddCell("abc", cellStyle, 5);
            table.AddCell("defg", cellStyle, 5);
            Assert.AreEqual(""
                            + "+----+" + Environment.NewLine
                            + "|abc |" + Environment.NewLine
                            + "+----+" + Environment.NewLine
                            + "|defg|" + Environment.NewLine
                            + "+----+", table.Render());
        }
    }
}