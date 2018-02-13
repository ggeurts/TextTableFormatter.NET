namespace TextTableFormatter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Represents a table cell
    /// </summary>
    internal class Cell
    {
        private static readonly char[] LineEndChars = { '\r', '\n' };

        private readonly string _content;

        /// <summary>
        /// Gets the cell content lines
        /// </summary>
        private readonly List<string> _lines = new List<string>();

        /// <summary>
        /// Gets the cell style
        /// </summary>
        public CellStyle Style { get; private set; }

        /// <summary>
        /// Gets the cell column span
        /// </summary>
        public int ColumnSpan { get; private set; }

        /// <summary>
        /// Gets the width required to display cell content without wrapping 
        /// as calculated during layout phase.
        /// </summary>
        internal int DesiredWidth
        {
            get
            {
                var maxLength = 0;
                for (var i = 0; i < _lines.Count; i++)
                {
                    if (_lines[i].Length > maxLength) maxLength = _lines[i].Length;
                }

                return maxLength;
            }
        }

        /// <summary>
        /// Gets the actual cell width as calculated during layout phase.
        /// </summary>
        internal int ActualWidth { get; private set; }

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

        public Cell(string content) 
            : this(content, new CellStyle())
        {}

        public Cell(string content, CellStyle style, int columnSpan = 1)
        {
            _content = content;
            this.Style = style;
            this.ColumnSpan = columnSpan;
        }

        public void PerformLayout(int maxWidth)
        {
            _lines.Clear();
            AddLines(_lines, _content, maxWidth);

            this.ActualWidth = Math.Min(this.DesiredWidth, maxWidth);
        }

        public string Render(int width)
        {
            PerformLayout(width);

            var sb = new StringBuilder();
            for (var i = 0; i < this.LineCount; i++)
            {
                Render(sb, width, i);
            }
            return sb.ToString();
        }

        public void Render(StringBuilder sb, int width, int lineIndex)
        {
            var line = this[lineIndex];
            if (lineIndex == 0 && string.IsNullOrEmpty(line)) line = this.Style.NullText;
            this.Style.Render(sb, line, width);
        }

        private void AddLines(List<string> lines, string content, int maxWidth)
        {
            if (string.IsNullOrEmpty(content)) return;

            var lineEndPos = content.IndexOfAny(LineEndChars);
            if (lineEndPos < 0)
            {
                AddLinesWithTextWrapping(lines, content, maxWidth);
                return;
            }

            using (var sr = new StringReader(content))
            {
                for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                {
                    AddLinesWithTextWrapping(lines, line, maxWidth);
                }
            }
        }

        private void AddLinesWithTextWrapping(List<string> lines, string content, int maxWidth)
        {
            if (string.IsNullOrEmpty(content)) return;

            if (content.Length <= maxWidth || this.Style.TextWrapping == CellTextWrappingStyle.NoWrap)
            {
                lines.Add(content);
                return;
            }

            var lineBegin = 0;
            while (lineBegin + maxWidth < content.Length)
            {
                var lineEnd = LastIndexOfLineBreakOpportunity(content, lineBegin, maxWidth);
                if (lineEnd < lineBegin)
                {
                    lines.Add(content.Substring(lineBegin, maxWidth));
                    lineBegin += maxWidth;
                }
                else if (char.IsWhiteSpace(content[lineEnd]))
                {
                    lines.Add(content.Substring(lineBegin, lineEnd - lineBegin));
                    lineBegin = lineEnd + 1;
                }
                else
                {
                    lines.Add(content.Substring(lineBegin, lineEnd + 1 - lineBegin));
                    lineBegin = lineEnd + 1;
                }
            }

            if (lineBegin < content.Length)
            {
                lines.Add(content.Substring(lineBegin));
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