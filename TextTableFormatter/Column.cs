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
        private readonly int _minWidth;
        private readonly int _maxWidth;

        public int ActualWidth { get; private set; }

        internal Column(int columnIndex, int minWidth, int maxWidth)
        {
            _columnIndex = columnIndex;
            _minWidth = minWidth;
            _maxWidth = maxWidth;
        }

        internal void PerformLayout(IList<Column> columns, int separatorWidth)
        {
            var desiredWidth = _minWidth;
            foreach (var cell in _cells)
            {
                var cellOverlapWithPreviousColumns = 0;

                if (cell.ColumnSpan > 1)
                {
                    for (var pos = _columnIndex - cell.ColumnSpan + 1; pos < _columnIndex; pos++)
                    {
                        cellOverlapWithPreviousColumns += columns[pos].ActualWidth + separatorWidth;
                    }
                }

                cell.PerformLayout(_maxWidth);

                var cellOverlap = cell.ActualWidth - cellOverlapWithPreviousColumns;
                if (cellOverlap > desiredWidth)
                {
                    desiredWidth = cellOverlap;
                }
            }

            this.ActualWidth = desiredWidth;
        }

        internal void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }
    }
}