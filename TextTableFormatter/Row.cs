using System.Collections.Generic;

namespace TextTableFormatter
{
    internal class Row
    {
        internal IList<Cell> Cells { get; }

        public Row()
        {
            Cells = new List<Cell>();
        }

        internal int ColumnSpan
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