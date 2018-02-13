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

    internal class Row
    {
        public IList<Cell> Cells { get; } = new List<Cell>();

        /// <summary>
        /// Gets number of columns that are spanned by this row
        /// </summary>
        public int ColumnSpan
        {
            get
            {
                var span = 0;
                foreach (var cell in this.Cells)
                {
                    span += cell.ColumnSpan;
                }
                return span;
            }
        }

        /// <summary>
        /// Gets the maximum number of content lines in cells that belong to this row.
        /// </summary>
        public int LineCount
        {
            get
            {
                var result = 0;
                foreach (var cell in this.Cells)
                {
                    if (cell.LineCount > result) result = cell.LineCount;
                }
                return result;
            }
        }

        internal bool HasCellSeparatorInPosition(int position)
        {
            if (position == 0) return true;
            var i = 0;
            foreach (var cell in Cells)
            {
                if (i >= position) return true;
                if (i + cell.ColumnSpan > position) return false;
                i += cell.ColumnSpan;
            }
            return true;
        }
    }
}