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
    using System.Collections.Generic;

    internal class Column
    {
        private readonly int _columnIndex;
        private readonly IList<Cell> _cells = new List<Cell>();

        public ColumnStyle Style { get; }

        public int ActualWidth { get; private set; }

        public int MinWidth
        {
            get { return this.Style.MinWidth; }
        }

        public int MaxWidth
        {
            get { return this.Style.MaxWidth; }
        }

        internal Column(int columnIndex, ColumnStyle style)
        {
            _columnIndex = columnIndex;
            this.Style = style ?? ColumnStyle.Default;
        }

        internal void PerformLayout(TextTable table, int separatorWidth)
        {
            var desiredWidth = this.MinWidth;

            var rowCount = table.Rows.Count;
            var rowIndex = 0;
            foreach (var cell in _cells)
            {
                var cellOverlapWithPreviousColumns = 0;

                if (cell.ColumnSpan > 1)
                {
                    var firstColumnIndex = _columnIndex - cell.ColumnSpan + 1;
                    for (var i = firstColumnIndex; i < _columnIndex; i++)
                    {
                        cellOverlapWithPreviousColumns += table.Columns[i].ActualWidth + separatorWidth;
                    }
                }

                var cellStyle = cell.Style ?? table.Style.GetInheritedStyle(this.Style, rowIndex, rowCount);
                var maxColumnWidth = this.MaxWidth;
                int maxCellWidth;
                checked
                {
                    maxCellWidth = maxColumnWidth < int.MaxValue
                        ? maxColumnWidth + cellOverlapWithPreviousColumns
                        : maxColumnWidth;
                }
                cell.PerformLayout(maxCellWidth, cellStyle);

                var cellOverlap = cell.ActualWidth - cellOverlapWithPreviousColumns;
                if (cellOverlap > desiredWidth)
                {
                    desiredWidth = cellOverlap;
                }

                rowIndex++;
            }

            this.ActualWidth = desiredWidth;
        }

        internal void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }
    }
}