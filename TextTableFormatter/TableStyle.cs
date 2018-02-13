namespace TextTableFormatter
{
    using System.Collections.Generic;
    using System.Text;

    public class TableStyle
    {
        private readonly string _prompt;

        /// <summary>
        /// Gets the table border style
        /// </summary>
        public TableBorderStyle BorderStyle { get; }

        public TableBorderVisibility BorderVisibility { get; }

        internal TableStyle(TableBorderStyle borderStyle = null, TableBorderVisibility borderVisibility = null, int leftMargin = 0)
        {
            this.BorderStyle = borderStyle ?? TableBorderStyle.CLASSIC;
            this.BorderVisibility = borderVisibility ?? TableBorderVisibility.SURROUND_HEADER_AND_COLUMNS;
            _prompt = leftMargin > 0 ? new string(' ', leftMargin) : string.Empty;
        }

        internal IEnumerable<string> RenderLines(TextTable table)
        {
            var rowCount = table.Rows.Count;
            Row previousRow = null;
            for (var i = 0; i < rowCount; i++)
            {
                var row = table.Rows[i];
                var isFirst = i == 0;
                var isSecond = i == 1;
                var isIntermediate = (i > 1 && i < rowCount - 1);
                var isLast = i == (rowCount - 1);
                foreach (var line in RenderRow(row, previousRow, table.Columns, isFirst, isSecond, isIntermediate, isLast))
                {
                    yield return line;
                }
                previousRow = row;
            }
        }

        internal string Render(TextTable table)
        {
            var sb = new StringBuilder();
            var rowCount = table.Rows.Count;
            Row previousRow = null;
            var firstRenderedRow = true;
            for (var i = 0; i < rowCount; i++)
            {
                var r = table.Rows[i];
                var isFirst = i == 0;
                var isSecond = i == 1;
                var isIntermediate = (i > 1 && i < rowCount - 1);
                var isLast = i == (rowCount - 1);

                if (firstRenderedRow)
                {
                    firstRenderedRow = false;
                }
                else
                {
                    sb.AppendLine();
                }
                RenderRow(sb, r, previousRow, table.Columns, isFirst, isSecond, isIntermediate, isLast);

                previousRow = r;
            }
            return sb.ToString();
        }

        private IEnumerable<string> RenderRow(Row row, Row previousRow, IList<Column> columns, bool isFirst, bool isSecond, bool isIntermediate, bool isLast)
        {
            var sb = new StringBuilder(_prompt, 80);

            if (isFirst)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Length = _prompt.Length;
                    this.BorderVisibility.RenderTopBorder(sb, columns, BorderStyle, row);
                    yield return sb.ToString();
                }
            }
            else
            {
                if (isIntermediate && this.BorderVisibility.IsMiddleSeparatorVisible 
                    || isSecond && this.BorderVisibility.IsHeaderSeparatorVisible 
                    || isLast && this.BorderVisibility.IsFooterSeparatorVisible)
                {
                    sb.Length = _prompt.Length;
                    this.BorderVisibility.RenderMiddleSeparator(sb, columns, BorderStyle, previousRow, row);
                    yield return sb.ToString();
                }
            }

            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                sb.Length = _prompt.Length;
                RenderRowContent(sb, columns, row, lineIndex);
                yield return sb.ToString();
            }

            if (isLast)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    sb.Length = _prompt.Length;
                    this.BorderVisibility.RenderBottomBorder(sb, columns, BorderStyle, row);
                    yield return sb.ToString();
                }
            }
        }

        private void RenderRow(StringBuilder sb, Row row, Row previousRow, IList<Column> columns, bool isFirst, bool isSecond, bool isIntermediate, bool isLast)
        {
            var linesWritten = 0;
            if (isFirst)
            {
                if (this.BorderVisibility.IsTopBorderVisible)
                {
                    sb.Append(_prompt);
                    this.BorderVisibility.RenderTopBorder(sb, columns, BorderStyle, row);
                    linesWritten++;
                }
            }
            else
            {
                if (isIntermediate && this.BorderVisibility.IsMiddleSeparatorVisible
                    || isSecond && this.BorderVisibility.IsHeaderSeparatorVisible
                    || isLast && this.BorderVisibility.IsFooterSeparatorVisible)
                {
                    sb.Append(_prompt);
                    this.BorderVisibility.RenderMiddleSeparator(sb, columns, BorderStyle, previousRow, row);
                    linesWritten++;
                }
            }

            for (var lineIndex = 0; lineIndex < row.LineCount; lineIndex++)
            {
                if (linesWritten > 0) sb.AppendLine();
                sb.Append(_prompt);
                RenderRowContent(sb, columns, row, lineIndex);
                linesWritten++;
            }

            if (isLast)
            {
                if (this.BorderVisibility.IsBottomBorderVisible)
                {
                    if (linesWritten > 0) sb.AppendLine();
                    sb.Append(_prompt);
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
    }
}