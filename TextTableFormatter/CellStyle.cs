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
    using System.Text;

    /// <summary>
    /// Represents a cell style
    /// </summary>
    public class CellStyle
    {
        private const string DOTS_TEXT = "...";

        /// <summary>
        /// Gets or sets the cell horizontal alignment
        /// </summary>
        public CellTextAlignment TextAlignment { get; }

        /// <summary>
        /// Gets or sets the cell content abbreviation style
        /// </summary>
        public CellTextTrimming TextTrimming { get; }

        /// <summary>
        /// Gets or sets the cell content wrapping style
        /// </summary>
        public CellTextWrapping TextWrapping { get; }

        public string NullText { get; }

        public CellStyle(
            CellTextAlignment textAlignment = CellTextAlignment.Left, 
            CellTextTrimming textTrimming = CellTextTrimming.Crop, 
            CellTextWrapping textWrapping = CellTextWrapping.NoWrap,
            string nullText = null)
        {
            this.TextAlignment = textAlignment;
            this.TextTrimming = textTrimming;
            this.TextWrapping = textWrapping;
            this.NullText = nullText;
        }

        public void Render(StringBuilder sb, string line, int width)
        {
            if (string.IsNullOrEmpty(line))
            {
                sb.Append(' ', width);
            }
            else if (line.Length < width)
            {
                RenderPadded(sb, line, width);
            }
            else if (line.Length == width)
            {
                sb.Append(line);
            }
            else
            {
                RenderTrimmed(sb, line, width);
            }
        }

        private void RenderPadded(StringBuilder sb, string line, int width)
        {
            var padding = width - line.Length;
            switch (this.TextAlignment)
            {
                case CellTextAlignment.Left:
                    sb.Append(line).Append(' ', padding);
                    break;
                case CellTextAlignment.Center:
                    var paddingLeft = padding / 2;
                    var paddingRight = padding - paddingLeft;
                    sb.Append(' ', paddingLeft).Append(line).Append(' ', paddingRight);
                    break;
                default:
                    sb.Append(' ', padding).Append(line);
                    break;
            }
        }

        private void RenderTrimmed(StringBuilder sb, string line, int width)
        {
            switch (this.TextTrimming)
            {
                case CellTextTrimming.Crop:
                    sb.Append(line, 0, width);
                    break;
                default:
                    if (width <= DOTS_TEXT.Length)
                    {
                        sb.Append(DOTS_TEXT, 0, width);
                    }
                    else
                    {
                        sb.Append(line, 0, width - DOTS_TEXT.Length).Append(DOTS_TEXT);
                    }
                    break;
            }
        }
    }
}