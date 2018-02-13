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
    /// <summary>
    /// Represents the cell horizontal alignment
    /// </summary>
    public enum CellTextAlignment
    {
        /// <summary>
        /// Left horizontal alignment
        /// </summary>
        Left,

        /// <summary>
        /// Center horizontal alignment
        /// </summary>
        Center,

        /// <summary>
        /// Right horizontal alignment
        /// </summary>
        Right
    };
}