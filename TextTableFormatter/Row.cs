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