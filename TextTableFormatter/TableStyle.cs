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
        public int LeftMargin { get; }
        public int HeaderRows { get; }
        public int FooterRows { get; }
        public CellStyle HeaderStyle { get; }
        public CellStyle CellStyle { get; }
        public CellStyle FooterStyle { get; }

        /// <summary>
        /// Gets the table border style
        /// </summary>
        public TableBorderStyle BorderStyle { get; }

        public TableBorderVisibility BorderVisibility { get; }

        internal TableStyle(
            TableBorderStyle borderStyle = null, 
            TableBorderVisibility borderVisibility = null, 
            int leftMargin = 0, 
            int headerRows = 0, 
            int footerRows = 0,
            CellStyle cellStyle = null,
            CellStyle headerStyle = null, 
            CellStyle footerStyle = null)
        {
            this.BorderStyle = borderStyle ?? TableBorderStyle.CLASSIC;
            this.BorderVisibility = borderVisibility ?? TableBorderVisibility.SURROUND_HEADER_AND_COLUMNS;
            this.LeftMargin = Math.Max(leftMargin, 0);
            this.HeaderRows = Math.Max(headerRows, 1);
            this.FooterRows = Math.Max(footerRows, 1);
            this.CellStyle = cellStyle;
            this.HeaderStyle = headerStyle;
            this.FooterStyle = footerStyle;
        }

        internal IEnumerable<string> RenderLines(TextTable table)
        {
            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                foreach (var line in RenderRow(table, rowIndex))
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
                RenderRow(sb, table, rowIndex);
            }
            return sb.ToString();
        }

        private IEnumerable<string> RenderRow(TextTable table, int rowIndex)
        {
            var sb = new StringBuilder(80).Append(' ', this.LeftMargin);

            // Render border above row
            if (rowIndex == 0)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Length = this.LeftMargin;
                    this.BorderVisibility.RenderTopBorder(sb, table, this.BorderStyle, rowIndex);
                    yield return sb.ToString();
                }
            }
            else if (IsMiddleBorderVisibleAbove(rowIndex, table.Rows.Count))
            {
                sb.Length = this.LeftMargin;
                this.BorderVisibility.RenderMiddleSeparator(sb, table, this.BorderStyle, rowIndex);
                yield return sb.ToString();
            }

            // Render row content
            var row = table.Rows[rowIndex];
            var rowType = GetRowType(rowIndex, table.Rows.Count);
            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                sb.Length = this.LeftMargin;
                RenderRowContent(sb, row, rowType, table.Columns, lineIndex);
                yield return sb.ToString();
            }

            // For last row only, render border below row
            if (rowIndex == table.Rows.Count - 1)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    sb.Length = this.LeftMargin;
                    this.BorderVisibility.RenderBottomBorder(sb, table, this.BorderStyle, rowIndex);
                    yield return sb.ToString();
                }
            }
        }

        private void RenderRow(StringBuilder sb, TextTable table, int rowIndex)
        {
            var linesWritten = 0;
            if (rowIndex == 0)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Append(' ', this.LeftMargin);
                    this.BorderVisibility.RenderTopBorder(sb, table, this.BorderStyle, rowIndex);
                    linesWritten++;
                }
            }
            else if (IsMiddleBorderVisibleAbove(rowIndex, table.Rows.Count))
            {
                sb.Append(' ', this.LeftMargin);
                this.BorderVisibility.RenderMiddleSeparator(sb, table, this.BorderStyle, rowIndex);
                linesWritten++;
            }

            var row = table.Rows[rowIndex];
            var rowType = GetRowType(rowIndex, table.Rows.Count);
            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                if (linesWritten > 0) sb.AppendLine();
                sb.Append(' ', this.LeftMargin);
                RenderRowContent(sb, row, rowType, table.Columns, lineIndex);
                linesWritten++;
            }

            if (rowIndex == table.Rows.Count - 1)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    if (linesWritten > 0) sb.AppendLine();
                    sb.Append(' ', this.LeftMargin);
                    this.BorderVisibility.RenderBottomBorder(sb, table, this.BorderStyle, rowIndex);
                }
            }
        }

        private void RenderRowContent(StringBuilder sb, Row row, RowType rowType, IList<Column> columns, int lineIndex)
        {
            // Left border
            if (this.BorderVisibility.IsLeftBorderVisible) sb.Append(BorderStyle.Left);

            // Cells
            var columnCount = columns.Count;
            var columnIndex = 0;
            foreach (var cell in row.Cells)
            {
                // cell separator
                if (IsCenterBorderVisibleOnLeft(columnIndex, columnCount)) sb.Append(BorderStyle.Center);

                // Cell content
                var sepWidth = BorderStyle.Center.Length;
                var width = -sepWidth;
                for (var pos = columnIndex; pos < columnIndex + cell.ColumnSpan; pos++)
                {
                    width = width + sepWidth + columns[pos].ActualWidth;
                }

                var contentLine = cell[lineIndex];
                cell.ActualStyle.Render(sb, contentLine, width);

                columnIndex = columnIndex + cell.ColumnSpan;
            }

            // Render missing cells
            for (; columnIndex < columnCount; columnIndex++)
            {
                // cell separator
                if (IsCenterBorderVisibleOnLeft(columnIndex, columnCount)) sb.Append(BorderStyle.Center);

                // Cell content
                sb.Append(' ', columns[columnIndex].ActualWidth);
            }

            // Right border
            if (this.BorderVisibility.IsRightBorderVisible) sb.Append(BorderStyle.Right);
        }

        private bool IsMiddleBorderVisibleAbove(int rowIndex, int rowCount)
        {
            if (rowIndex <= 0 || rowIndex >= rowCount) return false;
            if (this.HeaderRows > 0 && rowIndex <= this.HeaderRows) return this.BorderVisibility.IsHeaderSeparatorVisible;
            if (this.FooterRows > 0 && rowIndex >= rowCount - this.FooterRows) return this.BorderVisibility.IsFooterSeparatorVisible;
            return this.BorderVisibility.IsMiddleSeparatorVisible;
        }

        private bool IsCenterBorderVisibleOnLeft(int columnIndex, int columnCount)
        {
            return columnIndex > 0 
                && columnIndex < columnCount 
                && this.BorderVisibility.IsCenterSeparatorVisible;
        }

        private enum RowType
        {
            None,
            Header,
            Data,
            Footer
        }

        internal CellStyle GetInheritedStyle(ColumnStyle columnStyle, int rowIndex, int rowCount)
        {
            return GetInheritedStyle(columnStyle, GetRowType(rowIndex, rowCount));
        }

        private RowType GetRowType(int rowIndex, int rowCount)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return RowType.None;
            if (this.HeaderRows > 0 && rowIndex < this.HeaderRows) return RowType.Header;
            if (this.FooterRows > 0 && rowIndex >= rowCount - this.FooterRows) return RowType.Footer;
            return RowType.Data;
        }

        private CellStyle GetInheritedStyle(ColumnStyle columnStyle, RowType rowType)
        {
            switch (rowType)
            {
                case RowType.Header:
                    return columnStyle?.HeaderStyle 
                        ?? this.HeaderStyle
                        ?? columnStyle?.CellStyle
                        ?? this.CellStyle 
                        ?? CellStyle.Default;
                case RowType.Footer:
                    return columnStyle?.FooterStyle
                        ?? this.FooterStyle
                        ?? columnStyle?.CellStyle
                        ?? this.CellStyle
                        ?? CellStyle.Default;
                default:
                    return columnStyle?.CellStyle
                        ?? this.CellStyle
                        ?? CellStyle.Default;
            }
        }
    }
}