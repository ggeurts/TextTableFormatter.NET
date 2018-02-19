#region License and attribution
/*
 * License: Apache License Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 *
 * This code is a fork by Gerke Geurts (ggeurts) of TextTableFormatter.NET 1.0.1, a port by
 * Dario Santarelli (dsantarelli) of the Java TextTableFormatter library.
 *
 * Compared to TextTableFormatter 1.0.1, the API has been changed significantly: various style
 * types have been named differently, columns and column widths are configured differently,
 * XML escaping and ANSI escape sequences are no longer supported. This library supports
 * cell content with line breaks as well as wrapping of cell content that is longer than
 * the column width.
 */
#endregion

namespace TextTableFormatter
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a plain text table.
    /// </summary>
    public class TextTable
    {
        public TableStyle Style { get; }

        internal IList<Row> Rows { get; private set; } = new List<Row>();
        internal IList<Column> Columns { get; private set; } = new List<Column>();

        /// <summary>
        /// Gets the cell corresponding to the given row index and column index
        /// </summary>
        /// <param name="rowIndex">The table row index</param>
        /// <param name="columnIndex">The table column index</param>
        /// <returns></returns>
        internal Cell this[int rowIndex, int columnIndex]
        {
            get { return Rows[rowIndex].Cells[columnIndex]; }
        }

        /// <summary>
        /// Initializes a new instance of TextTable class
        /// </summary>
        /// <param name="borderStyle">The table border style</param>
        /// <param name="borderVisibility">The table visible borders</param>
        /// <param name="leftMargin">The table left margin</param>
        /// <param name="headerRows">The number of top rows that are to be styled as table headers.</param>
        /// <param name="footerRows">The number of bottom rows that are to be styled as table footers.</param>
        /// <param name="cellStyle">The default style for cells that are not contained by header or footer rows.</param>
        /// <param name="headerStyle">The default style for cells that are contained by header rows.</param>
        /// <param name="footerStyle">The default style for cells that are contained by footer rows.</param>
        public TextTable(
            TableBorderStyle borderStyle = null, 
            TableBorderVisibility borderVisibility = null, 
            int leftMargin = 0, 
            int headerRows = 0, 
            int footerRows = 0,
            CellStyle cellStyle = null,
            CellStyle headerStyle = null,
            CellStyle footerStyle = null)
        {
            this.Style = new TableStyle(borderStyle, borderVisibility, leftMargin, headerRows, footerRows, cellStyle, headerStyle, footerStyle);
        }

        public TextTable AddColumn(ColumnStyle style = null)
        {
            this.Columns.Add(new Column(this.Columns.Count, style));
            return this;
        }

        public TextTable AddColumns(int columnCount, ColumnStyle style = null)
        {
            for (var i = 0; i < columnCount; i++) AddColumn(style);
            return this;
        }

        /// <summary>
        /// Adds a cell with a given content, a style and a column span.
        /// The cell will be arranged in a new row if necessary
        /// </summary>
        /// <param name="content">The cell content.</param>
        /// <param name="style">Optional cell style. A <c>null</c> value indicates that style 
        /// is inherited from column and or table.</param>
        /// <param name="columnSpan">Optional cell column span, defaults to <c>1</c>.</param>
        public TextTable AddCell(object content = null, CellStyle style = null, int columnSpan = 1)
        {
            if (columnSpan < 1) throw new ArgumentOutOfRangeException(nameof(columnSpan), "Column span must be greater than or equal to zero.");

            var rowCount = this.Rows.Count;
            var currentRow = rowCount == 0 
                ? null
                : this.Rows[rowCount - 1];

            var columnCount = this.Columns.Count;
            if (currentRow == null || currentRow.ColumnSpan >= columnCount)
            {
                currentRow = new Row();
                this.Rows.Add(currentRow);
            }

            columnSpan = Math.Min(Math.Max(1, columnSpan), columnCount - currentRow.ColumnSpan);
            currentRow.Cells.Add(new Cell(content, style, columnSpan));

            return this;
        }

        public TextTable AddCells(params object[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddCell(item);
            }
            return this;
        }

        public TextTable AddCells<T>(IEnumerable<T> items, params Func<T, object>[] cellSelectors)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (cellSelectors == null) throw new ArgumentNullException(nameof(cellSelectors));

            foreach (var item in items)
            {
                foreach (var cellSelector in cellSelectors)
                {
                    AddCell(cellSelector(item));
                }
            }
            return this;
        }

        /// <summary>
        /// Renders the table as a string
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            PerformLayout();
            return this.Style.Render(this);
        }

        /// <summary>
        /// Renders the table as a string array (each array element is a row)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> RenderLines()
        {
            PerformLayout();
            return this.Style.RenderLines(this);
        }

        private void PerformLayout()
        {
            // First we connect the columns with the cells.
            foreach (var row in Rows)
            {
                var columnIndex = 0;
                foreach (var cell in row.Cells)
                {
                    var endCol = columnIndex + cell.ColumnSpan - 1;
                    if (endCol < this.Columns.Count)
                    {
                        var col = Columns[endCol];
                        col.AddCell(cell);
                        columnIndex = columnIndex + cell.ColumnSpan;
                    }
                }
            }

            // Then we calculate the appropriate column width for each one.
            foreach (var col in this.Columns)
            {
                col.PerformLayout(this, this.Style.BorderStyle.TopCenterCorner.Length);
            }
        }
    }
}