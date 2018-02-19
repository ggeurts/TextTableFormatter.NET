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
    using System.IO;
    using System.Text;

    /// <summary>
    /// Represents a table cell
    /// </summary>
    public class Cell
    {
        private static readonly char[] LineEndChars = { '\r', '\n' };

        private readonly object _content;

        /// <summary>
        /// Gets the cell content lines
        /// </summary>
        private readonly List<string> _lines = new List<string>();

        /// <summary>
        /// Gets the cell style
        /// </summary>
        public CellStyle Style { get; }

        /// <summary>
        /// Gets the cell column span
        /// </summary>
        public int ColumnSpan { get; }

        /// <summary>
        /// Gets the actual cell width as calculated during layout phase.
        /// </summary>
        internal int ActualWidth { get; private set; }

        /// <summary>
        /// Gets the actual cell style as calculated during layout phase.
        /// </summary>
        internal CellStyle ActualStyle { get; private set; }

        /// <summary>
        /// Gets the number of cell content lines.
        /// </summary>
        public int LineCount
        {
            get { return Math.Max(_lines.Count, 1); }
        }

        public string this[int lineIndex]
        {
            get
            {
                return lineIndex < _lines.Count
                    ? _lines[lineIndex]
                    : null;
            }
        }

        public Cell() 
            : this(null, new CellStyle())
        {}

        public Cell(object content) 
            : this(content, null)
        {}

        public Cell(object content, CellStyle style, int columnSpan = 1)
        {
            _content = content;
            this.Style = style;
            this.ColumnSpan = columnSpan;
        }

        internal void PerformLayout(int maxWidth, CellStyle actualStyle)
        {
            if (actualStyle == null) throw new ArgumentNullException(nameof(actualStyle));
            this.ActualStyle = actualStyle;

            _lines.Clear();
            AddLines(maxWidth, actualStyle);

            var desiredWidth = 0;
            for (var i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].Length > desiredWidth) desiredWidth = _lines[i].Length;
            }

            this.ActualWidth = Math.Min(desiredWidth, maxWidth);
        }

        public string Render(int width)
        {
            var actualStyle = this.Style ?? CellStyle.Default;
            PerformLayout(width, actualStyle);

            var sb = new StringBuilder();
            for (var i = 0; i < this.LineCount; i++)
            {
                actualStyle.Render(sb, this[i], width);
            }
            return sb.ToString();
        }

        private void AddLines(int maxWidth, CellStyle actualStyle)
        {
            var content = actualStyle.GetContentLines(_content);
            if (string.IsNullOrEmpty(content)) return;

            var lineEndPos = content.IndexOfAny(LineEndChars);
            if (lineEndPos < 0)
            {
                AddLinesWithTextWrapping(content, maxWidth, actualStyle);
                return;
            }

            using (var sr = new StringReader(content))
            {
                for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                {
                    AddLinesWithTextWrapping(line, maxWidth, actualStyle);
                }
            }
        }

        private void AddLinesWithTextWrapping(string content, int maxWidth, CellStyle actualStyle)
        {
            if (string.IsNullOrEmpty(content)) return;

            if (content.Length <= maxWidth || actualStyle.TextWrapping == CellTextWrapping.NoWrap)
            {
                _lines.Add(content);
                return;
            }

            var lineBegin = 0;
            while (lineBegin + maxWidth < content.Length)
            {
                var lineEnd = LastIndexOfLineBreakOpportunity(content, lineBegin, maxWidth);
                if (lineEnd < lineBegin)
                {
                    _lines.Add(content.Substring(lineBegin, maxWidth));
                    lineBegin += maxWidth;
                }
                else if (char.IsWhiteSpace(content[lineEnd]))
                {
                    _lines.Add(content.Substring(lineBegin, lineEnd - lineBegin));
                    lineBegin = lineEnd + 1;
                }
                else
                {
                    _lines.Add(content.Substring(lineBegin, lineEnd + 1 - lineBegin));
                    lineBegin = lineEnd + 1;
                }
            }

            if (lineBegin < content.Length)
            {
                _lines.Add(content.Substring(lineBegin));
            }
        }

        private static int LastIndexOfLineBreakOpportunity(string content, int lineBegin, int maxWidth)
        {
            for (var i = lineBegin + maxWidth - 1; i >= lineBegin; i--)
            {
                var ch = content[i];
                if (char.IsWhiteSpace(ch) || char.IsPunctuation(ch) || char.IsSeparator(ch)) return i;
            }
            return -1;
        }
    }
}