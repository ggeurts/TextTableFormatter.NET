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
    using System.Text;

    public class TableStyle
    {
        private readonly int _leftMargin;
        private readonly int _headerRows;
        private readonly int _footerRows;

        /// <summary>
        /// Gets the table border style
        /// </summary>
        public TableBorderStyle BorderStyle { get; }

        public TableBorderVisibility BorderVisibility { get; }

        internal TableStyle(TableBorderStyle borderStyle = null, TableBorderVisibility borderVisibility = null, int leftMargin = 0, int headerRows = 0, int footerRows = 0)
        {
            this.BorderStyle = borderStyle ?? TableBorderStyle.CLASSIC;
            this.BorderVisibility = borderVisibility ?? TableBorderVisibility.SURROUND_HEADER_AND_COLUMNS;
            _leftMargin = Math.Max(leftMargin, 0);
            _headerRows = Math.Max(headerRows, 1);
            _footerRows = Math.Max(footerRows, 1);
        }

        internal IEnumerable<string> RenderLines(TextTable table)
        {
            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                foreach (var line in RenderRow(table.Rows, rowIndex, table.Columns))
                {
                    yield return line;
                }
            }
        }

        internal string Render(TextTable table)
        {
            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                if (rowIndex > 0) sb.AppendLine();
                RenderRow(sb, table.Rows, rowIndex, table.Columns);
            }
            return sb.ToString();
        }

        private IEnumerable<string> RenderRow(IList<Row> rows, int rowIndex, IList<Column> columns)
        {
            var row = rows[rowIndex];
            var sb = new StringBuilder(80).Append(' ', _leftMargin);

            // Render border above row
            if (rowIndex == 0)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Length = _leftMargin;
                    this.BorderVisibility.RenderTopBorder(sb, columns, BorderStyle, row);
                    yield return sb.ToString();
                }
            }
            else if (IsMiddleBorderVisibleAbove(rowIndex, rows.Count))
            {
                sb.Length = _leftMargin;
                this.BorderVisibility.RenderMiddleSeparator(sb, columns, BorderStyle, rows[rowIndex - 1], row);
                yield return sb.ToString();
            }

            // Render row content
            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                sb.Length = _leftMargin;
                RenderRowContent(sb, columns, row, lineIndex);
                yield return sb.ToString();
            }

            // For last row only, render border below row
            if (rowIndex == rows.Count - 1)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    sb.Length = _leftMargin;
                    this.BorderVisibility.RenderBottomBorder(sb, columns, BorderStyle, row);
                    yield return sb.ToString();
                }
            }
        }

        private void RenderRow(StringBuilder sb, IList<Row> rows, int rowIndex, IList<Column> columns)
        {
            var row = rows[rowIndex];

            var linesWritten = 0;
            if (rowIndex == 0)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Append(' ', _leftMargin);
                    this.BorderVisibility.RenderTopBorder(sb, columns, BorderStyle, row);
                    linesWritten++;
                }
            }
            else if (IsMiddleBorderVisibleAbove(rowIndex, rows.Count))
            {
                sb.Append(' ', _leftMargin);
                this.BorderVisibility.RenderMiddleSeparator(sb, columns, BorderStyle, rows[rowIndex - 1], row);
                linesWritten++;
            }

            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                if (linesWritten > 0) sb.AppendLine();
                sb.Append(' ', _leftMargin);
                RenderRowContent(sb, columns, row, lineIndex);
                linesWritten++;
            }

            if (rowIndex == rows.Count - 1)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    if (linesWritten > 0) sb.AppendLine();
                    sb.Append(' ', _leftMargin);
                    this.BorderVisibility.RenderBottomBorder(sb, columns, BorderStyle, row);
                }
            }
        }

        private void RenderRowContent(StringBuilder sb, IList<Column> columns, Row row, int lineIndex)
        {
            // Left border
            if (this.BorderVisibility.IsLeftBorderVisible) sb.Append(BorderStyle.Left);

            // Cells
            var columnCount = columns.Count;
            var j = 0;
            foreach (var cell in row.Cells)
            {
                // cell separator
                if (j != 0)
                {
                    if ((j > 1 && j < columnCount - 1 && this.BorderVisibility.IsCenterSeparatorVisible)
                        || (j == 1 && this.BorderVisibility.IsLeftSeparatorVisible)
                        || (j == columnCount - 1 && this.BorderVisibility.IsRightSeparatorVisible))
                    {
                        sb.Append(BorderStyle.Center);
                    }
                }

                // Cell content
                var sepWidth = BorderStyle.Center.Length;
                var width = -sepWidth;
                for (var pos = j; pos < j + cell.ColumnSpan; pos++)
                {
                    width = width + sepWidth + columns[pos].ActualWidth;
                }

                cell.Render(sb, width, lineIndex);
                j = j + cell.ColumnSpan;
            }

            // Render missing cells
            for (; j < columnCount; j++)
            {
                // cell separator
                if (j != 0)
                {
                    if ((j > 1 && j < columnCount - 1 && this.BorderVisibility.IsCenterSeparatorVisible)
                        || (j == 1 && this.BorderVisibility.IsLeftSeparatorVisible)
                        || (j == columnCount - 1 && this.BorderVisibility.IsRightSeparatorVisible))
                    {
                        sb.Append(BorderStyle.Center);
                    }
                }

                // Cell content
                sb.Append(' ', columns[j].ActualWidth);
            }

            // Right border
            if (this.BorderVisibility.IsRightBorderVisible) sb.Append(BorderStyle.Right);
        }

        private bool IsMiddleBorderVisibleAbove(int rowIndex, int rowCount)
        {
            if (rowIndex <= 0 || rowIndex >= rowCount) return false;
            if (_headerRows > 0 && rowIndex <= _headerRows) return this.BorderVisibility.IsHeaderSeparatorVisible;
            if (_footerRows > 0 && rowIndex >= rowCount - _footerRows) return this.BorderVisibility.IsFooterSeparatorVisible;
            return this.BorderVisibility.IsMiddleSeparatorVisible;
        }
    }
}