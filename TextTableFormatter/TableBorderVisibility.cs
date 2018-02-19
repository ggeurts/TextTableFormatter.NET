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
    using System.Text;

    /// <summary>
    /// Represents which table borders are visible
    /// </summary>
    public class TableBorderVisibility
    {
        public static readonly TableBorderVisibility NONE = new TableBorderVisibility("..........");
        public static readonly TableBorderVisibility HEADER_ONLY = new TableBorderVisibility("t...t.....");
        public static readonly TableBorderVisibility HEADER_AND_FOOTER = new TableBorderVisibility("t.t.......");
        public static readonly TableBorderVisibility HEADER_FOOTER_AND_COLUMNS = new TableBorderVisibility("t.tttt....");
        public static readonly TableBorderVisibility HEADER_AND_FIRST_COLUMN = new TableBorderVisibility("t..t......");
        public static readonly TableBorderVisibility HEADER_FIRST_AND_LAST_COLUMN = new TableBorderVisibility("t..t.t....");
        public static readonly TableBorderVisibility HEADER_FOOTER_AND_FIRST_COLUMN = new TableBorderVisibility("t.tt......");
        public static readonly TableBorderVisibility HEADER_FOOTER_FIRST_AND_LAST_COLUMN = new TableBorderVisibility("t.tt.t....");
        public static readonly TableBorderVisibility HEADER_AND_COLUMNS = new TableBorderVisibility("t..ttt....");
        public static readonly TableBorderVisibility SURROUND_HEADER_AND_COLUMNS = new TableBorderVisibility("t..ttttttt");
        public static readonly TableBorderVisibility SURROUND_HEADER_FOOTER_AND_COLUMNS = new TableBorderVisibility("t.tttttttt");
        public static readonly TableBorderVisibility SURROUND = new TableBorderVisibility("......tttt");
        public static readonly TableBorderVisibility ALL = new TableBorderVisibility("tttttttttt");

        /// <summary>
        /// Gets or sets if the table bottom border is visible
        /// </summary>
        public bool IsBottomBorderVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table center separator is visible
        /// </summary>
        public bool IsCenterSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table footer separator is visible
        /// </summary>
        public bool IsFooterSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table header separator is visible
        /// </summary>
        public bool IsHeaderSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table left border is visible
        /// </summary>
        public bool IsLeftBorderVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table left separator is visible
        /// </summary>
        public bool IsLeftSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table middle separator is visible
        /// </summary>
        public bool IsMiddleSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table right border is visible
        /// </summary>
        public bool IsRightBorderVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table right separator is visible
        /// </summary>
        public bool IsRightSeparatorVisible { get; set; }

        /// <summary>
        /// Gets or sets if the table top border is visible
        /// </summary>
        public bool IsTopBorderVisible { get; set; }

        /// <summary>
        /// Initializes a new instance of TableVisibleBorders class
        /// </summary>
        public TableBorderVisibility()
        {
        }

        private TableBorderVisibility(string separatorsAndBordersToRender)
        {
            IsHeaderSeparatorVisible = Get(separatorsAndBordersToRender, 0);
            IsMiddleSeparatorVisible = Get(separatorsAndBordersToRender, 1);
            IsFooterSeparatorVisible = Get(separatorsAndBordersToRender, 2);
            IsLeftSeparatorVisible = Get(separatorsAndBordersToRender, 3);
            IsCenterSeparatorVisible = Get(separatorsAndBordersToRender, 4);
            IsRightSeparatorVisible = Get(separatorsAndBordersToRender, 5);
            IsTopBorderVisible = Get(separatorsAndBordersToRender, 6);
            IsBottomBorderVisible = Get(separatorsAndBordersToRender, 7);
            IsLeftBorderVisible = Get(separatorsAndBordersToRender, 8);
            IsRightBorderVisible = Get(separatorsAndBordersToRender, 9);
        }

        internal void RenderTopBorder(StringBuilder sb, TextTable table, TableBorderStyle tiles, int rowIndex)
        {
            RenderHorizontalSeparator(sb, table.Columns, tiles.TopLeftCorner,
                tiles.TopCenterCorner, tiles.TopRightCorner, tiles.Top, null,
                table.Rows[rowIndex], null, tiles.TopCenterCorner, tiles.CenterWidth);
        }

        internal void RenderMiddleSeparator(StringBuilder sb, TextTable table, TableBorderStyle tiles, int rowIndex)
        {
            RenderHorizontalSeparator(sb, table.Columns, tiles.MiddleLeftCorner,
                tiles.MiddleCenterCorner, tiles.MiddleRightCorner, tiles.Middle, table.Rows[rowIndex - 1],
                table.Rows[rowIndex], tiles.UpperColumnSpan, tiles.LowerColumnSpan,
                tiles.CenterWidth);
        }

        internal void RenderBottomBorder(StringBuilder sb, TextTable table, TableBorderStyle tiles, int rowIndex)
        {
            RenderHorizontalSeparator(sb, table.Columns, tiles.BottomLeftCorner,
                tiles.BottomCenterCorner, tiles.BottomRightCorner, tiles.Bottom, table.Rows[rowIndex],
                null, tiles.BottomCenterCorner, null, tiles.CenterWidth);
        }

        private void RenderHorizontalSeparator(StringBuilder sb, IList<Column> columns, string left, string cross, string right,
            char horizontal, Row upperRow, Row lowerRow, string upperColSpan, string lowerColSpan, int centerWidth)
        {
            // Upper Left Corner
            if (IsLeftBorderVisible) sb.Append(left);

            // Cells
            var totalColumns = columns.Count;
            for (var j = 0; j < totalColumns; j++)
            {
                // cell separator
                var upperSep = upperRow != null && upperRow.HasCellSeparatorInPosition(j);
                var lowerSep = lowerRow != null && lowerRow.HasCellSeparatorInPosition(j);

                if (j != 0)
                {
                    if ((j > 1 && j < totalColumns - 1 && IsCenterSeparatorVisible)
                        || (j == 1 && IsLeftSeparatorVisible)
                        || (j == totalColumns - 1 && IsRightSeparatorVisible))
                    {
                        if (upperSep)
                        {
                            sb.Append(lowerSep ? cross : upperColSpan);
                        }
                        else if (lowerSep)
                        {
                            sb.Append(lowerColSpan);
                        }
                        else
                        {
                            sb.Append(horizontal, centerWidth);
                        }
                    }
                }

                // Cell content
                var col = columns[j];
                sb.Append(horizontal, col.ActualWidth);
            }

            // Right border
            if (IsRightBorderVisible) sb.Append(right);
        }

        private static bool Get(string flags, int index)
        {
            return flags != null 
                && index >= 0
                && index <= flags.Length
                && 't' == char.ToLowerInvariant(flags[index]);
        }
    }
}